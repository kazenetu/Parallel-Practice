using System.Diagnostics;

namespace Parallel_Practice.Practices;

class ParallelInvokePractice
{
    public static void Run()
    {
        // 実行速度確認リスト
        var actions = new List<Action>();
        for (int i = 0; i < 10; i++)
        {
            var no = i + 1;
            actions.Add(
                () =>
                {
                    // 重い処理を想定
                    Thread.Sleep(1000);

                    // アクション番号や処理時間を表示
                    Console.WriteLine($"Hello, World! {no:0000} {DateTime.Now.ToString("HH:mm:ss.fff")}");
                }
            );
        }

        // 同期実行
        var sw = new Stopwatch();
        Console.WriteLine("----同期実行----");
        sw.Start();
        foreach (var action in actions)
        {
            action();
        }
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");

        // 複数タスク実行と完了待機
        sw.Reset();
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
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");

        // Parallel.Invoke実行
        sw.Reset();
        Console.WriteLine("----Parallel.Invoke----");
        sw.Start();
        Parallel.Invoke([.. actions]);
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");
    }
}