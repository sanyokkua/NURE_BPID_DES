using System;
using System.Collections.Generic;

namespace MonoDESGTK
{
	class DES
	{
		List<byte[]> roundKeys;
		static readonly byte S_BOX_VECTOR_SIZE = 6;
		static readonly byte BLOCK_SIZE = 64;
		static readonly byte HALF_BLOCK_SIZE = 32;
		static readonly byte NUMBER_OF_ROUNDS = 16;

		public DES ()
		{
			roundKeys = new List<byte[]> ();
		}

		public byte[] Encrypt (byte[] textInBytes, byte[] keyInBytes)
		{
			byte[] binaryText = GeneralMethods.ToBinary (textInBytes);
			byte[] binaryKey = GeneralMethods.ToBinary (keyInBytes);
			MakeKeys (binaryKey);
			var binaryResult = new List<byte> ();
			var blocks = new List<byte[]> ();
			byte bitCount = 0;
			var tempBlock = new byte[BLOCK_SIZE];
			foreach (var bit in binaryText) {
				tempBlock [bitCount++] = bit;
				if (bitCount == BLOCK_SIZE) {
					blocks.Add (tempBlock);
					tempBlock = new byte[BLOCK_SIZE];
					bitCount = 0;
				}
			}
			if (bitCount > 0) {
				for (; bitCount < tempBlock.Length; bitCount++)
					tempBlock [bitCount] = 0;
				blocks.Add (tempBlock);
			}
			foreach (var block in blocks) {
				byte[] encryptedBlock = StartEncrypt (block, binaryKey);
				foreach (var bit in encryptedBlock)
					binaryResult.Add (bit);
			}
			return GeneralMethods.ConvertBinaryToByteArray (binaryResult.ToArray ());
		}

		public byte[] Decrypt (byte[] cipher, byte[] key)
		{
			byte[] binaryText = GeneralMethods.ToBinary (cipher);
			byte[] binaryKey = GeneralMethods.ToBinary (key);
			MakeKeysReverse (binaryKey);
			var result = new List<byte> ();
			var blocks = new List<byte[]> (); 
			byte count = 0;
			var tmp = new byte[BLOCK_SIZE];
			foreach (var i in binaryText) {
				tmp [count++] = i;
				if (count == BLOCK_SIZE) {
					blocks.Add (tmp);
					tmp = new byte[BLOCK_SIZE];
					count = 0;
				}
			}
			if (count > 0) {
				tmp [count++] = 1;
				for (; count < tmp.Length; count++)
					tmp [count] = 0;
				blocks.Add (tmp);
			}
			foreach (var i in blocks) {
				byte[] temp = StartDecrypt (i, binaryKey);
				foreach (var j in temp)
					result.Add (j);
			}
			return GeneralMethods.ConvertBinaryToByteArray (result.ToArray ());
		}

		byte[] StartEncrypt (byte[] binaryText, byte[] binaryKey)
		{
			if (binaryText.Length != BLOCK_SIZE)
				throw new ArgumentException ("block length is not 64 (BLOCK_SIZE)");
			byte[] ipResult = GeneralShuffle (binaryText, IP);
			byte[] Li = GeneralMethods.GetSubsequence (ipResult, 0, ipResult.Length / 2); 
			byte[] Ri = GeneralMethods.GetSubsequence (ipResult, ipResult.Length / 2, ipResult.Length);
			for (byte i = 1; i <= NUMBER_OF_ROUNDS; i++) {
				var temp = XOR (Li, f (Ri, roundKeys [i - 1]));
				Li = Ri;
				Ri = temp;
			}
			return GeneralShuffle (GeneralMethods.Merge (Li, Ri), RIP);
		}

		byte[] StartDecrypt (byte[] cipher, byte[] key)
		{
			if (key == null)
				throw new ArgumentNullException ("key");
			if (cipher.Length != BLOCK_SIZE)
				throw new ArgumentException ("block length is not 64 (BLOCK_SIZE)");
			byte[] ipResult = GeneralShuffle (cipher, IP); 
			byte[] Li = GeneralMethods.GetSubsequence (ipResult, 0, ipResult.Length / 2);
			byte[] Ri = GeneralMethods.GetSubsequence (ipResult, ipResult.Length / 2, ipResult.Length);
			for (byte i = 1; i <= NUMBER_OF_ROUNDS; i++) { 
				var temp = XOR (Ri, f (Li, roundKeys [i - 1]));
				Ri = Li;
				Li = temp;
			}
			return GeneralShuffle (GeneralMethods.Merge (Li, Ri), RIP);
		}

		static byte[] f (byte[] Ri, byte[] ki)
		{
			if (Ri.Length != HALF_BLOCK_SIZE)
				throw new ArgumentException ("Ri size is not correct");
			byte[] extendedRi = GeneralShuffle (Ri, pE);
			extendedRi = XOR (extendedRi, ki);
			var BBoxes = new List<byte[]> ();
			byte bitCount = 0;
			var box = new byte[6];
			foreach (var bit in extendedRi) {
				box [bitCount++] = bit;
				if (bitCount == 6) {
					BBoxes.Add (box);
					box = new byte[6];
					bitCount = 0;
				}
			}
			var S = new List<byte[,]> { S1, S2, S3, S4, S5, S6, S7, S8 };
			var BBoxTouch = new List<byte[]> ();
			byte iteration = 0;
			foreach (var bBox in BBoxes) {
				var temp = GetNumberFromSbox (bBox, S [iteration++]);
				if (temp.Length != 4)
					temp = GeneralMethods.Align (temp, 4);
				BBoxTouch.Add (temp);
			}
			var binaryResult = new List<byte> ();
			foreach (var bBoxTouch in BBoxTouch)
				foreach (var bit in bBoxTouch)
					binaryResult.Add (bit);
			return GeneralShuffle (binaryResult.ToArray (), P);
			;
		}

		void MakeKeys (byte[] key)
		{
			byte[] left = GeneralShuffle (key, C0);
			byte[] right = GeneralShuffle (key, D0);
			for (byte i = 1; i <= NUMBER_OF_ROUNDS; i++) {
				RoundLeftShift (ref left, i);
				RoundLeftShift (ref right, i);
				roundKeys.Add (GeneralShuffle (GeneralMethods.Merge (left, right), P_KEY_COMPRESS));
			}
		}

		void MakeKeysReverse (byte[] key)
		{
			MakeKeys (key);
			roundKeys.Reverse ();
		}

		static byte[] GeneralShuffle (byte[] source, byte[] table)
		{
			var result = new List<byte> ();
			foreach (var tableArg in table)
				if (tableArg - 1 < source.Length)
					result.Add (source [tableArg - 1]);
			return result.ToArray ();
		}

		static byte[] XOR (byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
				throw new ArgumentException ("Length of arguments is different!");
			var result = new List<byte> ();
			for (int i = 0; i < first.Length; i++)
				result.Add (first [i] == second [i] ? (byte)0 : (byte)1);
			return result.ToArray ();
		}

		static byte[] GetNumberFromSbox (byte[] source, byte[,] box)
		{
			var line = GetLineNumberForSbox (source);
			var column = GetColumnNumberForSbox (source);
			return GeneralMethods.ToBinary (box [line, column]);
		}

		static byte GetLineNumberForSbox (byte[] binary)
		{
			if (binary.Length != S_BOX_VECTOR_SIZE)
				throw new ArgumentException ("Array size is not equal 6");
			return GeneralMethods.ToDecimal (new byte[] { binary [0], binary [binary.Length - 1] });
		}

		static byte GetColumnNumberForSbox (byte[] binary)
		{
			if (binary.Length != S_BOX_VECTOR_SIZE)
				throw new ArgumentException ("Array size is not equal 6");
			var binaryNumber = new List<byte> ();
			for (byte beg = 1; beg != binary.Length - 1; beg++)
				binaryNumber.Add (binary [beg]);
			return GeneralMethods.ToDecimal (binaryNumber.ToArray ());
		}

		static void RoundLeftShift (ref byte[] key, byte round)
		{
			if (round < 1 || round > 16)
				throw new ArgumentException ("Number of round is invalid");
			byte shift = SHIFT_LEFT_TABLE [round - 1];
			for (; shift > 0; shift--)
				for (int left = 0, right = left + 1; right < key.Length && left < key.Length - 1; left++, right++)
					Swap (ref key [left], ref key [right]);
		}

		static void Swap (ref byte lhv, ref byte rhv)
		{
			byte tmp = lhv;
			lhv = rhv;
			rhv = tmp;
		}

		static readonly byte[] IP = {
			58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
			62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
			57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
			61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
		};

		static readonly byte[] pE = {
			32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11,
			12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21,
			22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1
		};

		static readonly byte[,] S1 = {
			{ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
			{ 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
			{ 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
			{ 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }
		};

		static readonly byte[,] S2 = {
			{ 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
			{ 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
			{ 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
			{ 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
		};

		static readonly byte[,] S3 = {
			{ 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
			{ 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
			{ 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
			{ 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
		};

		static readonly byte[,] S4 = {
			{ 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
			{ 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
			{ 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
			{ 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
		};

		static readonly byte[,] S5 = {
			{ 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
			{ 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
			{ 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
			{ 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }
		};

		static readonly byte[,] S6 = {
			{ 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
			{ 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
			{ 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
			{ 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
		};

		static readonly byte[,] S7 = {
			{ 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
			{ 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
			{ 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
			{ 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
		};

		static readonly byte[,] S8 = {
			{ 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
			{ 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
			{ 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
			{ 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }
		};

		static readonly byte[] P = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23,
			26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27,
			3, 9, 19, 13, 30, 6, 22, 11, 4, 25
		};

		static readonly byte[] RIP = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
			38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
			36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
			34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25
		};

		static readonly byte[] C0 = {    57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
			42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
			27, 19, 11, 3, 60, 52, 44, 36
		};

		static readonly byte[] D0 = {    63, 55, 47, 39, 31, 23, 15, 7, 62, 54,
			46, 38, 30, 22, 14, 6, 61, 53, 45, 37,
			29, 21, 13, 5, 28, 20, 12, 4
		};

		static readonly byte[] SHIFT_LEFT_TABLE = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

		static readonly byte[] P_KEY_COMPRESS = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4,
			26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40,
			51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
		};
	}
}

