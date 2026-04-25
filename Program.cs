using System.Diagnostics;

namespace Parallel_Practice;

class Program
{
    static void Main(string[] args)
    {
        // 実行速度確認リスト
        var actions = new List<Action>
        {
           () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Hello, World11! {DateTime.Now.ToString("HH:mm:ss.fff")}");
            },
           () =>
                {
                Thread.Sleep(1000);
                Console.WriteLine($"Hello, World2! {DateTime.Now.ToString("HH:mm:ss.fff")}");
            },
            () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Hello, World13 {DateTime.Now.ToString("HH:mm:ss.fff")}");
            }
        };

        var sw = new Stopwatch();
        Console.WriteLine("----同期実行----");
        sw.Start();
        foreach (var action in actions)
        {
            action();
        }
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");

        // タスクリスト変換・実行
        sw.Reset();
        var tasks = new List<Task>();
        foreach (var action in actions)
        {
            //tasks.Add(Task.Run((action)));
            tasks.Add(Task.Factory.StartNew((action)));
        }
        Console.WriteLine("----Task.Factory.StartNew----");
        sw.Start();
        foreach (var task in tasks)
        {
            task.Wait();
        }
        sw.Stop();
        Console.WriteLine($">>実行結果：{sw.Elapsed}");

    }
}
