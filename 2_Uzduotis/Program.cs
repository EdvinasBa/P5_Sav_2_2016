using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2_Uzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = ReadFile("Duomenys.txt");
            RemoveLongComment(ref text);
            RemoveRegularComments(ref text);
            PrintToConsole(text);
            Console.ReadLine();
        }
        public static string[] ReadFile(string fName)
        {
            string[] line = File.ReadAllLines(@fName);
            return line; 
        }
        public static void RemoveRegularComments(ref string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != null)
                {
                    string line = text[i];
                    for (int j = 0; j < line.Length - 1; j++)
                        if (line[j] == '/' && line[j + 1] == '/')
                        {
                            text[i] = line.Remove(j);
                            break;
                        }
                }
            }
        }
        public static void RemoveLongComment(ref string[] text)
        {
            bool inComment = false;
            for (int i = 0; i < text.Length; i++)
            {
                string line = text[i];

                int startLineIndex = 0;
                int startIndex = 0;

                int endLineIndex = 0;
                int endIndex = 0;

                for (int j = 0; j < line.Length-1; j++)
                    if (!inComment)
                    {
                        if (line[j] == '/' && line[j + 1] == '*')
                        {
                            startLineIndex = i;
                            startIndex = j;
                            text[i] = line.Remove(startIndex);
                            inComment = true;
                        }
                    }
                    else
                    {
                        if (line[j] == '*' && line[j + 1] == '/')
                        {
                            endLineIndex = i;
                            endIndex = j;
                            inComment = false;
                        }
                        else
                            text[i] = null;
                    }
            }
        }
        public static void PrintToConsole(string[] text)
        {
            foreach(string line in text)
            {
                Console.WriteLine(line);
            }
        }
    }
}
