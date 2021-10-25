using System;
using System.IO;

namespace DataConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader sr = File.OpenText(@"C:\Users\Wesle\OneDrive\Desktop\A.txt"))
            {
                string s = "";
                int count = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s == "") continue;
                    s = s.Trim('"');
                    ConvertStringWord.Convert(s);
                    count++;
                }
                Console.WriteLine($"Converted {count} sucessfully!");
            }
        }
    }
}
