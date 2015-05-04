using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETProject
{
    /// <summary>
    /// Interaction logic for BalloonQuestion.xaml
    /// </summary>
    public partial class BalloonQuestion : UserControl
    {
        public BalloonQuestion(string image, string antwoord)
        {
            InitializeComponent();
            balloonImage.Source = ImagePath.Bmprel(image);
            balloonLabel.Content = antwoord;
            balloonLabel.Margin = new Thickness(34 - (antwoord.Length * 4), 28, 0, 0);
        }

        public int GetContent() {
            return Convert.ToInt32(balloonLabel.Content);
        }
    }
}
