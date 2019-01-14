using System;
using System.IO;

namespace WordGuessGame
{
    class Program
    {
        static string errorLog = "../../../error_logs.txt";
        static StreamWriter errorLogger = File.AppendText(errorLog);
        /// <summary>
        /// Entry point for Program. Calls the Menu.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string pathText = "../../../random_words.txt";
            Menu(pathText);
            Game(pathText);
        }

        /// <summary>
        /// This Method allows the user to input what part of the game they want to use.
        /// </summary>
        /// <param name="path">Path to File with Random Words</param>
        /// <returns> Method calls to adding words, starting game, exiting program</returns>

        static int Menu(string path)
        {
            Console.WriteLine("WORD GUESS");
            Console.WriteLine("1. Add Words\n2. Play Game\n3. Exit");
            string menuChoice = Console.ReadLine();
            string lowerCaseMenuChoice = menuChoice.ToLower();
            switch (lowerCaseMenuChoice)
            {
                case "add words":
                    return AddWords(path);
                case "1":
                    return AddWords(path);
                case "play game":
                    return Game(path);
                case "2":
                    return Game(path);
                case "exit":
                    return Exit();
                default:
                    return Menu(path);
            }
        }
        /// <summary>
        /// Method to Create a text file for the guess game.
        /// User can add more that one word.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Method call to return to the main menu</returns>
        static int AddWords(string path)
        {
            bool done = false;
            Console.WriteLine("What word would to like to add to the game?");
            string inputWordCreate = Console.ReadLine();
            string wordToAddCreate = inputWordCreate.ToLower();
            UpdateFile(path, wordToAddCreate);
            while (done == false)
            {
                Console.WriteLine("\nWould you like to add another word? If you are done hit ENTER.");
                string inputWordUpdate = Console.ReadLine();
                if (inputWordUpdate == "")
                {
                    done = true;
                }
                string wordToAddUpdate = inputWordUpdate.ToLower();
                UpdateFile(path, wordToAddUpdate);
            }
            Console.Clear();
            return Menu(path);
        }
        /// <summary>
        /// Method that runs the game
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Replay method</returns>
        static int Game(string path)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Word Guess!\nThe game where you get to guess a random word.\nI am your host Computer.\nLet's begin!");

            // Get Random Word and its length
            string[] randomWords = ReadFile(path);
            Random random = new Random();
            int randomIndex = random.Next(0, randomWords.Length - 1);
            int randomWordLength = randomWords.Length;

            string randomWord = randomWords[randomIndex].ToLower();
            string lettersGuessed = "";

            // Create Display Word
            string display = "";
            foreach(char letter in randomWord)
            {
                display += "_ ";
            }

            while (display.Contains("_"))
            {
                // Get Letter from User
                Console.WriteLine(display);
                Console.WriteLine($"Guesses [ {lettersGuessed} ]");
                Console.WriteLine("Guess a letter.");
                string userInput = Console.ReadLine();
                if (userInput.Length > 1 )
                {
                    Console.Clear();
                    continue;
                }
                char userGuess = Convert.ToChar(userInput.ToLower());

                if (!lettersGuessed.Contains(userGuess))
                {
                    lettersGuessed += $"{userGuess} ";
                }
                
                display = UpdateDisplay(display, randomWord, userGuess);
                Console.Clear();
            }
            Console.WriteLine(display);
            Console.WriteLine("Thank you for playing!");
            Console.Read();
            return Replay(path);
        }
        /// <summary>
        /// Check if user guess is in the random word
        /// </summary>
        /// <param name="display"></param>
        /// <param name="randomWord"></param>
        /// <param name="userGuess"></param>
        /// <returns>display word </returns>
        static string UpdateDisplay(string display, string randomWord, char userGuess)
        {
            string temp = "";
            int x = 0;
            foreach (char symbol in display)
            {
                if (symbol == '_' && randomWord[x / 2] == userGuess)
                {
                    temp += userGuess;
                }
                else
                {
                    temp += symbol;
                }
                x += 1;
            }
            return temp;
        }
        /// <summary>
        /// Method to allow the user the option to play again or return to the main menu.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>False return</returns>
        static int Replay(string path)
        {
            Console.WriteLine("Would you like to play again? Yes or No");
            string replayChoiceInput = Console.ReadLine();
            string lowerCaseReplayChoice = replayChoiceInput.ToLower();

            if (lowerCaseReplayChoice == "yes")
            {
                Game(path);
            }
            else if (lowerCaseReplayChoice == "no")
            {
                Menu(path);
            }
            else
            {
                Replay(path);
            }

            return -1;
        }
        /// <summary>
        /// Closes the Application
        /// </summary>
        /// <returns>False return, Application should close before then.</returns>
        static int Exit()
        {
            Environment.Exit(1);
            return -1;
        }
        /// <summary>
        /// Method to Create a File with a word
        /// </summary>
        /// <param name="path"></param>
        /// <param name="word"></param>
        public static void CreateFile(string path, string word)
        {
            if (File.Exists("random_words.txt"))
            {
                UpdateFile(path, word);
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    try
                    {
                        streamWriter.WriteLine(word);
                    }
                    catch (Exception e)
                    {
                        errorLogger.WriteLine(e);
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// Method to Create a File without a word
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            StreamWriter streamWriter = new StreamWriter(path);
        }
        /// <summary>
        /// Method to Read the contents of a file.
        /// </summary>
        /// <param name="path"></param>
        public static string[] ReadFile(string path)
        {
            try
            {
                string[] textLines = File.ReadAllLines(path);
                return textLines;
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// Method to Update text in a file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="word"></param>
        public static void UpdateFile(string path, string word)
        {
            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(word);
                }
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// MEthod to delete a file 
        /// </summary>
        /// <param name="path"></param>
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
