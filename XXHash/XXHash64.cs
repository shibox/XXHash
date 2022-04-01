using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XXHash
{
    public static partial class XXHash64
    {
        private const ulong PRIME64_1 = 11400714785074694791UL;
        private const ulong PRIME64_2 = 14029467366897019727UL;
        private const ulong PRIME64_3 = 1609587929392839161UL;
        private const ulong PRIME64_4 = 9650029242287828579UL;
        private const ulong PRIME64_5 = 2870177450012600261UL;

        public unsafe static ulong Hash(string input, Encoding encoder, ulong seed = 0)
        {
            var buf = encoder.GetBytes(input);
            fixed (byte* buffer = buf)
            {
                return Hash(buffer, buf.Length, seed);
            }
        }

        public unsafe static ulong Hash(string input, ulong seed = 0)
        {
            fixed (char* buffer = input)
            {
                return Hash((byte*)buffer, input.Length * sizeof(char), seed);
            }
        }

        public unsafe static ulong Hash(string input, int offset, int count, ulong seed = 0)
        {
            fixed (char* @in = input)
            {
                char* pd = @in;
                pd += offset;
                return Hash((byte*)pd, count * sizeof(char), seed);
            }
        }

        public unsafe static ulong Hash(this char[] input, int offset, int count, ulong seed = 0)
        {
            if (input == null || input.Length == 0 || count == 0)
                return 0;
            fixed (Char* @in = &input[offset])
            {
                return Hash((byte*)@in, count * sizeof(Char),seed);
            }
        }

        public unsafe static ulong Hash(this char[] input, ulong seed = 0)
        {
            if (input == null || input.Length == 0)
                return 0;
            fixed (Char* @in = &input[0])
            {
                return Hash((byte*)@in, input.Length * sizeof(Char),seed);
            }
        }

        public static unsafe ulong Hash(Span<byte> input, ulong seed = 0)
        {
            fixed (byte* ptr = input)
            { 
                return Hash(ptr, input.Length, seed);
            }
        }

        public static unsafe ulong Hash(byte* input, int count, ulong seed = 0)
        {
            ulong h64;
            byte* bEnd = input + count;
            if (count >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1 = seed + PRIME64_1 + PRIME64_2;
                ulong v2 = seed + PRIME64_2;
                ulong v3 = seed + 0;
                ulong v4 = seed - PRIME64_1;

                do
                {
                    v1 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v1 = rol31(v1);
                    v2 = rol31(v2);
                    v3 = rol31(v3);
                    v4 = rol31(v4);

                    v1 *= PRIME64_1;
                    v2 *= PRIME64_1;
                    v3 *= PRIME64_1;
                    v4 *= PRIME64_1;
                }
                while (input <= limit);

                h64 = rol1(v1) + rol7(v2) + rol12(v3) + rol18(v4);

                v1 *= PRIME64_2;
                v1 = rol31(v1);
                v1 *= PRIME64_1;
                h64 ^= v1;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v2 *= PRIME64_2;
                v2 = rol31(v2);
                v2 *= PRIME64_1;
                h64 ^= v2;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v3 *= PRIME64_2;
                v3 = rol31(v3);
                v3 *= PRIME64_1;
                h64 ^= v3;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v4 *= PRIME64_2;
                v4 = rol31(v4);
                v4 *= PRIME64_1;
                h64 ^= v4;
                h64 = h64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h64 = seed + PRIME64_5;
            }

            h64 += (ulong)count;


            while (input + 8 <= bEnd)
            {
                ulong k1 = *((ulong*)input);
                k1 *= PRIME64_2;
                k1 = rol31(k1);
                k1 *= PRIME64_1;
                h64 ^= k1;
                h64 = rol27(h64) * PRIME64_1 + PRIME64_4;
                input += 8;
            }

            if (input + 4 <= bEnd)
            {
                h64 ^= *(uint*)input * PRIME64_1;
                h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
                input += 4;
            }

            while (input < bEnd)
            {
                h64 ^= ((ulong)*input) * PRIME64_5;
                h64 = rol11(h64) * PRIME64_1;
                input++;
            }

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;

            return h64;
        }

        public static unsafe ulong Hash(byte* input, uint start,uint end, ulong seed = 0)
        {
            input += start;
            uint count = end - start;
            ulong h64;
            byte* bEnd = input + count;
            if (count >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1 = seed + PRIME64_1 + PRIME64_2;
                ulong v2 = seed + PRIME64_2;
                ulong v3 = seed + 0;
                ulong v4 = seed - PRIME64_1;

                do
                {
                    v1 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v1 = rol31(v1);
                    v2 = rol31(v2);
                    v3 = rol31(v3);
                    v4 = rol31(v4);

                    v1 *= PRIME64_1;
                    v2 *= PRIME64_1;
                    v3 *= PRIME64_1;
                    v4 *= PRIME64_1;
                }
                while (input <= limit);

                h64 = rol1(v1) + rol7(v2) + rol12(v3) + rol18(v4);

                v1 *= PRIME64_2;
                v1 = rol31(v1);
                v1 *= PRIME64_1;
                h64 ^= v1;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v2 *= PRIME64_2;
                v2 = rol31(v2);
                v2 *= PRIME64_1;
                h64 ^= v2;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v3 *= PRIME64_2;
                v3 = rol31(v3);
                v3 *= PRIME64_1;
                h64 ^= v3;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v4 *= PRIME64_2;
                v4 = rol31(v4);
                v4 *= PRIME64_1;
                h64 ^= v4;
                h64 = h64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h64 = seed + PRIME64_5;
            }

            h64 += (ulong)count;


            while (input + 8 <= bEnd)
            {
                ulong k1 = *((ulong*)input);
                k1 *= PRIME64_2;
                k1 = rol31(k1);
                k1 *= PRIME64_1;
                h64 ^= k1;
                h64 = rol27(h64) * PRIME64_1 + PRIME64_4;
                input += 8;
            }

            if (input + 4 <= bEnd)
            {
                h64 ^= *(uint*)input * PRIME64_1;
                h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
                input += 4;
            }

            while (input < bEnd)
            {
                h64 ^= ((ulong)*input) * PRIME64_5;
                h64 = rol11(h64) * PRIME64_1;
                input++;
            }

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;

            return h64;
        }

        public static unsafe ulong HashUnroll(byte* input, int count, ulong seed = 0)
        {
            if (count > 8)
                return Hash(input, count, seed);
            ulong h64 = 0;
            switch (count)
            {
                case 0:
                    return 0;
                case 1:
                    h64 = seed + PRIME64_5 + 1;

                    h64 ^= ((ulong)*input) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    input++;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;
                case 2:
                    h64 = seed + PRIME64_5 + 2;

                    h64 ^= ((ulong)*input) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input + 1)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    input += 2;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;
                case 3:
                    h64 = seed + PRIME64_5 + 3;

                    h64 ^= ((ulong)*input) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input+1)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input+2)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    input += 3;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;
                case 4:
                    h64 = seed + PRIME64_5 + 4;

                    h64 ^= *(uint*)input * PRIME64_1;
                    h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
                    input += 4;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;

                case 5:
                    h64 = seed + PRIME64_5 + 5;

                    h64 ^= *(uint*)input * PRIME64_1;
                    h64 = rol23(h64) * PRIME64_2 + PRIME64_3;

                    h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;

                    input += 5;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;

                case 6:
                    h64 = seed + PRIME64_5 + 6;

                    h64 ^= *(uint*)input * PRIME64_1;
                    h64 = rol23(h64) * PRIME64_2 + PRIME64_3;

                    h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input + 5)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;

                    input += 6;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;

                case 7:
                    h64 = seed + PRIME64_5 + 4;

                    h64 ^= *(uint*)input * PRIME64_1;
                    h64 = rol23(h64) * PRIME64_2 + PRIME64_3;

                    h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input + 5)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;
                    h64 ^= ((ulong)*(input + 6)) * PRIME64_5;
                    h64 = rol11(h64) * PRIME64_1;

                    input += 7;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;

                case 8:
                    h64 = seed + PRIME64_5 + 4;

                    ulong k1 = *((ulong*)input);
                    k1 *= PRIME64_2;
                    k1 = rol31(k1);
                    k1 *= PRIME64_1;
                    h64 ^= k1;
                    h64 = rol27(h64) * PRIME64_1 + PRIME64_4;
                    input += 8;

                    h64 ^= h64 >> 33;
                    h64 *= PRIME64_2;
                    h64 ^= h64 >> 29;
                    h64 *= PRIME64_3;
                    h64 ^= h64 >> 32;
                    break;
            }
            return h64;
        }

        public static unsafe ulong Hash1(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 1;

            h64 ^= ((ulong)*input) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            
            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash2(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 2;

            h64 ^= ((ulong)*input) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input+1)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash3(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 3;

            h64 ^= ((ulong)*input) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input + 1)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input + 2)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash4(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 4;

            h64 ^= *(uint*)input * PRIME64_1;
            h64 = rol23(h64) * PRIME64_2 + PRIME64_3;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash5(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 5;

            h64 ^= *(uint*)input * PRIME64_1;
            h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
            h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash6(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 6;

            h64 ^= *(uint*)input * PRIME64_1;
            h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
            h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input + 5)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash7(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 7;

            h64 ^= *(uint*)input * PRIME64_1;
            h64 = rol23(h64) * PRIME64_2 + PRIME64_3;
            h64 ^= ((ulong)*(input + 4)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input + 5)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;
            h64 ^= ((ulong)*(input + 6)) * PRIME64_5;
            h64 = rol11(h64) * PRIME64_1;

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe ulong Hash8(byte* input, ulong seed = 0)
        {
            ulong h64 = seed + PRIME64_5 + 8;

            ulong k1 = *((ulong*)input);
            k1 *= PRIME64_2;
            k1 = rol31(k1);
            k1 *= PRIME64_1;
            h64 ^= k1;
            h64 = rol27(h64) * PRIME64_1 + PRIME64_4;
            
            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;
            return h64;
        }

        public static unsafe void Hash(byte* input, int count, ref ulong r1, ref ulong r2, ulong s1 = 0,ulong s2=1)
        {
            ulong h1_64;
            ulong h2_64;
            byte* bEnd = input + count;

            if (count >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1_1 = s1 + PRIME64_1 + PRIME64_2;
                ulong v2_1 = s1 + PRIME64_2;
                ulong v3_1 = s1 + 0;
                ulong v4_1 = s1 - PRIME64_1;

                ulong v1_2 = s2 + PRIME64_1 + PRIME64_2;
                ulong v2_2 = s2 + PRIME64_2;
                ulong v3_2 = s2 + 0;
                ulong v4_2 = s2 - PRIME64_1;

                do
                {
                    v1_1 += *((ulong*)input) * PRIME64_2;
                    v1_2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v2_1 += *((ulong*)input) * PRIME64_2;
                    v2_2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v3_1 += *((ulong*)input) * PRIME64_2;
                    v3_2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);
                    v4_1 += *((ulong*)input) * PRIME64_2;
                    v4_2 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v1_1 = rol31(v1_1);
                    v1_2 = rol31(v1_2);

                    v2_1 = rol31(v2_1);
                    v2_2 = rol31(v2_2);

                    v3_1 = rol31(v3_1);
                    v3_2 = rol31(v3_2);

                    v4_1 = rol31(v4_1);
                    v4_2 = rol31(v4_2);

                    v1_1 *= PRIME64_1;
                    v1_2 *= PRIME64_1;

                    v2_1 *= PRIME64_1;
                    v2_2 *= PRIME64_1;

                    v3_1 *= PRIME64_1;
                    v3_2 *= PRIME64_1;

                    v4_1 *= PRIME64_1;
                    v4_2 *= PRIME64_1;
                }
                while (input <= limit);

                h1_64 = rol1(v1_1) + rol7(v2_1) + rol12(v3_1) + rol18(v4_1);
                h2_64 = rol1(v1_2) + rol7(v2_2) + rol12(v3_2) + rol18(v4_2);

                v1_1 *= PRIME64_2;
                v1_2 *= PRIME64_2;

                v1_1 = rol31(v1_1);
                v1_2 = rol31(v1_2);

                v1_1 *= PRIME64_1;
                v1_2 *= PRIME64_1;

                h1_64 ^= v1_1;
                h2_64 ^= v1_2;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;

                v2_1 *= PRIME64_2;
                v2_2 *= PRIME64_2;

                v2_1 = rol31(v2_1);
                v2_2 = rol31(v2_2);

                v2_1 *= PRIME64_1;
                v2_2 *= PRIME64_1;

                h1_64 ^= v2_1;
                h2_64 ^= v2_2;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;

                v3_1 *= PRIME64_2;
                v3_2 *= PRIME64_2;

                v3_1 = rol31(v3_1);
                v3_2 = rol31(v3_2);

                v3_1 *= PRIME64_1;
                v3_2 *= PRIME64_1;

                h1_64 ^= v3_1;
                h2_64 ^= v3_2;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;

                v4_1 *= PRIME64_2;
                v4_2 *= PRIME64_2;

                v4_1 = rol31(v4_1);
                v4_2 = rol31(v4_2);

                v4_1 *= PRIME64_1;
                v4_2 *= PRIME64_1;

                h1_64 ^= v4_1;
                h2_64 ^= v4_2;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h1_64 = s1 + PRIME64_5;
                h2_64 = s2 + PRIME64_5;
            }

            h1_64 += (ulong)count;
            h2_64 += (ulong)count;


            while (input + 8 <= bEnd)
            {
                ulong k1_1 = *((ulong*)input);
                ulong k1_2 = *((ulong*)input);

                k1_1 *= PRIME64_2;
                k1_2 *= PRIME64_2;

                k1_1 = rol31(k1_1);
                k1_2 = rol31(k1_2);

                k1_1 *= PRIME64_1;
                k1_2 *= PRIME64_1;

                h1_64 ^= k1_1;
                h2_64 ^= k1_2;

                h1_64 = rol27(h1_64) * PRIME64_1 + PRIME64_4;
                h2_64 = rol27(h2_64) * PRIME64_1 + PRIME64_4;

                input += 8;
            }

            if (input + 4 <= bEnd)
            {
                h1_64 ^= *(uint*)input * PRIME64_1;
                h2_64 ^= *(uint*)input * PRIME64_1;

                h1_64 = rol23(h1_64) * PRIME64_2 + PRIME64_3;
                h2_64 = rol23(h2_64) * PRIME64_2 + PRIME64_3;

                input += 4;
            }

            while (input < bEnd)
            {
                h1_64 ^= ((ulong)*input) * PRIME64_5;
                h2_64 ^= ((ulong)*input) * PRIME64_5;

                h1_64 = rol11(h1_64) * PRIME64_1;
                h2_64 = rol11(h2_64) * PRIME64_1;

                input++;
            }

            h1_64 ^= h1_64 >> 33;
            h2_64 ^= h2_64 >> 33;

            h1_64 *= PRIME64_2;
            h2_64 *= PRIME64_2;

            h1_64 ^= h1_64 >> 29;
            h2_64 ^= h2_64 >> 29;

            h1_64 *= PRIME64_3;
            h2_64 *= PRIME64_3;

            h1_64 ^= h1_64 >> 32;
            h2_64 ^= h2_64 >> 32;

            r1 = h1_64;
            r2 = h2_64;
        }

        public static unsafe void Hash(byte* input, int count, ref ulong r1, ref ulong r2,ref ulong r3, ulong s1 = 0, ulong s2 = 1,ulong s3=2)
        {
            ulong h1_64;
            ulong h2_64;
            ulong h3_64;
            byte* bEnd = input + count;

            if (count >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1_1 = s1 + PRIME64_1 + PRIME64_2;
                ulong v2_1 = s1 + PRIME64_2;
                ulong v3_1 = s1 + 0;
                ulong v4_1 = s1 - PRIME64_1;

                ulong v1_2 = s2 + PRIME64_1 + PRIME64_2;
                ulong v2_2 = s2 + PRIME64_2;
                ulong v3_2 = s2 + 0;
                ulong v4_2 = s2 - PRIME64_1;

                ulong v1_3 = s3 + PRIME64_1 + PRIME64_2;
                ulong v2_3 = s3 + PRIME64_2;
                ulong v3_3 = s3 + 0;
                ulong v4_3 = s3 - PRIME64_1;

                do
                {
                    v1_1 += *((ulong*)input) * PRIME64_2;
                    v1_2 += *((ulong*)input) * PRIME64_2;
                    v1_3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v2_1 += *((ulong*)input) * PRIME64_2;
                    v2_2 += *((ulong*)input) * PRIME64_2;
                    v2_3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v3_1 += *((ulong*)input) * PRIME64_2;
                    v3_2 += *((ulong*)input) * PRIME64_2;
                    v3_3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v4_1 += *((ulong*)input) * PRIME64_2;
                    v4_2 += *((ulong*)input) * PRIME64_2;
                    v4_3 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v1_1 = rol31(v1_1);
                    v1_2 = rol31(v1_2);
                    v1_3 = rol31(v1_3);

                    v2_1 = rol31(v2_1);
                    v2_2 = rol31(v2_2);
                    v2_3 = rol31(v2_3);

                    v3_1 = rol31(v3_1);
                    v3_2 = rol31(v3_2);
                    v3_3 = rol31(v3_3);

                    v4_1 = rol31(v4_1);
                    v4_2 = rol31(v4_2);
                    v4_3 = rol31(v4_3);

                    v1_1 *= PRIME64_1;
                    v1_2 *= PRIME64_1;
                    v1_3 *= PRIME64_1;

                    v2_1 *= PRIME64_1;
                    v2_2 *= PRIME64_1;
                    v2_3 *= PRIME64_1;

                    v3_1 *= PRIME64_1;
                    v3_2 *= PRIME64_1;
                    v3_3 *= PRIME64_1;

                    v4_1 *= PRIME64_1;
                    v4_2 *= PRIME64_1;
                    v4_3 *= PRIME64_1;
                }
                while (input <= limit);

                h1_64 = rol1(v1_1) + rol7(v2_1) + rol12(v3_1) + rol18(v4_1);
                h2_64 = rol1(v1_2) + rol7(v2_2) + rol12(v3_2) + rol18(v4_2);
                h3_64 = rol1(v1_3) + rol7(v2_3) + rol12(v3_3) + rol18(v4_3);

                v1_1 *= PRIME64_2;
                v1_2 *= PRIME64_2;
                v1_3 *= PRIME64_2;

                v1_1 = rol31(v1_1);
                v1_2 = rol31(v1_2);
                v1_3 = rol31(v1_3);

                v1_1 *= PRIME64_1;
                v1_2 *= PRIME64_1;
                v1_3 *= PRIME64_1;

                h1_64 ^= v1_1;
                h2_64 ^= v1_2;
                h3_64 ^= v1_3;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;

                v2_1 *= PRIME64_2;
                v2_2 *= PRIME64_2;
                v2_3 *= PRIME64_2;

                v2_1 = rol31(v2_1);
                v2_2 = rol31(v2_2);
                v2_3 = rol31(v2_3);

                v2_1 *= PRIME64_1;
                v2_2 *= PRIME64_1;
                v2_3 *= PRIME64_1;

                h1_64 ^= v2_1;
                h2_64 ^= v2_2;
                h3_64 ^= v2_3;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;

                v3_1 *= PRIME64_2;
                v3_2 *= PRIME64_2;
                v3_3 *= PRIME64_2;

                v3_1 = rol31(v3_1);
                v3_2 = rol31(v3_2);
                v3_3 = rol31(v3_3);

                v3_1 *= PRIME64_1;
                v3_2 *= PRIME64_1;
                v3_3 *= PRIME64_1;

                h1_64 ^= v3_1;
                h2_64 ^= v3_2;
                h3_64 ^= v3_3;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;

                v4_1 *= PRIME64_2;
                v4_2 *= PRIME64_2;
                v4_3 *= PRIME64_2;

                v4_1 = rol31(v4_1);
                v4_2 = rol31(v4_2);
                v4_3 = rol31(v4_3);

                v4_1 *= PRIME64_1;
                v4_2 *= PRIME64_1;
                v4_3 *= PRIME64_1;

                h1_64 ^= v4_1;
                h2_64 ^= v4_2;
                h3_64 ^= v4_3;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h1_64 = s1 + PRIME64_5;
                h2_64 = s2 + PRIME64_5;
                h3_64 = s3 + PRIME64_5;
            }

            h1_64 += (ulong)count;
            h2_64 += (ulong)count;
            h3_64 += (ulong)count;

            while (input + 8 <= bEnd)
            {
                ulong k1_1 = *((ulong*)input);
                ulong k1_2 = *((ulong*)input);
                ulong k1_3 = *((ulong*)input);

                k1_1 *= PRIME64_2;
                k1_2 *= PRIME64_2;
                k1_3 *= PRIME64_2;

                k1_1 = rol31(k1_1);
                k1_2 = rol31(k1_2);
                k1_3 = rol31(k1_3);

                k1_1 *= PRIME64_1;
                k1_2 *= PRIME64_1;
                k1_3 *= PRIME64_1;

                h1_64 ^= k1_1;
                h2_64 ^= k1_2;
                h3_64 ^= k1_3;

                h1_64 = rol27(h1_64) * PRIME64_1 + PRIME64_4;
                h2_64 = rol27(h2_64) * PRIME64_1 + PRIME64_4;
                h3_64 = rol27(h3_64) * PRIME64_1 + PRIME64_4;

                input += 8;
            }

            if (input + 4 <= bEnd)
            {
                h1_64 ^= *(uint*)input * PRIME64_1;
                h2_64 ^= *(uint*)input * PRIME64_1;
                h3_64 ^= *(uint*)input * PRIME64_1;

                h1_64 = rol23(h1_64) * PRIME64_2 + PRIME64_3;
                h2_64 = rol23(h2_64) * PRIME64_2 + PRIME64_3;
                h3_64 = rol23(h3_64) * PRIME64_2 + PRIME64_3;

                input += 4;
            }

            while (input < bEnd)
            {
                h1_64 ^= ((ulong)*input) * PRIME64_5;
                h2_64 ^= ((ulong)*input) * PRIME64_5;
                h3_64 ^= ((ulong)*input) * PRIME64_5;

                h1_64 = rol11(h1_64) * PRIME64_1;
                h2_64 = rol11(h2_64) * PRIME64_1;
                h3_64 = rol11(h3_64) * PRIME64_1;

                input++;
            }

            h1_64 ^= h1_64 >> 33;
            h2_64 ^= h2_64 >> 33;
            h3_64 ^= h3_64 >> 33;

            h1_64 *= PRIME64_2;
            h2_64 *= PRIME64_2;
            h3_64 *= PRIME64_2;

            h1_64 ^= h1_64 >> 29;
            h2_64 ^= h2_64 >> 29;
            h3_64 ^= h3_64 >> 29;

            h1_64 *= PRIME64_3;
            h2_64 *= PRIME64_3;
            h3_64 *= PRIME64_3;

            h1_64 ^= h1_64 >> 32;
            h2_64 ^= h2_64 >> 32;
            h3_64 ^= h3_64 >> 32;

            r1 = h1_64;
            r2 = h2_64;
            r3 = h3_64;
        }

        public static unsafe void Hash(byte* input, int count, ref ulong r1, ref ulong r2, ref ulong r3,ref ulong r4, ulong s1 = 0, ulong s2 = 1, ulong s3 = 2,ulong s4=3)
        {
            ulong h1_64;
            ulong h2_64;
            ulong h3_64;
            ulong h4_64;
            byte* bEnd = input + count;

            if (count >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1_1 = s1 + PRIME64_1 + PRIME64_2;
                ulong v2_1 = s1 + PRIME64_2;
                ulong v3_1 = s1 + 0;
                ulong v4_1 = s1 - PRIME64_1;

                ulong v1_2 = s2 + PRIME64_1 + PRIME64_2;
                ulong v2_2 = s2 + PRIME64_2;
                ulong v3_2 = s2 + 0;
                ulong v4_2 = s2 - PRIME64_1;

                ulong v1_3 = s3 + PRIME64_1 + PRIME64_2;
                ulong v2_3 = s3 + PRIME64_2;
                ulong v3_3 = s3 + 0;
                ulong v4_3 = s3 - PRIME64_1;

                ulong v1_4 = s4 + PRIME64_1 + PRIME64_2;
                ulong v2_4 = s4 + PRIME64_2;
                ulong v3_4 = s4 + 0;
                ulong v4_4 = s4 - PRIME64_1;

                do
                {
                    v1_1 += *((ulong*)input) * PRIME64_2;
                    v1_2 += *((ulong*)input) * PRIME64_2;
                    v1_3 += *((ulong*)input) * PRIME64_2;
                    v1_4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v2_1 += *((ulong*)input) * PRIME64_2;
                    v2_2 += *((ulong*)input) * PRIME64_2;
                    v2_3 += *((ulong*)input) * PRIME64_2;
                    v2_4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v3_1 += *((ulong*)input) * PRIME64_2;
                    v3_2 += *((ulong*)input) * PRIME64_2;
                    v3_3 += *((ulong*)input) * PRIME64_2;
                    v3_4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v4_1 += *((ulong*)input) * PRIME64_2;
                    v4_2 += *((ulong*)input) * PRIME64_2;
                    v4_3 += *((ulong*)input) * PRIME64_2;
                    v4_4 += *((ulong*)input) * PRIME64_2;
                    input += sizeof(ulong);

                    v1_1 = rol31(v1_1);
                    v1_2 = rol31(v1_2);
                    v1_3 = rol31(v1_3);
                    v1_4 = rol31(v1_4);

                    v2_1 = rol31(v2_1);
                    v2_2 = rol31(v2_2);
                    v2_3 = rol31(v2_3);
                    v2_4 = rol31(v2_4);

                    v3_1 = rol31(v3_1);
                    v3_2 = rol31(v3_2);
                    v3_3 = rol31(v3_3);
                    v3_4 = rol31(v3_4);

                    v4_1 = rol31(v4_1);
                    v4_2 = rol31(v4_2);
                    v4_3 = rol31(v4_3);
                    v4_4 = rol31(v4_4);

                    v1_1 *= PRIME64_1;
                    v1_2 *= PRIME64_1;
                    v1_3 *= PRIME64_1;
                    v1_4 *= PRIME64_1;

                    v2_1 *= PRIME64_1;
                    v2_2 *= PRIME64_1;
                    v2_3 *= PRIME64_1;
                    v2_4 *= PRIME64_1;

                    v3_1 *= PRIME64_1;
                    v3_2 *= PRIME64_1;
                    v3_3 *= PRIME64_1;
                    v3_4 *= PRIME64_1;

                    v4_1 *= PRIME64_1;
                    v4_2 *= PRIME64_1;
                    v4_3 *= PRIME64_1;
                    v4_4 *= PRIME64_1;
                }
                while (input <= limit);

                h1_64 = rol1(v1_1) + rol7(v2_1) + rol12(v3_1) + rol18(v4_1);
                h2_64 = rol1(v1_2) + rol7(v2_2) + rol12(v3_2) + rol18(v4_2);
                h3_64 = rol1(v1_3) + rol7(v2_3) + rol12(v3_3) + rol18(v4_3);
                h4_64 = rol1(v1_4) + rol7(v2_4) + rol12(v3_4) + rol18(v4_4);

                v1_1 *= PRIME64_2;
                v1_2 *= PRIME64_2;
                v1_3 *= PRIME64_2;
                v1_4 *= PRIME64_2;

                v1_1 = rol31(v1_1);
                v1_2 = rol31(v1_2);
                v1_3 = rol31(v1_3);
                v1_4 = rol31(v1_4);

                v1_1 *= PRIME64_1;
                v1_2 *= PRIME64_1;
                v1_3 *= PRIME64_1;
                v1_4 *= PRIME64_1;

                h1_64 ^= v1_1;
                h2_64 ^= v1_2;
                h3_64 ^= v1_3;
                h4_64 ^= v1_4;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;
                h4_64 = h4_64 * PRIME64_1 + PRIME64_4;

                v2_1 *= PRIME64_2;
                v2_2 *= PRIME64_2;
                v2_3 *= PRIME64_2;
                v2_4 *= PRIME64_2;

                v2_1 = rol31(v2_1);
                v2_2 = rol31(v2_2);
                v2_3 = rol31(v2_3);
                v2_4 = rol31(v2_4);

                v2_1 *= PRIME64_1;
                v2_2 *= PRIME64_1;
                v2_3 *= PRIME64_1;
                v2_4 *= PRIME64_1;

                h1_64 ^= v2_1;
                h2_64 ^= v2_2;
                h3_64 ^= v2_3;
                h4_64 ^= v2_4;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;
                h4_64 = h4_64 * PRIME64_1 + PRIME64_4;

                v3_1 *= PRIME64_2;
                v3_2 *= PRIME64_2;
                v3_3 *= PRIME64_2;
                v3_4 *= PRIME64_2;

                v3_1 = rol31(v3_1);
                v3_2 = rol31(v3_2);
                v3_3 = rol31(v3_3);
                v3_4 = rol31(v3_4);

                v3_1 *= PRIME64_1;
                v3_2 *= PRIME64_1;
                v3_3 *= PRIME64_1;
                v3_4 *= PRIME64_1;

                h1_64 ^= v3_1;
                h2_64 ^= v3_2;
                h3_64 ^= v3_3;
                h4_64 ^= v3_4;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;
                h4_64 = h4_64 * PRIME64_1 + PRIME64_4;

                v4_1 *= PRIME64_2;
                v4_2 *= PRIME64_2;
                v4_3 *= PRIME64_2;
                v4_4 *= PRIME64_2;

                v4_1 = rol31(v4_1);
                v4_2 = rol31(v4_2);
                v4_3 = rol31(v4_3);
                v4_4 = rol31(v4_4);

                v4_1 *= PRIME64_1;
                v4_2 *= PRIME64_1;
                v4_3 *= PRIME64_1;
                v4_4 *= PRIME64_1;

                h1_64 ^= v4_1;
                h2_64 ^= v4_2;
                h3_64 ^= v4_3;
                h4_64 ^= v4_4;
                h1_64 = h1_64 * PRIME64_1 + PRIME64_4;
                h2_64 = h2_64 * PRIME64_1 + PRIME64_4;
                h3_64 = h3_64 * PRIME64_1 + PRIME64_4;
                h4_64 = h4_64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h1_64 = s1 + PRIME64_5;
                h2_64 = s2 + PRIME64_5;
                h3_64 = s3 + PRIME64_5;
                h4_64 = s4 + PRIME64_5;
            }

            h1_64 += (ulong)count;
            h2_64 += (ulong)count;
            h3_64 += (ulong)count;
            h4_64 += (ulong)count;

            while (input + 8 <= bEnd)
            {
                ulong k1_1 = *((ulong*)input);
                ulong k1_2 = *((ulong*)input);
                ulong k1_3 = *((ulong*)input);
                ulong k1_4 = *((ulong*)input);

                k1_1 *= PRIME64_2;
                k1_2 *= PRIME64_2;
                k1_3 *= PRIME64_2;
                k1_4 *= PRIME64_2;

                k1_1 = rol31(k1_1);
                k1_2 = rol31(k1_2);
                k1_3 = rol31(k1_3);
                k1_4 = rol31(k1_4);

                k1_1 *= PRIME64_1;
                k1_2 *= PRIME64_1;
                k1_3 *= PRIME64_1;
                k1_4 *= PRIME64_1;

                h1_64 ^= k1_1;
                h2_64 ^= k1_2;
                h3_64 ^= k1_3;
                h4_64 ^= k1_4;

                h1_64 = rol27(h1_64) * PRIME64_1 + PRIME64_4;
                h2_64 = rol27(h2_64) * PRIME64_1 + PRIME64_4;
                h3_64 = rol27(h3_64) * PRIME64_1 + PRIME64_4;
                h4_64 = rol27(h4_64) * PRIME64_1 + PRIME64_4;

                input += 8;
            }

            if (input + 4 <= bEnd)
            {
                h1_64 ^= *(uint*)input * PRIME64_1;
                h2_64 ^= *(uint*)input * PRIME64_1;
                h3_64 ^= *(uint*)input * PRIME64_1;
                h4_64 ^= *(uint*)input * PRIME64_1;

                h1_64 = rol23(h1_64) * PRIME64_2 + PRIME64_3;
                h2_64 = rol23(h2_64) * PRIME64_2 + PRIME64_3;
                h3_64 = rol23(h3_64) * PRIME64_2 + PRIME64_3;
                h4_64 = rol23(h4_64) * PRIME64_2 + PRIME64_3;

                input += 4;
            }

            while (input < bEnd)
            {
                h1_64 ^= ((ulong)*input) * PRIME64_5;
                h2_64 ^= ((ulong)*input) * PRIME64_5;
                h3_64 ^= ((ulong)*input) * PRIME64_5;
                h4_64 ^= ((ulong)*input) * PRIME64_5;

                h1_64 = rol11(h1_64) * PRIME64_1;
                h2_64 = rol11(h2_64) * PRIME64_1;
                h3_64 = rol11(h3_64) * PRIME64_1;
                h4_64 = rol11(h4_64) * PRIME64_1;

                input++;
            }

            h1_64 ^= h1_64 >> 33;
            h2_64 ^= h2_64 >> 33;
            h3_64 ^= h3_64 >> 33;
            h4_64 ^= h4_64 >> 33;

            h1_64 *= PRIME64_2;
            h2_64 *= PRIME64_2;
            h3_64 *= PRIME64_2;
            h4_64 *= PRIME64_2;

            h1_64 ^= h1_64 >> 29;
            h2_64 ^= h2_64 >> 29;
            h3_64 ^= h3_64 >> 29;
            h4_64 ^= h4_64 >> 29;

            h1_64 *= PRIME64_3;
            h2_64 *= PRIME64_3;
            h3_64 *= PRIME64_3;
            h4_64 *= PRIME64_3;

            h1_64 ^= h1_64 >> 32;
            h2_64 ^= h2_64 >> 32;
            h3_64 ^= h3_64 >> 32;
            h4_64 ^= h4_64 >> 32;

            r1 = h1_64;
            r2 = h2_64;
            r3 = h3_64;
            r4 = h4_64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol1(ulong x) { return (x << 1) | (x >> (64 - 1)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol7(ulong x) { return (x << 7) | (x >> (64 - 7)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol11(ulong x) { return (x << 11) | (x >> (64 - 11)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol12(ulong x) { return (x << 12) | (x >> (64 - 12)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol18(ulong x) { return (x << 18) | (x >> (64 - 18)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol23(ulong x) { return (x << 23) | (x >> (64 - 23)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol27(ulong x) { return (x << 27) | (x >> (64 - 27)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rol31(ulong x) { return (x << 31) | (x >> (64 - 31)); }

    }
}
