using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Zin
    {
        private String sentence, wrongWord, correctedWord;
        private int difficulty;


         public Zin() { 
        }

        public Zin(string sentence, string wrongWord, string correctedWord, int difficulty) {
            this.sentence = sentence;
            this.wrongWord = wrongWord;
            this.correctedWord = correctedWord;
            this.difficulty = difficulty;
        }

        public string Sentence
        {
            get { return sentence; }
            set { sentence = value; }
        }

        public string WrongWord
        {
            get { return wrongWord; }
            set { wrongWord = value; }
        }

        public string CorrectedWord
        {
            get { return correctedWord; }
            set { correctedWord = value; }
        }
       
        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }
        

    }
}
