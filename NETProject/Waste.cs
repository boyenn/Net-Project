using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Waste
    {
        string name, type;

        public Waste()
        {
        }

        public Waste(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
