using System.Diagnostics;

namespace Parallel_Practice;

class Program
{
    static void Main(string[] args)
    {
        // 実行速度確認リスト
        var actions = new List<Action>();
        for (int i = 0; i < 10; i++)
        {
            var no = i + 1;
            actions.Add(
                () =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Hello, World! {no:0000} {DateTime.Now.ToString("HH:mm:ss.fff")}");
                }
            );            
        }

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
        var tasks = new List<Task>();
        foreach (var action in actions)
        {
            //tasks.Add(Task.Run((action)));
            tasks.Add(Task.Factory.StartNew(action));
        }
        Console.WriteLine("----Task.Factory.StartNew----");
        sw.Start();
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
