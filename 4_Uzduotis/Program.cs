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
            removeWords(ref text, "tada");
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
        public static void removeWords(ref string[] text, string word)
        {
            char[] punctuation = new char[] { ',', '.', '!', '?', ';', ':', '-', '"', '(', ')'};
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                word = word.Trim();
                if (text[i].Contains(word))
                {
                    foreach(char ch in punctuation)
                    {
                        string newWord = word + ch;
                        if (text[i].Contains(newWord))
                        {
                            int wordIndex = text[i].IndexOf(newWord);
                            if (wordIndex != 0)
                            {
                                if (text[i].Length != wordIndex + newWord.Length && text[i].Substring(wordIndex - 1, 1) == " ")
                                    newWord += ' ';
                            }
                            text[i] = text[i].Remove(wordIndex, newWord.Length);
                        }
                    }
                }
            }
        }
    }
}
