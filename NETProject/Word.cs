using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Word
    {
        private string word, shaken, description;
        private Random r = new Random();
        private int difficulty;

        public Word() { 
        }

        public Word(string word, string description, int difficulty) {
            this.word = word;
            this.description = description;
            this.difficulty = difficulty;
            shaken = ShakeWord(this.word);
        }

        private string ShakeWord(string word)
        {
            string full = word;
            string shaken = String.Empty;
            while (shaken.Length != word.Length)
            {
                int random = r.Next(0, full.Length - 1);
                shaken += full.Substring(random, 1);
                full = full.Remove(random, 1);
            }
            return shaken;
        }

        public string Words
        {
            get { return word; }
            set { word = value; }
        }

        public string Shaken
        {
            get { return shaken; }
            set { shaken = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }
    }
}
