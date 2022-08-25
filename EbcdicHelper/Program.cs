using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbcdicHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter some text.");
            string text = Console.ReadLine();

            byte[] ascii = Encoding.ASCII.GetBytes(text); // 轉成ascii
            byte[] ebcdic = EbcdicHelper.ConvertAsciiToEbcdic(ascii); // 轉成ebcdic
            string hexValue = BitConverter.ToString(ebcdic).Replace("-", ""); // 輸出16進位數值字串

            Console.WriteLine(hexValue);
        }
    }
}
