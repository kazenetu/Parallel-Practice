using Parallel_Practice.Practices;

namespace Parallel_Practice;

class Program
{
    static void Main(string[] args)
    {
        // 練習クラス：Parallel.Invoke：実行
        ParallelInvokePractice.Run();

        // 練習クラス：Parallel.Invoke：リスト設定：実行
        ParallelInvokeReturnStringPractice.Run();
    }
}
