using System;
using System.IO;

namespace WordGuessGame
{
    class Program
    {
        static string errorLog = "../../../error_logs.txt";
        static StreamWriter errorLogger = File.AppendText(errorLog);

        static void Main(string[] args)
        {
            string pathText = "../../../random_words.txt";
            Game(pathText);
        }
        static void Game(string path)
        {
            CreateFile(path);
            UpdateFile(path);
            ReadFile(path);
            //DeleteFile(path);
        }
        public static void CreateFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                try
                {
                    streamWriter.WriteLine("Kona is the cutest!");
                }
                catch(Exception e)
                {
                    errorLogger.WriteLine(e);
                    throw;
                }
            }

        }
        public static void ReadFile(string path)
        {
            try
            {
                string[] textLines = File.ReadAllLines(path);
                foreach (string textLine in textLines)
                {
                    Console.WriteLine(textLine);
                }
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(e);
                throw;
            }
        }
        public static void UpdateFile(string path)
        {
            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine("She Likes to snore an night");
                }
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(e);
                throw;
            }
        }
        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(e);
                throw;
            }
        }
    }
}
