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

                        case "NETProject.User":
                            country = new Country(words[i++], Convert.ToInt32(words[i++]), Convert.ToInt32(words[i++]), Convert.ToInt32(words[i]));
                            list.Add(country);
                            break;

                        case "Project.Classe":

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
