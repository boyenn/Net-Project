using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WiskundeProject
{
    static class Settings
    {
        private static StreamReader inputStream = null;
        private static List<string> names = new List<string>();
        private static List<int> values = new List<int>();

         public static void ReadSettingsFile()
        {
            string line = "";
            string[] words = new string[2];
            string fileToSearch = "Settings.txt";
        

            try
            {

                inputStream = File.OpenText(fileToSearch);
                char seperator = ';';

                line = inputStream.ReadLine();

                while (line != null)
                {
                    words = line.Split(seperator);

                    names.Add(words[0]);
                    values.Add(Convert.ToInt32(words[1]));
                    line = inputStream.ReadLine();

                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: Bestand niet gevonden: " + fileToSearch);
            }

            if (inputStream != null)
            {
                inputStream.Close();
            }
        }
         public static int getValue(string name)
         {
             for (int i = 0; i < Settings.Names.Count; i++)
             {
                 if (Settings.Names[i].Equals(name))
                 {
                     return(Settings.Values[i]);
                 }
             }
             return 0;
         }
        public static List<string> Names
        {
            get { return Settings.names; }
            set { Settings.names = value; }
        }
        public static List<int> Values
        {
            get { return Settings.values; }
            set { Settings.values = value; }
        }
    }
}
