using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Parallel_Practice.Practices;

class ParallelInvokeReturnStringPractice
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("〜〜練習クラス：Parallel.Invoke：リスト設定〜〜");

        // アクション生成数
        var actionCreateCount = 50;
        Console.WriteLine($"　　アクション生成数:{actionCreateCount}");
        Console.WriteLine();

        // プログレス単位
        var progressUnit = actionCreateCount / 10;

        // まとめてConsole.WriteLineするための文字列リスト
        var consoleWites = new ConcurrentBag<string>();

        // 実行速度確認リスト
        var actions = new List<Action>();
        for (int i = 0; i < actionCreateCount; i++)
        {
            var no = i + 1;
            actions.Add(
                () =>
                {
                    // 重い処理を想定
                    Thread.Sleep(1000);

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
        foreach (var action in actions)
        {
            tasks.Add(Task.Factory.StartNew(action));
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

        // Parallel.Invoke実行
        var titleParallelInvoke = "Parallel.Invoke";
        Console.WriteLine($"----{titleParallelInvoke}----");
        sw.Reset();
        sw.Start();
        Parallel.Invoke([.. actions]);
        sw.Stop();
        var timeParallelInvoke = $">>実行結果：{sw.Elapsed}";
        Console.WriteLine(timeParallelInvoke);

        // Parallel.Invoke実行：詳細出力
        output.AppendLine($"--{titleParallelInvoke}：詳細--");
        output.AppendLine(string.Join(string.Empty, consoleWites));
        output.AppendLine(timeParallelInvoke);

        // 出力結果詳細
        Console.WriteLine();
        Console.WriteLine(output.ToString());
    }
}