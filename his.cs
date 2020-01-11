using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EncryptDecryptTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: EncryptDecryptTool <mode> <string>");
                Console.WriteLine("Modes: encrypt or decrypt");
                return;
            }

            string mode = args[0].ToLower();
            string inputString = args[1];

            if (mode == "encrypt")
            {
                string encryptedString = Encrypt(inputString);
                Console.WriteLine("Encrypted string: " + encryptedString);
            }
            else if (mode == "decrypt")
            {
                string decryptedString = Decrypt(inputString);
                Console.WriteLine("Decrypted string: " + decryptedString);
            }
            else
            {
                Console.WriteLine("Invalid mode. Use 'encrypt' or 'decrypt'.");
            }
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            // Step 1: Convert each character to hexadecimal and then to binary
            string binaryString = "";
            foreach (char c in plainText)
            {
                binaryString += Convert.ToString(c, 16).PadLeft(4, '0');
            }

            // Step 2: Apply transformation logic similar to Decrypt
            // This part should reverse the steps from Decrypt to "obfuscate" the data

            string transformedBinary = ""; // Placeholder for transformation logic (reverse of Decrypt)

            // Step 3: Add ending "A" or any other marker as needed
            string encryptedString = transformedBinary + "A"; // Placeholder ending based on Decrypt structure

            return encryptedString;
        }

        public static string Decrypt(string strDataSource)
        {
            if (string.IsNullOrEmpty(strDataSource.Trim()))
                return null;

            string str1 = strDataSource.Trim().Substring(0, strDataSource.LastIndexOf("A"));
            string str2 = "";
            for (int startIndex = 0; startIndex < str1.Length; ++startIndex)
            {
                string str3 = "";
                switch (str1.Substring(startIndex, 1))
                {
                    case "E":
                        int int16 = (int)Convert.ToInt16(str1.Substring(startIndex + 1, 1));
                        for (int index = 0; index < int16; ++index)
                            str3 += str1.Substring(startIndex + 2, 1);
                        startIndex += 2;
                        break;
                    case "D":
                        string str4 = str1.Substring(startIndex + 1, 1);
                        str3 = str3 + str4 + str4 + str4 + str4;
                        ++startIndex;
                        break;
                    case "C":
                        string str5 = str1.Substring(startIndex + 1, 1);
                        str3 = str3 + str5 + str5 + str5;
                        ++startIndex;
                        break;
                    case "B":
                        string str6 = str1.Substring(startIndex + 1, 1);
                        str3 = str3 + str6 + str6;
                        ++startIndex;
                        break;
                    default:
                        str3 = str1.Substring(startIndex, 1);
                        break;
                }
                str2 += str3;
            }

            string str7 = str2;
            int length1 = str7.Length;
            string str8 = "";
            for (int startIndex = 0; startIndex < str7.Length; ++startIndex)
            {
                string str9 = str7.Substring(startIndex, 1);
                str8 += Convert.ToString(Convert.ToInt32(str9, 16), 2).PadLeft(4, '0');
            }

            string str10 = str8;
            int num1 = str10.Length / 16;
            int num2 = 16;
            string[] strArray = new string[1600];
            for (int index = 0; index < str10.Length; ++index)
            {
                int num3 = index / num2;
                int num4 = index % num2;
                strArray[num4 * num1 + num3] = (num4 * num1 + num3) % 2 != 1 ? str10.Substring(num3 * num2 + num4, 1) : (str10.Substring(num3 * num2 + num4, 1) == "1" ? "0" : "1");
            }

            string input = string.Join("", strArray);
            string str11 = input.Length.ToString();
            int num5 = 0;
            for (int startIndex = 0; startIndex < str11.Length; ++startIndex)
                num5 += (int)Convert.ToInt16(str11.Substring(startIndex, 1));
            for (int index = num5 - 1; index < input.Length; index += num5)
            {
                int num6 = 0;
                for (int startIndex = index - 1; startIndex > index - num5; --startIndex)
                    num6 += (int)Convert.ToInt16(input.Substring(startIndex, 1));
                if (num6 % 2 == 1)
                {
                    string str12 = input.Substring(index, 1) == "0" ? "1" : "0";
                    input = input.Substring(0, index) + str12 + input.Substring(index + 1);
                }
            }

            string str13 = input.Replace("1", "");
            int num7 = input.Length - str13.Length;
            int length2 = (int)Convert.ToInt16(num7.ToString().Substring(num7.ToString().Length - 1));
            switch (length2)
            {
                case 0:
                    length2 = 7;
                    break;
                case 1:
                    length2 = 13;
                    break;
            }

            for (int index1 = length2 - 1; index1 < input.Length; index1 += length2)
            {
                string str14 = input.Substring(index1 - (length2 - 1), length2);
                string str15 = "";
                for (int index2 = 0; index2 < length2; ++index2)
                    str15 += str14.Substring(length2 - 1 - index2, 1);
                input = input.Substring(0, index1 - (length2 - 1)) + str15 + input.Substring(index1 + 1);
            }

            CaptureCollection captures = Regex.Match(input, "([01]{8})+").Groups[1].Captures;
            byte[] bytes = new byte[captures.Count];
            for (int i = 0; i < captures.Count; ++i)
                bytes[i] = Convert.ToByte(captures[i].Value, 2);
            string str16 = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
            return str16.Substring(0, str16.LastIndexOf("A"));
        }
    }
}
