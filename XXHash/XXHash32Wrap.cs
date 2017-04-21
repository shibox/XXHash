// Create By Shibox 


using System.Runtime.InteropServices;
using System;

namespace XXHash
{
    public static partial class XXHash32
    {

			
			public unsafe static ulong Hash(this Byte[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Byte* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Byte),seed);
				}
			}

			public unsafe static ulong Hash(this Byte[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Byte* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Byte),seed);
				}
			}
				
			public unsafe static ulong Hash(this SByte[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (SByte* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(SByte),seed);
				}
			}

			public unsafe static ulong Hash(this SByte[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (SByte* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(SByte),seed);
				}
			}
				
			public unsafe static ulong Hash(this UInt16[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (UInt16* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(UInt16),seed);
				}
			}

			public unsafe static ulong Hash(this UInt16[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (UInt16* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(UInt16),seed);
				}
			}
				
			public unsafe static ulong Hash(this Int16[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Int16* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Int16),seed);
				}
			}

			public unsafe static ulong Hash(this Int16[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Int16* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Int16),seed);
				}
			}
				
			public unsafe static ulong Hash(this UInt32[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (UInt32* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(UInt32),seed);
				}
			}

			public unsafe static ulong Hash(this UInt32[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (UInt32* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(UInt32),seed);
				}
			}
				
			public unsafe static ulong Hash(this Int32[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Int32* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Int32),seed);
				}
			}

			public unsafe static ulong Hash(this Int32[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Int32* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Int32),seed);
				}
			}
				
			public unsafe static ulong Hash(this UInt64[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (UInt64* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(UInt64),seed);
				}
			}

			public unsafe static ulong Hash(this UInt64[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (UInt64* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(UInt64),seed);
				}
			}
				
			public unsafe static ulong Hash(this Int64[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Int64* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Int64),seed);
				}
			}

			public unsafe static ulong Hash(this Int64[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Int64* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Int64),seed);
				}
			}
				
			public unsafe static ulong Hash(this Single[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Single* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Single),seed);
				}
			}

			public unsafe static ulong Hash(this Single[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Single* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Single),seed);
				}
			}
				
			public unsafe static ulong Hash(this Double[] input, int offset, int count, uint seed = 0)
			{
				if(input == null || input.Length == 0 || count == 0)
					return 0;
				fixed (Double* @in = &input[offset])
				{
					return Hash((byte*)@in, count * sizeof(Double),seed);
				}
			}

			public unsafe static ulong Hash(this Double[] input, uint seed = 0)
			{
				if(input == null || input.Length == 0)
					return 0;
				fixed (Double* @in = &input[0])
				{
					return Hash((byte*)@in, input.Length * sizeof(Double),seed);
				}
			}
		
    }
}