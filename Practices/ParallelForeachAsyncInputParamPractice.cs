using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace Parallel_Practice.Practices;

class ParallelForeachAsyncInputParamPractice
{
    public static async Task RunAsync()
    {
        Console.WriteLine();
        Console.WriteLine("〜〜練習クラス：Parallel.ForEach：非同期：パラメータ設定〜〜");

        // アクション生成数
        var actionCreateCount = 10;
        Console.WriteLine($"　　アクション生成数:{actionCreateCount}");
        Console.WriteLine();

        // プログレス単位
        var progressUnit = actionCreateCount / 10;

        // まとめてConsole.WriteLineするための文字列リスト
        var consoleWites = new ConcurrentBag<string>();

        // 実行速度確認リスト
        var funcs = new List<Func<long, Task>>();
        for (int i = 0; i < actionCreateCount; i++)
        {
            funcs.Add(
                async no =>
                {
                    // 重い処理を想定
                    await Task.Delay(1000);

                    // アクション番号や処理時間の文字列を生成
                    var result = $"Hello, World! {no:0000} {DateTime.Now.ToString("HH:mm:ss.fff")}{Environment.NewLine}";

                    // 文字列リストに追加
                    consoleWites.Add(result);

                    // プログレス確認と描画
                    if (no % progressUnit == 0)
                        Console.Write("＝");
                }
            );
        }

        // 詳細出力用文字列
        var output = new StringBuilder();
        output.AppendLine();
        output.AppendLine("〜〜詳細出力〜〜");

        // 複数タスク実行と完了待機
        var titleTaskFactoryStartNew = "Task.Factory.StartNew";
        Console.WriteLine($"----{titleTaskFactoryStartNew}----");
        var sw = new Stopwatch();
        consoleWites.Clear();
        sw.Start();
        var tasks = new List<Task>();
        foreach (var func in funcs)
        {
            var no = tasks.Count + 1;
            tasks.Add(func(no));
        }
        foreach (var task in tasks)
        {
            task.Wait();
        }
        Console.WriteLine();
        sw.Stop();
        var timeTaskFactoryStartNew = $">>実行結果：{sw.Elapsed}";
        Console.WriteLine(timeTaskFactoryStartNew);

        // 複数タスク実行と完了待機：詳細出力
        output.AppendLine($"--{titleTaskFactoryStartNew}：詳細--");
        output.AppendLine(string.Join(string.Empty, consoleWites));
        output.AppendLine(timeTaskFactoryStartNew);
        output.AppendLine();

        // Parallel.ForEachAsync実行
        var titleParallelForEachAsync = "Parallel.ForEachAsync";
        Console.WriteLine($"----{titleParallelForEachAsync}----");
        consoleWites.Clear();
        sw.Reset();
        var noList = new ConcurrentBag<long>();
        sw.Start();
        await Parallel.ForEachAsync(funcs, async (func, ct) =>
        {
            noList.Add(1);
            var no = noList.Count + 1;
            await func(no);
        });
        Console.WriteLine();
        sw.Stop();
        var timeParallelForEachAsync = $">>実行結果：{sw.Elapsed}";
        Console.WriteLine(timeParallelForEachAsync);

        // Parallel.ForEachAsync実行：実行件数：アクション生成数
        var titleParallelForEachAsyncMaxIsActionCreateCount = "Parallel.ForEachAsync：最大並列数:アクション生成数";
        Console.WriteLine($"----{titleParallelForEachAsyncMaxIsActionCreateCount}----");
        consoleWites.Clear();
        sw.Reset();
        noList.Clear();
        sw.Start();
        await Parallel.ForEachAsync(funcs, new ParallelOptions() { MaxDegreeOfParallelism = actionCreateCount }, async (func, ct) =>
        {
            noList.Add(1);
            var no = noList.Count + 1;
            await func(no);
        });
        Console.WriteLine();
        sw.Stop();
        var timeParallelForEachAsyncMaxIsActionCreateCount = $">>実行結果：{sw.Elapsed}";
        Console.WriteLine(timeParallelForEachAsyncMaxIsActionCreateCount);

        // Parallel.ForEachAsync：詳細出力
        output.AppendLine($"--{titleParallelForEachAsyncMaxIsActionCreateCount}：詳細--");
        output.AppendLine(string.Join(string.Empty, consoleWites));
        output.AppendLine(timeParallelForEachAsyncMaxIsActionCreateCount);

        // 出力結果詳細
        Console.WriteLine();
        Console.WriteLine(output.ToString());
    }
}