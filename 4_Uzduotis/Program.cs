using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace _4_Uzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = ReadFile("Duomenys.txt");
            remoweWord(ref text, "tada");
            WriteFile("Rezultatai.txt", text);
        }
        public static string[] ReadFile(string fName)
        {
            string[] line = File.ReadAllLines(@fName, Encoding.UTF8);
            return line;
        }
        public static void WriteFile(string fName, string[] text)
        {
            var results = File.CreateText(fName);
            foreach (string line in text)
                results.WriteLine(line);
            results.Close();
        }

        public static void remoweWord(ref string[] text, string word)
        {
            for (int i = 0; i < text.Length; i++)
            {
                List<string> badWordList = new List<string>();
                bool falseWord = false;
                for (int j = 0; j <= text[i].Length - word.Length; j++)
                {
                    string temp = word;
                    falseWord = false;
                    for (int k = 0; k < word.Length && !falseWord; k++)
                    {
                        if (text[i][j + k] != word[k])
                            falseWord = true;
                    }
                    if (j + word.Length < text[i].Length && char.IsLetterOrDigit(text[i][j + word.Length]))
                        falseWord = true;
                    if (!falseWord)
                    {
                        j += word.Length;
                        while (j < text[i].Length && (char.IsPunctuation(text[i][j]) || text[i][j] == ' '))
                        {
                            temp += text[i][j];
                            j++;
                        }
                        int begin = j - temp.Length;
                        //Printing to console.
                        for (int x = 0; x < text[i].Length; x++)
                        {
                            if (x >= begin && x < j)
                                Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(text[i][x]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        //Adding that naughty word
                        badWordList.Add(temp);
                        Console.WriteLine();
                    }
                }
                foreach(string badWord in badWordList)
                {
                    Console.WriteLine(badWord);
                    text[i] = text[i].Replace(badWord,"");
                }
            }
        }
    }
}
