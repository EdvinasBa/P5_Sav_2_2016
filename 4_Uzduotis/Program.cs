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
            if (args.Length == 0)
                removeWord(ref text, "labas");
            else
                removeWord(ref text, args[0]);
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

        public static void removeWord(ref string[] text, string word)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write("Eilute: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(i);
                Console.ForegroundColor = ConsoleColor.Gray;
                //List of words that should be removed
                List<string> badWordList = new List<string>();
                //If the algorithm finds that given and current "word" matches this is true
                bool falseWord = false;
                for (int j = 0; j <= text[i].Length - word.Length; j++)
                {
                    //Reset everything
                    string temp = word;
                    falseWord = false;
                    for (int k = 0; k < word.Length && !falseWord; k++)
                    {
                        if (text[i][j + k] != word[k])
                            falseWord = true;
                    }
                    //Check if the given word is a part of other word
                    if (j + word.Length < text[i].Length && char.IsLetterOrDigit(text[i][j + word.Length]))
                        falseWord = true;
                    //Add punctuation to bad word
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
                    //Console.WriteLine(badWord);
                    text[i] = text[i].Replace(badWord,"");
                }
            }
        }
    }
}
