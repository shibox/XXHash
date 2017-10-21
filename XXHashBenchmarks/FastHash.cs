using System;
using System.Collections.Generic;
using System.Text;

namespace XXHashBenchmarks
{
    public class FastHash
    {
        public unsafe static ulong Hash(byte[] bytes)
        {
            fixed (byte* pd = &bytes[0])
                return Hash(pd, bytes.Length);
        }

        public unsafe static ulong Hash(byte* src, int count)
        {
            switch (count)
            {
                case 0:
                    return 0;
                case 1:
                    return *src;
                case 2:
                    return *(ushort*)src;
                case 3:
                    return (*(uint*)src) >> 8;
                case 4:
                    return *(uint*)src;
                case 5:
                    return (*(ulong*)src) >> 24;
                case 6:
                    return (*(ulong*)src) >> 16;
                case 7:
                    return (*(ulong*)src) >> 8;
                case 8:
                    return *(ulong*)src;

                case 9:
                    return *(ulong*)src | *(src + 8);
                case 10:
                    return *(ulong*)src | (ulong)(*(ushort*)(src + 8));
                case 11:
                    return *(ulong*)src | (ulong)((*(uint*)(src + 8)) >> 8);
                case 12:
                    return *(ulong*)src | (ulong)(*(uint*)(src + 8));
                case 13:
                    return *(ulong*)src | ((*(ulong*)(src + 8)) >> 24);
                case 14:
                    return *(ulong*)src | ((*(ulong*)(src + 8)) >> 16);
                case 15:
                    return *(ulong*)src | ((*(ulong*)(src + 8)) >> 8);
                case 16:
                    return *(ulong*)src | *(ulong*)(src + 8);

                case 17:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(src + 16);
                case 18:
                    return *(ulong*)src | *(ulong*)(src + 8) | (ulong)(*(ushort*)(src + 16));
                case 19:
                    return *(ulong*)src | *(ulong*)(src + 8) | (ulong)((*(uint*)(src + 16)) >> 8);
                case 20:
                    return *(ulong*)src | *(ulong*)(src + 8) | (ulong)(*(uint*)(src + 16));
                case 21:
                    return *(ulong*)src | *(ulong*)(src + 8) | ((*(ulong*)(src + 16)) >> 24);
                case 22:
                    return *(ulong*)src | *(ulong*)(src + 8) | ((*(ulong*)(src + 16)) >> 16);
                case 23:
                    return *(ulong*)src | *(ulong*)(src + 8) | ((*(ulong*)(src + 16)) >> 8);
                case 24:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16);

                case 25:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | *(src + 24);
                case 26:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | (ulong)(*(ushort*)(src + 24));
                case 27:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | (ulong)((*(uint*)(src + 24)) >> 8);
                case 28:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | (ulong)(*(uint*)(src + 24));
                case 29:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | ((*(ulong*)(src + 24)) >> 24);
                case 30:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | ((*(ulong*)(src + 24)) >> 16);
                case 31:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | ((*(ulong*)(src + 24)) >> 8);
                case 32:
                    return *(ulong*)src | *(ulong*)(src + 8) | *(ulong*)(src + 16) | *(ulong*)(src + 24);
                default:
                    return XXHash.XXHash64.Hash(src, count, 0);
            }
        }

    }
}
