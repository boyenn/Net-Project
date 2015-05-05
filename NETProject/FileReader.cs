using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NETProject
{
    class FileReader
    {
        private static StreamReader inputStream = null;
        private Country country;
        private Word word;
       // private User user;
        private Zin zin;
        private Waste waste;

        public FileReader()
        {

        }

        public IList<Object> ReadFile<T>(string file, int numberOfWords) where T : class, new()
        {
            IList<Object> list = new List<Object>();
            T instance = new T();
            string line = "";
            string[] words = new string[numberOfWords];
            string fileToSearch = file;
            try
            {
                inputStream = new StreamReader(file, Encoding.Default, true);

                line = inputStream.ReadLine();

                while (line != null)
                {
                    int i = 0;
                    words = line.Split(';');
                    switch (Convert.ToString(instance.GetType()))
                    {
                        case "NETProject.Country":
                            country = new Country(words[i++], Convert.ToInt32(words[i++]), Convert.ToInt32(words[i++]), Convert.ToInt32(words[i]));
                            list.Add(country);
                            break;

                        case "NETProject.Waste":
                            waste = new Waste("Resources/Images/Waste/" + words[i++] + ".png", words[i]);
                            list.Add(waste);
                            break;

                        case "NETProject.Word":
                            word = new Word(words[i++], words[i++], Convert.ToInt32(words[i]));
                            list.Add(word);
                            break;
                        case "NETProject.Zin":
                            
                            zin = new Zin(words[i++], words[i++], words[i++], Convert.ToInt32(words[i]));
                            list.Add(zin);
                            break;
                    }
                    line = inputStream.ReadLine();
                }
            }
            catch (FileNotFoundException ex)
            {
                System.Windows.MessageBox.Show("Error: Bestand niet gevonden: " + fileToSearch);
            }
            finally
            {
                inputStream.Close();
            }
            return list;
        }
    }
}
