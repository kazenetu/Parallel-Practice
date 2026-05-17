using Parallel_Practice.Practices;

namespace Parallel_Practice;

class Program
{
    /// <summary>
    /// パラメータ
    /// </summary>
    private enum Params
    {
        all,
        invoke, invoke_string, 
        foreach_inputparam, foreach_async
    };

    /// <summary>
    /// メイン処理
    /// </summary>
    /// <param name="args">パラメータ</param>
    static async Task Main(string[] args)
    {
        // 引数なしの場合はヘルプ
        if (args.Length <= 0)
        {
            Console.WriteLine("dotnet run <パラメータ>");
            Console.WriteLine($"パラメータ：{Params.all}                   すべて実施");
            Console.WriteLine($"　　　　　：{Params.invoke}                Parallel.Invoke   コンソール表示");
            Console.WriteLine($"　　　　　：{Params.invoke_string}         Parallel.Invoke   ConcurrentBag使用");
            Console.WriteLine($"　　　　　：{Params.foreach_inputparam}    Parallel.ForEach  パラメータ設定");
            Console.WriteLine($"　　　　　：{Params.foreach_async}         Parallel.ForEach(非同期)  パラメータ設定");
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

        // 練習クラス：Parallel.ForEach：パラメータ設定：実行
        if (param == $"{Params.all}" || param == $"{Params.foreach_inputparam}")
            ParallelForeachInputParamPractice.Run();

        // 練習クラス：Parallel.ForEach：非同期：パラメータ設定
        if (param == $"{Params.all}" || param == $"{Params.foreach_async}")
            await ParallelForeachAsyncInputParamPractice.RunAsync();
    }
}
