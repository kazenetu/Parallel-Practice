using System.Collections.Concurrent;
using System.Diagnostics;

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


        // 複数タスク実行と完了待機
        var sw = new Stopwatch();
        consoleWites.Clear();
        sw.Start();
        var tasks = new List<Task>();
        foreach (var action in actions)
        {
            tasks.Add(Task.Factory.StartNew(action));
        }
        Console.WriteLine("----Task.Factory.StartNew----");
        foreach (var task in tasks)
        {
            task.Wait();
        }
        Console.WriteLine();
        Console.WriteLine(string.Join(string.Empty, consoleWites));
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");

        // Parallel.Invoke実行
        sw.Reset();
        consoleWites.Clear();
        Console.WriteLine("----Parallel.Invoke----");
        sw.Start();
        Parallel.Invoke([.. actions]);
        Console.WriteLine();
        Console.WriteLine(string.Join(string.Empty, consoleWites));
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");
    }
}