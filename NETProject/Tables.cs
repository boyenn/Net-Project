using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class Operations
    {
        int operation, number1, number2;

        public Operations(int operation, int num1, int num2)
        {
            Random r = new Random();
            this.operation = operation;
            if (operation == 4 || operation == 2)
            {
                number1 = num1 * num2;
                number2 = num2;
            }
            else
            {
                number1 = num1;
                number2 = num2;
            }
        }

        public int getAnswer()
        {
            int answer = 0;
            switch (operation)
            {
                case 1:
                    answer = number1 + number2;
                    break;
                case 2:
                    answer = number1 - number2;
                    break;
                case 3:
                    answer = number1 * number2;
                    break;
                case 4:
                    answer = number1 / number2;
                    break;
            }
            return answer;
        }

        public string getOp()
        {
            string op = string.Empty;
            switch (operation)
            {
                case 1:
                    op = " + ";
                    break;
                case 2:
                    op = " - ";
                    break;
                case 3:
                    op = " X ";
                    break;
                case 4:
                    op = " : ";
                    break;
            }
            return op;
        }

        public int Number1
        {
            get { return number1; }
            set { number1 = value; }
        }

        public int Number2
        {
            get { return number2; }
            set { number2 = value; }
        }
    }
}
