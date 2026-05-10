using System.ComponentModel;
using System.Runtime.CompilerServices;
using Parallel_Practice.Practices;

namespace Parallel_Practice;

class Program
{
    /// <summary>
    /// パラメータ
    /// </summary>
    private enum  Params
    {
        all,
        invoke, invoke_string
    };

    /// <summary>
    /// メイン処理
    /// </summary>
    /// <param name="args">パラメータ</param>
    static void Main(string[] args)
    {
        // 引数なしの場合はヘルプ
        if (args.Length <= 0)
        {
            Console.WriteLine("dotnet run <パラメータ>");
            Console.WriteLine($"パラメータ：{Params.all}\tすべて実施");
            Console.WriteLine($"　　　　　：{Params.invoke}\tParallel.Invokeコンソール表示");
            Console.WriteLine($"　　　　　：{Params.invoke_string}\tParallel.Invoke ConcurrentBag使用");
            return;
        }

        // パラメータ設定
        var param = args[0];

        // 練習クラス：Parallel.Invoke：実行
        if (param == $"{Params.all}" || param == $"{Params.invoke}")
            ParallelInvokePractice.Run();

        // 練習クラス：Parallel.Invoke：リスト設定：実行
        if (param == $"{Params.all}" || param == $"{Params.invoke_string}")
            ParallelInvokeReturnStringPractice.Run();
    }
}
