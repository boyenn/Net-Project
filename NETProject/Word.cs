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
            List<string> full = new List<string>();
            string shaken = string.Empty;
            for (int i = 0; i < word.Length; i++)
            {
                full.Add(word.Substring(i, 1));
            }
            while (shaken.Length != word.Length)
            {
                for (int i = 0; i < full.Count; i++)
                {
                    int random = r.Next(0, full.Count - 1);
                    shaken += full[random];
                    full.RemoveAt(random);
                }
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
