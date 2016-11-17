using System;
using System.Text;
using System.IO;

namespace _2_Uzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = ReadFile("Duomenys.txt");
            string[] original = new string[text.Length];

            Array.Copy(text, original, text.Length);

            RemoveLongComment(ref text);
            RemoveRegularComments(ref text);

            PrintAnalysisToConsole(original, text);
            ResultsToFile("Rezultatai.txt", text);

            Console.ReadLine();
        }
        public static string[] ReadFile(string fName)
        {
            string[] line = File.ReadAllLines(@fName, Encoding.UTF8);
            return line;
        }
        public static void ResultsToFile(string fName, string[] text)
        {
            using (StreamWriter writer = new StreamWriter(fName))
            {
                foreach (string line in text)
                    if (!string.IsNullOrWhiteSpace(line))
                        writer.WriteLine(line);
            }
        }

        public static void RemoveRegularComments(ref string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string line = text[i];
                if (!string.IsNullOrWhiteSpace(text[i]))
                {
                    for (int j = 0; j < line.Length - 1; j++)
                        if (line[j] == '/' && line[j + 1] == '/')
                        {
                            text[i] = line.Remove(j);
                            break;
                        }
                }
            }
            //RemoveNullStrings(ref text);
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

                if (!string.IsNullOrWhiteSpace(line))
                for (int j = 0; j < line.Length - 1; j++)
                    {
                        if (!inComment)
                        {
                            if (line[j] == '/' && line[j + 1] == '*')
                            {
                                startLineIndex = i;
                                text[i] = line.Remove(j);
                                inComment = true;
                            }
                        }
                        else
                        {
                            if (line[j] == '*' && line[j + 1] == '/')
                            {
                                endLineIndex = i;
                                endIndex = j;
                                text[i] = line.Remove(0, j + 2);
                                inComment = false;
                            }
                            else
                                text[i] = null;
                        }
                    }
            }
            //RemoveNullStrings(ref text);
        }

        public static void RemoveNullStrings(ref string[] text)
        {
            int notNullCount = StringsWithValues(text);
            string[] newText = new string[notNullCount];
            int counter = 0;
            foreach(string line in text)
                if (!string.IsNullOrWhiteSpace(line))
                {
                    newText[counter++] = line;
                }
            text = newText;
        }
        public static int StringsWithValues(string[] text)
        {
            int notNullCount = 0;
            foreach (string line in text)
                if (!string.IsNullOrWhiteSpace(line))
                    notNullCount++;
            return notNullCount;
        }

        public static void PrintToConsole(string[] text)
        {
            foreach(string line in text)
            {
                Console.WriteLine(line);
            }
        }
        public static void PrintAnalysisToConsole(string[] original, string[] text)
        {
            for (int i = 0; i < original.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(text[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(original[i]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                    Console.WriteLine(text[i]);
            }
        }
    }
}
