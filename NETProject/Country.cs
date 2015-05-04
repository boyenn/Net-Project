using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Country
    {
        private string name;
        private int difficulty;
        private int x, y;

        public Country()
        {
        }

        public Country(string name, int x, int y, int difficulty)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.x = x;
            this.y = y;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
