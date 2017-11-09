using System;
using System.Runtime.InteropServices;
using System.Text;

/* The MIT License (MIT) Copyright(c)
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
* documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights 
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
* to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in  
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
* NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
* DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

public static class IniFileHelper
{
	public static int capacity = 512;


	[DllImport("kernel32", CharSet = CharSet.Unicode)]
	private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	static extern int GetPrivateProfileString(string section, string key, string defaultValue, [In, Out] char[] value, int size, string filePath);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	private static extern int GetPrivateProfileSection(string section, IntPtr keyValue, int size, string filePath);

	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);

	public static bool WriteValue(string section, string key, string value, string filePath)
	{
		bool result = WritePrivateProfileString(section, key, value, filePath);
		return result;
	}

	public static bool DeleteSection(string section, string filepath)
	{
		bool result = WritePrivateProfileString(section, null, null, filepath);
		return result;
	}

	public static bool DeleteKey(string section, string key, string filepath)
	{
		bool result = WritePrivateProfileString(section, key, null, filepath);
		return result;
	}

	public static string ReadValue(string section, string key, string filePath, string defaultValue = "")
	{
		var value = new StringBuilder(capacity);
		GetPrivateProfileString(section, key, defaultValue, value, value.Capacity, filePath);
		return value.ToString();
	}

	public static string[] ReadSections(string filePath)
	{
		// first line will not recognize if ini file is saved in UTF-8 with BOM
		while (true)
		{
			char[] chars = new char[capacity];
			int size = GetPrivateProfileString(null, null, "", chars, capacity, filePath);

			if (size == 0)
			{
				return null;
			}

			if (size < capacity - 2)
			{
				string result = new String(chars, 0, size);
				string[] sections = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
				return sections;
			}

			capacity = capacity * 2;
		}
	}

	public static string[] ReadKeys(string section, string filePath)
	{
		// first line will not recognize if ini file is saved in UTF-8 with BOM
		while (true)
		{
			char[] chars = new char[capacity];
			int size = GetPrivateProfileString(section, null, "", chars, capacity, filePath);

			if (size == 0)
			{
				return null;
			}

			if (size < capacity - 2)
			{
				string result = new String(chars, 0, size);
				string[] keys = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
				return keys;
			}

			capacity = capacity * 2;
		}
	}

	public static string[] ReadKeyValuePairs(string section, string filePath)
	{
		while (true)
		{
			IntPtr returnedString = Marshal.AllocCoTaskMem(capacity * sizeof(char));
			int size = GetPrivateProfileSection(section, returnedString, capacity, filePath);

			if (size == 0)
			{
				Marshal.FreeCoTaskMem(returnedString);
				return null;
			}

			if (size < capacity - 2)
			{
				string result = Marshal.PtrToStringAuto(returnedString, size - 1);
				Marshal.FreeCoTaskMem(returnedString);
				string[] keyValuePairs = result.Split('\0');
				return keyValuePairs;
			}

			Marshal.FreeCoTaskMem(returnedString);
			capacity = capacity * 2;
		}
	}
}
