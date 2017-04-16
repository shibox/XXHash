using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XXHash
{
    public static class XXHash64Old
    {
        private static ulong PRIME64_1 = 11400714785074694791UL;
        private static ulong PRIME64_2 = 14029467366897019727UL;
        private static ulong PRIME64_3 = 1609587929392839161UL;
        private static ulong PRIME64_4 = 9650029242287828579UL;
        private static ulong PRIME64_5 = 2870177450012600261UL;
        //private const ulong PRIME64_1 = 11400714785074694791UL;
        //private const ulong PRIME64_2 = 14029467366897019727UL;
        //private const ulong PRIME64_3 = 1609587929392839161UL;
        //private const ulong PRIME64_4 = 9650029242287828579UL;
        //private const ulong PRIME64_5 = 2870177450012600261UL;

        public static unsafe ulong Calculate(byte* buffer, int len, ulong seed = 0)
        {
            ulong h64;

            byte* bEnd = buffer + len;

            if (len >= 32)
            {
                byte* limit = bEnd - 32;

                ulong v1 = seed + PRIME64_1 + PRIME64_2;
                ulong v2 = seed + PRIME64_2;
                ulong v3 = seed + 0;
                ulong v4 = seed - PRIME64_1;

                do
                {
                    v1 += *((ulong*)buffer) * PRIME64_2;
                    buffer += sizeof(ulong);
                    v2 += *((ulong*)buffer) * PRIME64_2;
                    buffer += sizeof(ulong);
                    v3 += *((ulong*)buffer) * PRIME64_2;
                    buffer += sizeof(ulong);
                    v4 += *((ulong*)buffer) * PRIME64_2;
                    buffer += sizeof(ulong);

                    v1 = RotateLeft64(v1, 31);
                    v2 = RotateLeft64(v2, 31);
                    v3 = RotateLeft64(v3, 31);
                    v4 = RotateLeft64(v4, 31);

                    v1 *= PRIME64_1;
                    v2 *= PRIME64_1;
                    v3 *= PRIME64_1;
                    v4 *= PRIME64_1;
                }
                while (buffer <= limit);

                h64 = RotateLeft64(v1, 1) + RotateLeft64(v2, 7) + RotateLeft64(v3, 12) + RotateLeft64(v4, 18);

                v1 *= PRIME64_2;
                v1 = RotateLeft64(v1, 31);
                v1 *= PRIME64_1;
                h64 ^= v1;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v2 *= PRIME64_2;
                v2 = RotateLeft64(v2, 31);
                v2 *= PRIME64_1;
                h64 ^= v2;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v3 *= PRIME64_2;
                v3 = RotateLeft64(v3, 31);
                v3 *= PRIME64_1;
                h64 ^= v3;
                h64 = h64 * PRIME64_1 + PRIME64_4;

                v4 *= PRIME64_2;
                v4 = RotateLeft64(v4, 31);
                v4 *= PRIME64_1;
                h64 ^= v4;
                h64 = h64 * PRIME64_1 + PRIME64_4;
            }
            else
            {
                h64 = seed + PRIME64_5;
            }

            h64 += (ulong)len;


            while (buffer + 8 <= bEnd)
            {
                ulong k1 = *((ulong*)buffer);
                k1 *= PRIME64_2;
                k1 = RotateLeft64(k1, 31);
                k1 *= PRIME64_1;
                h64 ^= k1;
                h64 = RotateLeft64(h64, 27) * PRIME64_1 + PRIME64_4;
                buffer += 8;
            }

            if (buffer + 4 <= bEnd)
            {
                h64 ^= *(uint*)buffer * PRIME64_1;
                h64 = RotateLeft64(h64, 23) * PRIME64_2 + PRIME64_3;
                buffer += 4;
            }

            while (buffer < bEnd)
            {
                h64 ^= ((ulong)*buffer) * PRIME64_5;
                h64 = RotateLeft64(h64, 11) * PRIME64_1;
                buffer++;
            }

            h64 ^= h64 >> 33;
            h64 *= PRIME64_2;
            h64 ^= h64 >> 29;
            h64 *= PRIME64_3;
            h64 ^= h64 >> 32;

            return h64;
        }

        public unsafe static ulong Calculate(string value, Encoding encoder, ulong seed = 0)
        {
            var buf = encoder.GetBytes(value);

            fixed (byte* buffer = buf)
            {
                return Calculate(buffer, buf.Length, seed);
            }
        }
        public unsafe static ulong CalculateRaw(string buf, ulong seed = 0)
        {
            fixed (char* buffer = buf)
            {
                return Calculate((byte*)buffer, buf.Length * sizeof(char), seed);
            }
        }

        public unsafe static ulong Calculate(byte[] buf, int len = -1, ulong seed = 0)
        {
            if (len == -1)
                len = buf.Length;

            fixed (byte* buffer = buf)
            {
                return Calculate(buffer, len, seed);
            }
        }

        public unsafe static ulong Calculate(int[] buf, int len = -1, ulong seed = 0)
        {
            if (len == -1)
                len = buf.Length;

            fixed (int* buffer = buf)
            {
                return Calculate((byte*)buffer, len * sizeof(int), seed);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong RotateLeft64(ulong value, int count)
        {
            return (value << count) | (value >> (64 - count));
        }

    }
}
