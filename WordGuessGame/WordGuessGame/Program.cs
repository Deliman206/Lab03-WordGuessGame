using System;
using System.Collections.Generic;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        /// <summary>
        /// Entry point for Program. Calls the Menu.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string pathText = "../../../random_words.txt";
            Menu(pathText);
        }

        /// <summary>
        /// This Method allows the user to input what part of the game they want to use.
        /// </summary>
        /// <param name="path">Path to File with Random Words</param>
        /// <returns> Method calls to adding words, starting game, exiting program</returns>

        static void Menu(string path)
        {
            Console.Clear();
            Console.WriteLine("WORD GUESS");
            Console.WriteLine("1. Add Words\n2. View Words\n3. Remove Words\n4. Start New List\n5. Play Game\n6. Exit");
            string menuChoice = Console.ReadLine();
            string lowerCaseMenuChoice = menuChoice.ToLower();

            if (lowerCaseMenuChoice == "add words" || lowerCaseMenuChoice == "1")
                AddWords(path);
            if (lowerCaseMenuChoice == "view words" || lowerCaseMenuChoice == "2")
                ViewWords(path);
            if (lowerCaseMenuChoice == "remove words" || lowerCaseMenuChoice == "3")
                RemoveWords(path);
            if (lowerCaseMenuChoice == "start new list" || lowerCaseMenuChoice == "4")
                StartNewList(path);
            if (lowerCaseMenuChoice == "play game" || lowerCaseMenuChoice == "5")
                Game(path);
            if (lowerCaseMenuChoice == "exit" || lowerCaseMenuChoice == "6")
                Exit();
            else
            {
                Menu(path);
            }
        }
        /// <summary>
        /// Deletes the list of words for the game and creates a new one.
        /// Allows the user to enter more than one word into the new list.
        /// </summary>
        /// <param name="path"></param>
        static void StartNewList(string path)
        {
            DeleteFile(path);
            bool done = false;
            string firstWord = "";
            while (firstWord == "")
            {
                Console.Clear();
                Console.WriteLine("What word would to like to add to the game?");
                string inputWordCreate = Console.ReadLine();
                firstWord = inputWordCreate.ToLower();
            }
            CreateFile(path, firstWord);
            while (done == false)
            {
                Console.Clear();
                Console.WriteLine("\nWould you like to add another word? If you are done hit ENTER.");
                string inputWordUpdate = Console.ReadLine();
                if (inputWordUpdate == "")
                {
                    done = true;
                    continue;
                }
                string wordToAddUpdate = inputWordUpdate.ToLower();
                UpdateFile(path, wordToAddUpdate);
            }
            Console.Clear();
            Menu(path);

        }
        /// <summary>
        /// Method to Create a text file for the guess game.
        /// User can add more that one word.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Method call to return to the main menu</returns>

        static void AddWords(string path)
        {
            Console.Clear();
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
                    continue;

                }
                string wordToAddUpdate = inputWordUpdate.ToLower();
                UpdateFile(path, wordToAddUpdate);
            }
            Console.Clear();
            Menu(path);
        }
        /// <summary>
        /// Method to view all the words in the random_words.txt file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Method call to return to the main menu</returns>
        static void ViewWords(string path)
        {
            Console.Clear();
            string[] lines = ReadFile(path);

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("PRESS ENTER TO WHEN DONE ....");
            Console.Read();
        }
        /// <summary>
        /// Method to remove any words from the 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static void RemoveWords(string path)
        {
            Console.Clear();
            string[] viewlines = ReadFile(path);

            foreach (string line in viewlines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("What word would to like to remove from the game?");
            // Take input and convert to lower case
            string inputWordRemove = Console.ReadLine();
            string wordToRemove = inputWordRemove.ToLower();

            // Make array of all the words
            string[] lines = ReadFile(path);

            // Make a new List to search the words
            List<string> Words = new List<string>();

            // Decalre a counter for search 
            int count = 0;
            int removeIndex = 0;

            // make string array into List<string>
            // track the idex of the word to remove
            foreach(string line in lines)
            {
                Words.Add(line);
                if (line == wordToRemove)
                {
                    removeIndex = count;
                }
                count += 1;
            }

            // Remove at the tracked index in the List
            Words.RemoveAt(removeIndex);

            // Convert to Output type string[]
            string[] newWordsList = Words.ToArray();

            //Delete the old file
            DeleteFile(path);

            // Recreate the File
            foreach(string line in newWordsList)
            {
                CreateFile(path, line);
            }

            // Go home Menu
            Menu(path);
        }
        /// <summary>
        /// Method that runs the game
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Replay method</returns>
        static void Game(string path)
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
            Replay(path);
        }
        /// <summary>
        /// Closes the Application
        /// </summary>
        /// <returns>False return, Application should close before then.</returns>
        static void Exit()
        {
            Environment.Exit(1);
        }
        /// <summary>
        /// Check if user guess is in the random word
        /// </summary>
        /// <param name="display"></param>
        /// <param name="randomWord"></param>
        /// <param name="userGuess"></param>
        /// <returns>display word </returns>
        static public string UpdateDisplay(string display, string randomWord, char userGuess)
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
        static void Replay(string path)
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
            
        }
        /// <summary>
        /// Method to Create a File with a word.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="word"></param>
        public static void CreateFile(string path, string word)
        {
            if (File.Exists(path))
                throw new Exception();
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                streamWriter.WriteLine(word);
            }
            catch (Exception)
            {
                Console.WriteLine("This file already exists!");
                throw;
            }
        }
        /// <summary>
        /// Method to Read the contents of a file.
        /// </summary>
        /// <param name="path"></param>
        public static string[] ReadFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception();
            try
            {
                string[] textLines = File.ReadAllLines(path);
                return textLines;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            if (!File.Exists(path))
                throw new Exception();
            try
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(word);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
