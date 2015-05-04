using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Points
    {
        string section, student, date;
        int score;

        public Points(string section, string student, int score, string date)
        {
            this.section = section;
            this.student = student;
            this.score = score;
            this.date = date;
        }

        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        public string Student
        {
            get { return student; }
            set { student = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
