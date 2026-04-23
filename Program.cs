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
    }
}
