using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XXHash
{
    public static class XXHash32
    {
        private const uint PRIME32_1 = 2654435761U;
        private const uint PRIME32_2 = 2246822519U;
        private const uint PRIME32_3 = 3266489917U;
        private const uint PRIME32_4 = 668265263U;
        private const uint PRIME32_5 = 374761393U;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint CalculateInline(byte* buffer, int len, uint seed = 0)
        {
            unchecked
            {
                uint h32;

                byte* bEnd = buffer + len;

                if (len >= 16)
                {
                    byte* limit = bEnd - 16;

                    uint v1 = seed + PRIME32_1 + PRIME32_2;
                    uint v2 = seed + PRIME32_2;
                    uint v3 = seed + 0;
                    uint v4 = seed - PRIME32_1;

                    do
                    {
                        v1 += *((uint*)buffer) * PRIME32_2;
                        buffer += sizeof(uint);
                        v2 += *((uint*)buffer) * PRIME32_2;
                        buffer += sizeof(uint);
                        v3 += *((uint*)buffer) * PRIME32_2;
                        buffer += sizeof(uint);
                        v4 += *((uint*)buffer) * PRIME32_2;
                        buffer += sizeof(uint);

                        v1 = rol13(v1);
                        v2 = rol13(v2);
                        v3 = rol13(v3);
                        v4 = rol13(v4);

                        v1 *= PRIME32_1;
                        v2 *= PRIME32_1;
                        v3 *= PRIME32_1;
                        v4 *= PRIME32_1;
                    }
                    while (buffer <= limit);

                    h32 = rol1(v1) + rol7(v2) + rol12(v3) + rol18(v4);
                }
                else
                {
                    h32 = seed + PRIME32_5;
                }

                h32 += (uint)len;


                while (buffer + 4 <= bEnd)
                {
                    h32 += *((uint*)buffer) * PRIME32_3;
                    h32 = rol17(h32) * PRIME32_4;
                    buffer += 4;
                }

                while (buffer < bEnd)
                {
                    h32 += (uint)(*buffer) * PRIME32_5;
                    h32 = rol11(h32) * PRIME32_1;
                    buffer++;
                }

                h32 ^= h32 >> 15;
                h32 *= PRIME32_2;
                h32 ^= h32 >> 13;
                h32 *= PRIME32_3;
                h32 ^= h32 >> 16;

                return h32;
            }
        }

        public static unsafe uint Calculate(byte* buffer, int len, uint seed = 0)
        {
            return CalculateInline(buffer, len, seed);
        }

        public unsafe static uint Calculate(string value, Encoding encoder, uint seed = 0)
        {
            var buf = encoder.GetBytes(value);

            fixed (byte* buffer = buf)
            {
                return CalculateInline(buffer, buf.Length, seed);
            }
        }

        public unsafe static uint CalculateRaw(string buf, uint seed = 0)
        {
            fixed (char* buffer = buf)
            {
                return CalculateInline((byte*)buffer, buf.Length * sizeof(char), seed);
            }
        }

        public unsafe static uint Calculate(byte[] buf, int len = -1, uint seed = 0)
        {
            if (len == -1)
                len = buf.Length;

            fixed (byte* buffer = buf)
            {
                return CalculateInline(buffer, len, seed);
            }
        }

        public unsafe static uint Calculate(int[] buf, int len = -1, uint seed = 0)
        {
            if (len == -1)
                len = buf.Length;

            fixed (int* buffer = buf)
            {
                return Calculate((byte*)buffer, len * sizeof(int), seed);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol1(uint x) { return (x << 1) | (x >> (32 - 1)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol7(uint x) { return (x << 7) | (x >> (32 - 7)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol11(uint x) { return (x << 11) | (x >> (32 - 11)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol12(uint x) { return (x << 12) | (x >> (32 - 12)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol18(uint x) { return (x << 18) | (x >> (32 - 18)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol13(uint x) { return (x << 13) | (x >> (32 - 13)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint rol17(uint x) { return (x << 17) | (x >> (32 - 17)); }

    }


}
