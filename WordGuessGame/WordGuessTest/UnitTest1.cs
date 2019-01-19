using System;
using System.IO;
using Xunit;
using WordGuessGame;
namespace WordGuessTest
{
    public class UpdateDisplayTest
    {
        [Fact]
        public void UpdateInWord()
        {
            string display = "_ _ _ _ ";
            string randomWord = "kona";
            char userGuess = 'n';
            Assert.Equal("_ _ n _ ", Program.UpdateDisplay(display, randomWord, userGuess));
        }
        [Fact]
        public void UpdateNotInWord()
        {
            string display = "_ _ _ _ ";
            string randomWord = "kona";
            char userGuess = 'h';
            Program.UpdateDisplay(display, randomWord, userGuess);
            Assert.Equal("_ _ _ _ ", Program.UpdateDisplay(display, randomWord, userGuess));
        }
    }
    public class CreateFileTest
    {
        [Fact]
        public void CreatFileNew()
        {
            string path = "/FileForXUnitTestingCreate.txt";
            Assert.False(File.Exists(path));
            Program.CreateFile(path, "Kona");
            Assert.True(File.Exists(path));
            Program.DeleteFile(path);
        }
        [Fact]
        public void CreateFileAlreadyExists()
        {
            string path = "/FileForXUnitTestingCreate.txt";
            Program.CreateFile(path, "Kona");
            Assert.Throws<Exception>(()=> Program.CreateFile(path, "Boo Boo"));
            Program.DeleteFile(path);
        }
    }
    public class UpdateFileTest
    {
        [Fact]
        public void UpdateFileWithText()
        {
            string path = "/FileForXUnitTestingUpdate.txt";
            Program.CreateFile(path, "Kona");
            Program.UpdateFile(path, "Boo Boo");
            string[] testFile = Program.ReadFile(path);
            int length = testFile.Length;

            Assert.Equal(2, length);
            Program.DeleteFile(path);
        }
        [Fact]
        public void UpdateFileDNE()
        {
            string path = "Kona_Is_So_Cute";
            Assert.Throws<Exception>(() => Program.UpdateFile(path, "Boo Boo"));
        }
    }
    public class ReadFileTest
    {
        [Fact]
        public void ReadFileExists()
        {
            string path = "/FileForXUnitTestingRead.txt";
            string[] lines = Program.ReadFile(path);
            string contents = lines[0];
            Assert.Equal("Kona", contents);
        }
        [Fact]
        public void ReadFileDNE()
        {
            string path = "Kona_Is_So_Cute";
            Assert.Throws<Exception>(() => Program.ReadFile(path));
        }
    }
    public class DeleteFileTest
    {
        [Fact]
        public void FileRemoved()
        {
            string path = "/FileForXUnitTestingDelete.txt";
            Program.CreateFile(path, "Kona");
            Assert.True(File.Exists(path));

            Program.DeleteFile(path);
            Assert.False(File.Exists(path));
        }
    }


}
