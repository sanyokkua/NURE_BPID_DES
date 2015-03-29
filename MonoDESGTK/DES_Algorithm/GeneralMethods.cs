using System;
using System.Collections.Generic;
using System.Text;

namespace MonoDESGTK
{
	static class GeneralMethods
	{
		public static byte[] ToBinary (int digit)
		{
			var st = new Stack<int> ();
			var vec = new List<byte> ();
			int tmp = digit;
			if (tmp != 0) {
				while (tmp != 0) {
					st.Push (tmp & 1);
					tmp = tmp >> 1;
				}
				while (st.Count > 0)
					vec.Add (Convert.ToByte (st.Pop ()));
			} else
				vec.Add (0);
			return Align (vec.ToArray (), 8);
		}

		public static byte[] ToBinary (byte[] array)
		{
			var result = new List<byte> ();
			foreach (var i in array)
				result.AddRange (ToBinary (i));
			return result.ToArray ();
		}

		public static byte ToDecimal (byte[] binary)
		{
			int res = 0;
			for (int i = 0, j = binary.Length - 1; i < binary.Length; i++)
				res += binary [j--] * (1 << i);
			return Convert.ToByte (res);
		}

		public static byte[] Merge (byte[] left, byte[] right)
		{
			var result = new byte[left.Length + right.Length];
			byte i = 0;
			for (; i < left.Length; i++)
				result [i] = left [i];
			for (byte j = 0; j < right.Length; j++)
				result [i++] = right [j];
			return result;
		}

		public static byte[] GetSubsequence (byte[] array, int begin, int end)
		{
			if (array.Length == 0 || begin >= end || begin < 0 || end < 0 || begin >= array.Length)
				throw new ArgumentException ("arguments is invalid");
			int length = end - begin;
			var result = new byte[length];
			for (byte i = 0; i < result.Length && begin < end; begin++, i++)
				result [i] = array [begin];
			return result;
		}

		public static byte[] Align (byte[] array, int maxSize)
		{
			if (maxSize < 0)
				throw new ArgumentException ("maxSize is negative");
			if (array.Length > maxSize)
				return DeAlign (array, maxSize);
			var difference = maxSize - array.Length;
			if (difference <= 0)
				return array;
			var result = new List<byte> ();
			for (byte i = 0; i < difference; i++)
				result.Add (0);
			for (byte i = 0; i < array.Length; i++)
				result.Add (array [i]);
			return result.ToArray ();
		}

		public static byte[] DeAlign (byte[] array, int size)
		{
			var result = new byte[size];
			int count = array.Length - 1;
			for (int i = result.Length - 1; i >= 0; i--) {
				result [i] = array [count--];
			}
			return result;
		}

		public static byte[] ConvertBinaryToByteArray (byte[] binary)
		{
			if (binary.Length % 8 != 0)
				throw new ArgumentException ("Can't convert binary array to byte array, because binary.length % 8 != 0");
			const int byteSize = 8;
			var array = new List<byte> ();
			int count = 0;
			var temp = new byte[byteSize];
			foreach (var i in binary) {
				temp [count++] = i;
				if (count == byteSize) {
					count = 0;
					array.Add (ToDecimal (temp));
					temp = new byte[byteSize];
				}
			}
			return array.ToArray ();
		}

		public static byte[] ConvertStringToByteArray (string text)
		{
			return Encoding.Default.GetBytes (text);
		}

		public static string ConvertByteArrayToString (byte[] array)
		{
			return Encoding.Default.GetString (array);
		}
	}
}

