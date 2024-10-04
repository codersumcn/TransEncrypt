using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DecryptTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DecryptTool <encrypted_string>");
                return;
            }

            string encryptedString = args[0];
            string decryptedString = Decrypt(encryptedString);
            Console.WriteLine("Decrypted string: " + decryptedString);
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
