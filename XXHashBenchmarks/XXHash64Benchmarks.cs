using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXHash;

namespace XXHashBenchmarks
{
    public class XXHash64Benchmarks
    {
        public static void Run()
        {
            Random rd = new Random();
            byte[] bytes = new byte[1024];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)rd.Next(0, byte.MaxValue);

            ulong hash1 = 0;
            ulong hash2 = 0;
            Stopwatch w = Stopwatch.StartNew();
            for (int i = 0; i < 10000000; i++)
            {
                //hash1 = XXHash64.Calculate(bytes);
                hash2 = XXHash64Old.Calculate(bytes);
            }

            w.Stop();
            Console.WriteLine($"cost:{w.ElapsedMilliseconds}");
            Console.WriteLine(hash1 == hash2);
        }

    }
}
