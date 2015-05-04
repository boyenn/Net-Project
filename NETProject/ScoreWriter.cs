using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class ScoreWriter
    {
        private List<Points> studentPoints = new List<Points>();
        private static StreamReader inputStream = null;
        private static StreamWriter outputStream = null;
        private Points points;
        public ScoreWriter() {
            ReadPoints();
        }

        public void UpdateScore() {
            outputStream = File.CreateText("Resources/Files/Points.txt");
            for (int i = 0; i < studentPoints.Count; i++) {
                points = studentPoints[i];
                outputStream.WriteLine(points.Section + ";" + points.Student + ";"  + points.Score + ";" + points.Date);
            }
            outputStream.Close();
            ReadPoints();
        }

        public void AddPoints(Points points) {
            studentPoints.Add(points);
        }

        private void ReadPoints() {
            string line = "";
            string[] words = new string[4];
            string fileToSearch = "Resources/Files/Points.txt";
            try
            {
                inputStream = File.OpenText(fileToSearch);
                char seperator = ';';

                line = inputStream.ReadLine();

                while (line != null)
                {
                    words = line.Split(seperator);
                    points = new Points(words[0], words[1], Convert.ToInt32(words[2]), words[3]);
                    studentPoints.Add(points);
                    line = inputStream.ReadLine();
                }
            }
            catch (FileNotFoundException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                inputStream.Close();
            }
        }
    }
}
