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

        public unsafe static ulong GetHashCode(string input, Encoding encoder, ulong seed = 0)
        {
            var buf = encoder.GetBytes(input);
            fixed (byte* buffer = buf)
            {
                return GetHashCode(buffer, buf.Length, seed);
            }
        }

        public unsafe static ulong GetHashCode(string input, ulong seed = 0)
        {
            fixed (char* buffer = input)
            {
                return GetHashCode((byte*)buffer, input.Length * sizeof(char), seed);
            }
        }

        public unsafe static ulong GetHashCode(string input, int offset, int count, ulong seed = 0)
        {
            fixed (char* @in = input)
            {
                char* pd = @in;
                pd += offset;
                return GetHashCode((byte*)pd, count * sizeof(char), seed);
            }
        }

        public unsafe static ulong GetHashCode(this Char[] input, int offset, int count, ulong seed = 0)
        {
            if (input == null || input.Length == 0 || count == 0)
                return 0;
            fixed (Char* @in = &input[offset])
            {
                return GetHashCode((byte*)@in, count * sizeof(Char),seed);
            }
        }

        public unsafe static ulong GetHashCode(this Char[] input, ulong seed = 0)
        {
            if (input == null || input.Length == 0)
                return 0;
            fixed (Char* @in = &input[0])
            {
                return GetHashCode((byte*)@in, input.Length * sizeof(Char),seed);
            }
        }

        public static unsafe ulong GetHashCode(byte* input, int count, ulong seed = 0)
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
