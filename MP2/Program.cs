//Author Name:Elad Perlman
//Project Name: MP2
//File Name: Program.cs
//Date Created: Nov, 21st, 2020
//Date Modified: Nov, 29th, 2020
//Description: This program recieves a file with multiple lines, then checks if each line classifies as a valid nerd word and writes back to a result file the result of each
//line and how many valid nerd words exist.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MP2
{
    class Program
    {
        private static Stopwatch timer = new Stopwatch();

        private static StreamReader outFile;
        private static StreamWriter inFile = File.CreateText("Perlman_Elad.txt");

        private static List<string> cheatCodes = new List<string>();
        
        private static bool showResults;
        private static bool isValid;
        private static int validWords = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Would you like to displays results?\nENTER y to display or any other input to not display: ");
            showResults = Console.ReadLine() == "y";

            timer.Reset();
            timer.Start();

            try
            {
                outFile = File.OpenText("input.txt"); 

                while (!outFile.EndOfStream)
                {
                    cheatCodes.Add(outFile.ReadLine());
                    isValid = NerdWordCheck(cheatCodes[cheatCodes.Count - 1]);
                    validWords = isValid ? validWords + 1 : validWords;
                    inFile.WriteLine(cheatCodes[cheatCodes.Count - 1] += isValid ? ":YES" : ":NO");

                    if (showResults)
                    {
                        Console.WriteLine(cheatCodes[cheatCodes.Count - 1]);
                    }
                }

                outFile.Close();
                inFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            timer.Stop();

            Console.WriteLine("Valid Nerd-Words: " + validWords);
            Console.WriteLine("Total Runtime: " + GetTimeOutput(timer));

            Console.ReadLine();
        }

        private static bool NerdWordCheck(string word)
        {
            if (word == "X") 
            {
                return true;
            }

            int aLoc = -1;

            for (int i = 0; i < word.Length; ++i)
            {
                switch (word[i])
                {
                    case 'A':
                        aLoc = i;
                        break;
                    case 'B' when aLoc >= 0:
                        return NerdWordCheck(word.Substring(aLoc + 1, i - aLoc - 1)) && NerdWordCheck(word.Substring(0, aLoc) + "X" + word.Substring(i + 1));
                    case 'Y' when aLoc == -1:
                        return NerdWordCheck(word.Substring(0, i)) && NerdWordCheck(word.Substring(i + 1));
                }
            }

            return false;
        }

        public static string GetTimeOutput(Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            int millis = ts.Milliseconds;
            int seconds = ts.Seconds;
            int minutes = ts.Minutes;
            int hours = ts.Hours;
            int days = ts.Days;

            return "Time- Days:Hours:Minutes:Seconds.Milliseconds:" + days + ":" + hours + ":" + minutes + ":" + seconds + "." + millis;
        }
    }
}
