using BenchmarkDotNet.Running;
using System;

namespace XXHashBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<XXHashBench>();
            //XXHash64Benchmarks.Run();
            //Console.ReadLine();
        }
    }
}
