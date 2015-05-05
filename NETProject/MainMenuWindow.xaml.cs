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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NETProject
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        //*----------------------*
        //Algemene properties
        //*----------------------*

        private int imageIndex;
        private int difficulty;


        //*----------------------*
        //Vlaggen properties
        //*----------------------*

        private FlagControl flag;
        private DispatcherTimer timer = new DispatcherTimer();
        private IList<Country> countries = new List<Country>();
        private List<FlagControl> wrong = new List<FlagControl>();
        private Random r = new Random();
        private Country country;
        private Points achieved;
        private ScoreWriter writer = new ScoreWriter();
        private FileReader reader = new FileReader();
        private DateTime today = DateTime.Today;
        private bool dragging = false;

        private int correct = 0;
        private int time = 300;

        //*----------------------*
        //Ballonnen properties
        //*----------------------*

        private BalloonQuestion baloon;
        private Operations operation;
        private DispatcherTimer timer2 = new DispatcherTimer();
        private int _time = 20;
        private int lower = 4, upper = 10;


        //*----------------------*
        //Woorden properties
        //*----------------------*

        //private Random r = new Random();
        IList<Word> words = new List<Word>();
        List<Word> randoms = new List<Word>();

        //*----------------------*
        //Zinnen properties
        //*----------------------*

        private int questionSentenceCounter;
        private DispatcherTimer timer3 = new DispatcherTimer();
        private bool mark = true;
        private IList<Zin> sentenceList = new List<Zin>();
        private IList<Zin> randomSentenceList = new List<Zin>();
        private Zin currentSentence;

        //*----------------------*
        //Breuken properties
        //*----------------------*
        private Random rnd = new Random();
        private int denominatorMin, denominatorMax, denominator1, numerator1, denominator2, numerator2, actionInt, multiplier, solutionDenominator, solutionNumerator;
        private string actionString;

        public MainMenuWindow()
        {
            InitializeComponent();

            //*----------------------*
            //Algemene initialisatie
            //*----------------------*

            difficulty = (int)difficultySlider.Value;

            SetActionListeners();
            SetVisibilityComponents();
            SetPersonalInfo();
            SetMarginComponents();
            SetImages();

            //*----------------------*
            //Vlaggen initialisatie
            //*----------------------*

            MapImage.Source = ImagePath.Bmprel("Resources/Images/map.png");
            MapImage.MouseUp += MapImage_MouseUp;
            vlaggenGrid.MouseUp += mapMainWindow_MouseUp;
            vlaggenGrid.MouseLeave += mapMainWindow_MouseLeave;
            countries = reader.ReadFile<Country>("Resources/Files/Locations.txt", 4).Select(s => (Country)s).ToList();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            createNewExercise();

            //*----------------------*
            //Ballonnen initialisatie
            //*----------------------*

            //CreateQuestion();
            SetCursor();
            timer2.Tick += timer_Tick2;
            timer2.Interval = TimeSpan.FromSeconds(1);
            //timer2.Start();
            balloonTimeLabel.Content = String.Format("{0:00}:{1:00}", _time / 60, _time % 60);


            //*----------------------*
            //Woorden initialisatie
            //*----------------------*

            words = reader.ReadFile<Word>("Resources/Files/Woorden.txt", 3).Select(s => (Word)s).ToList();
            NewEx();

            //*----------------------*
            //Zinnen initialisatie
            //*----------------------*

            timer3.Interval = TimeSpan.FromMilliseconds(10);
            timer3.Tick += timer_Tick3;
            zinTextBox.LostFocus += zinTextBox_LostFocus;

            sentenceList = reader.ReadFile<Zin>("Resources/Files/NederlandsOpgaven.txt", 4).Select(s => (Zin)s).ToList();
            zinTextBox.SelectionBrush = Brushes.Yellow;
            makeNewSentences();

            //*----------------------*
            //Breuken initialisatie
            //*----------------------*
            Settings.ReadSettingsFile();

            denominatorMin = Settings.getValue("denominatorMin");
            denominatorMax = Settings.getValue("denominatorMax");

            DrawLines();
            GenerateExercise(denominatorMin, denominatorMax);
        }

        

        //*----------------------*
        //Algemene methods
        //*----------------------*

        public void SetActionListeners()
        {
            mainMenuButton.Click += MainMenuButtonListener;
            exercisesButton.Click += MainMenuButtonListener;
            manageButton.Click += MainMenuButtonListener;
            logoutButton.Click += MainMenuButtonListener;

            image1.MouseDown += images_MouseDown;
            image2.MouseDown += images_MouseDown;
            image3.MouseDown += images_MouseDown;
            image4.MouseDown += images_MouseDown;
            image5.MouseDown += images_MouseDown;
            image6.MouseDown += images_MouseDown;


            excercisesManagementButton.Click += ManageMenuButtonListener;
            pupilManagementButton.Click += ManageMenuButtonListener;
            teacherManagementButton.Click += ManageMenuButtonListener;

            addPupilButton.Click += managePupilsButtonListener;
            changePupilButton.Click += managePupilsButtonListener;
            deletePupilButton.Click += managePupilsButtonListener;


            addTeacherButton.Click += manageTeachersButtonListener;
            changeTeacherButton.Click += manageTeachersButtonListener;
            deleteTeacherButton.Click += manageTeachersButtonListener;
        }

        public void images_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image temporaryImage = (Image)e.Source;
            int index = Convert.ToInt32(temporaryImage.Name.Substring(temporaryImage.Name.Length - 1));
            SetFocusImage(index);
        }


        void managePupilsButtonListener(object sender, RoutedEventArgs e)
        {
            User pupilObject;
            Button button = (Button)e.Source;


            switch (Convert.ToString(button.Content))
            {
                case "Toevoegen Leerling":

                    AddUserWindow addUserWindow = new AddUserWindow(0, this);
                    addUserWindow.ShowDialog();

                    break;
                case "Wachtwoord Wijzigen":

                    pupilObject = (User)dataGridPupils.SelectedItem;

                    if (pupilObject == null)
                    {
                        MessageBox.Show(this, "Geen leerling geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PasswordChangeWindow passwordChangeWindow = new PasswordChangeWindow(pupilObject);
                        passwordChangeWindow.ShowDialog();
                    }

                    break;
                case "Verwijder Leerling":

                    pupilObject = (User)dataGridPupils.SelectedItem;

                    if (pupilObject == null)
                    {
                        MessageBox.Show(this, "Geen leerling geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string message = "Bent u zeker dat u deze leerling wilt verwijderen?";
                        string caption = "Confirmation";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;

                        if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                        {


                            for (int i = 0; i < UserSummary.UserList.Count; i++)
                            {
                                User user = (User)UserSummary.UserList[i];
                                if (user.UserName == pupilObject.UserName)
                                {
                                    UserSummary.UserList.RemoveAt(i);
                                    UserSummary.WriteTextFile();
                                    AddStudentsToList();
                                }
                            }
                        }
                    }
                    break;

            }
        }




        void manageTeachersButtonListener(object sender, RoutedEventArgs e)
        {
            User teacherObject;
            Button button = (Button)e.Source;


            switch (Convert.ToString(button.Content))
            {
                case "Toevoegen Leerkracht":
                    AddUserWindow addUserWindow = new AddUserWindow(1, this);

                    addUserWindow.ShowDialog();

                    break;
                case "Wachtwoord Wijzigen":
                    teacherObject = (User)dataGridTeachers.SelectedItem;

                    if (teacherObject == null)
                    {
                        MessageBox.Show(this, "Geen leerkracht geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PasswordChangeWindow passwordChangeWindow = new PasswordChangeWindow(teacherObject);
                        passwordChangeWindow.ShowDialog();
                    }
                    break;
                case "Verwijder Leerkracht":

                    teacherObject = (User)dataGridTeachers.SelectedItem;

                    if (teacherObject == null)
                    {
                        MessageBox.Show(this, "Geen leerkracht geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string message = "Bent u zeker dat u deze leerkracht wilt verwijderen?";
                        string caption = "Confirmation";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;

                        if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                        {
                            for (int i = 0; i < UserSummary.UserList.Count; i++)
                            {
                                User user = (User)UserSummary.UserList[i];
                                if (user.UserName == teacherObject.UserName)
                                {
                                    UserSummary.UserList.RemoveAt(i);
                                    UserSummary.WriteTextFile();

                                    AddTeachersToList();

                                }
                            }
                        }
                    }
                    break;

            }
        }


        public void MainMenuButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Main Menu":
                    mainMenuTab.IsSelected = true;
                    break;
                case "Oefeningen":
                    exercisesTab.IsSelected = true;
                    break;
                case "Beheer":
                    manageTab.IsSelected = true;
                    break;
                case "Log Uit":
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                    break;
            }
        }

        public void ManageMenuButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Beheer Oefeningen":
                    excerciseManagementTab.IsSelected = true;
                    break;
                case "Beheer Leerlingen":
                    pupilManagementTab.IsSelected = true;
                    AddStudentsToList();
                    break;
                case "Beheer Leerkrachten":
                    teacherManagementTab.IsSelected = true;
                    AddTeachersToList();
                    break;

            }
        }

        public void AddTeachersToList()
        {

            dataGridTeachers.Items.Clear();
            foreach (User user in UserSummary.UserList)
            {
                if (user.UserType == 1)
                {
                    var data = new User { UserName = user.UserName };
                    dataGridTeachers.Items.Add(data);
                }
            }

        }

        public void AddStudentsToList()
        {

            dataGridPupils.Items.Clear();
            foreach (User user in UserSummary.UserList)
            {
                if (user.UserType == 0)
                {
                    var data = new User { UserName = user.UserName, UserPoints = user.UserPoints, UserHighscore = user.UserHighscore };
                    dataGridPupils.Items.Add(data);
                }
            }

        }

        public void ShowDetails(object sender, RoutedEventArgs e)
        {
            User obj = ((FrameworkElement)sender).DataContext as User;
            MessageBox.Show(obj.UserName);
        }


        public void SetVisibilityComponents()
        {
            switch (UserSummary.CurrentUser.UserType)
            {
                case 0:
                    manageButton.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    teacherManagementButton.Visibility = Visibility.Hidden;
                    pointsLabel.Visibility = Visibility.Hidden;
                    highscoreLabel.Visibility = Visibility.Hidden;
                    pointsLabel2.Visibility = Visibility.Hidden;
                    highscoreLabel2.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    pointsLabel.Visibility = Visibility.Hidden;
                    highscoreLabel.Visibility = Visibility.Hidden;
                    pointsLabel2.Visibility = Visibility.Hidden;
                    highscoreLabel2.Visibility = Visibility.Hidden;
                    break;
            }

        }

        public void SetPersonalInfo()
        {
            usernameLabel2.Content = UserSummary.CurrentUser.UserName;
            pointsLabel2.Content = UserSummary.CurrentUser.UserPoints;
            highscoreLabel2.Content = UserSummary.CurrentUser.UserHighscore;
        }


        public void SetMarginComponents()
        {
            if (UserSummary.CurrentUser.UserType == 0)
            {
                usernameLabel.Margin = new Thickness(310, 10, 0, 0);
                usernameLabel2.Margin = new Thickness(310, 35, 0, 0);

                pointsLabel.Margin = new Thickness(398, 10, 0, 0);
                pointsLabel2.Margin = new Thickness(398, 35, 0, 0);

                highscoreLabel.Margin = new Thickness(484, 10, 0, 0);
                highscoreLabel2.Margin = new Thickness(484, 35, 0, 0);
            }
            else
            {
                usernameLabel.Margin = new Thickness(395, 10, 0, 0);
                usernameLabel2.Margin = new Thickness(395, 35, 0, 0);
            }
        }

        public void SetImages()
        {
            image1.Source = ImagePath.Bmprel("Resources/Images/hoofdRekenen.jpg");
            image2.Source = ImagePath.Bmprel("Resources/Images/breuken.jpg");
            image3.Source = ImagePath.Bmprel("Resources/Images/sorteren.jpg");
            image4.Source = ImagePath.Bmprel("Resources/Images/vlaggen.jpg");
            image5.Source = ImagePath.Bmprel("Resources/Images/fouteZinnen.jpg");
            image6.Source = ImagePath.Bmprel("Resources/Images/woordHussel.jpg");
        }

        public void SetFocusImage(int index)
        {
            border1.BorderBrush = Brushes.White;
            border2.BorderBrush = Brushes.White;
            border3.BorderBrush = Brushes.White;
            border4.BorderBrush = Brushes.White;
            border5.BorderBrush = Brushes.White;
            border6.BorderBrush = Brushes.White;

            switch (index)
            {
                case 1: border1.BorderBrush = Brushes.Red; break;
                case 2: border2.BorderBrush = Brushes.Red; break;
                case 3: border3.BorderBrush = Brushes.Red; break;
                case 4: border4.BorderBrush = Brushes.Red; break;
                case 5: border5.BorderBrush = Brushes.Red; break;
                case 6: border6.BorderBrush = Brushes.Red; break;
            }
        }


        //*----------------------*
        //Oefening vlaggen methods
        //*----------------------*

        private void timer_Tick2(object sender, EventArgs e)
        {
            time--;
            timeLabel.Content = String.Format("{0:00}:{1:00}", time / 60, time % 60);
            CheckTime();
        }

        private void mapMainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void mapMainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
        }

        private void MapImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Point x = Mouse.GetPosition(mapCanvas);
            if (dragging)
            {
                FlagControl curr;
                bool added = false;
                Point p = Mouse.GetPosition(mapCanvas);
                int ind = FindIndex((int)p.X, (int)p.Y);
                //MessageBox.Show(Convert.ToString(ind));
                Country pos = countries[ind];
                flag = new FlagControl("Resources/Images/Flags/" + flag.GetLand() + "256.png", flag.GetLand(), pos.X, pos.Y);
                flag.Margin = new Thickness(pos.X - 60, pos.Y - 120, 0, 0);
                flag.MouseDoubleClick += flag_MouseDoubleClick;
                if (mapCanvas.Children.Count != 0)
                {
                    for (int i = 0; i < mapCanvas.Children.Count; i++)
                    {
                        curr = (FlagControl)mapCanvas.Children[i];
                        if ((flag.X == curr.X) && (flag.Y == curr.Y))
                        {
                            added = false;
                            break;
                        }
                        else
                        {
                            added = true;
                        }
                    }
                }
                else
                {
                    added = true;
                }

                if (added)
                {
                    mapCanvas.Children.Add(flag);
                    for (int i = 0; i < flagList.Children.Count; i++)
                    {
                        curr = (FlagControl)flagList.Children[i];
                        if (flag.GetLand().Equals(curr.GetLand()))
                        {
                            flagList.Children.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Er staat al een vlag op deze locatie.");
                }
                dragging = false;
            }
            //MessageBox.Show("X: " + x.X + ";Y:" + x.Y);
        }

        private void flag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
            flag = (FlagControl)sender;
        }

        private void flag_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            flag = (FlagControl)sender;
            for (int i = 0; i < mapCanvas.Children.Count; i++)
            {
                FlagControl curr = (FlagControl)mapCanvas.Children[i];
                if (flag.GetLand().Equals(curr.GetLand()))
                {
                    mapCanvas.Children.RemoveAt(i);
                    flag = new FlagControl("Resources/Images/Flags/" + curr.GetLand() + "256.png", curr.GetLand());
                    flag.MouseDown += flag_MouseDown;
                    flagList.Children.Add(flag);
                }
            }
        }

        public void createNewExercise()
        {
            mapCanvas.Children.Clear();
            flagList.Children.Clear();
            time = 300;
            timer.Start();
            timeLabel.Content = String.Format("{0:00}:{1:00}", time / 60, time % 60);
            FillList();
        }

        public void CheckTime()
        {
            if (time == 0)
            {
                timer.Stop();
                MessageBox.Show("Je tijd is om!");
                CheckFlags();
            }
        }

        public void CheckFlags()
        {
            correct = 0;
            Country needed = null;
            for (int i = 0; i < mapCanvas.Children.Count; i++)
            {
                flag = (FlagControl)mapCanvas.Children[i];
                for (int j = 0; j < countries.Count; j++)
                {
                    if (flag.GetLand().Equals(countries[j].Name))
                    {
                        needed = countries[j];
                    }
                }
                if ((flag.X == needed.X) && (flag.Y == needed.Y))
                {
                    correct++;
                }
                else
                {
                    flag.Margin = new Thickness(needed.X - 60, needed.Y - 120, 0, 0);
                    flag.InCanvas = true;
                    flag.WrongLabel.Content = "Fout!";
                    wrong.Add(flag);
                }
            }
            mapCanvas.Children.Clear();
            for (int i = 0; i < flagList.Children.Count; i++)
            {
                flag = (FlagControl)flagList.Children[i];
                flag.WrongLabel.Content = "Fout!";
                for (int j = 0; j < countries.Count; j++)
                {
                    if (flag.GetLand().Equals(countries[j].Name))
                    {
                        needed = countries[j];
                    }
                }
                flag.Margin = new Thickness(needed.X - 60, needed.Y - 120, 0, 0);
                flag.InCanvas = true;
                wrong.Add(flag);
            }
            flagList.Children.Clear();
            for (int i = 0; i < wrong.Count; i++)
            {
                mapCanvas.Children.Add(wrong[i]);
            }
            if (correct == 10)
            {
                MessageBox.Show("Proficiat! Je hebt alle vlaggen juist geplaatst.");
            }
            else
            {
                MessageBox.Show("Je hebt " + correct + " vlag(gen) correct op de map geplaatst.");
            }
            achieved = new Points("Algemene kennis", "Maarten Bloemen", correct, String.Format("{0:d}", today));
            writer.AddPoints(achieved);
            writer.UpdateScore();
        }

        public int FindIndex(int x, int y)
        {
            int index = 0;
            for (int i = 0; i < countries.Count; i++)
            {
                Country curr = countries[i];
                Country closest = countries[index];
                if ((curr.X > x - 50) && (curr.X < x + 50) && (curr.Y > y - 50) && (curr.Y < y + 50))
                {
                    if (Math.Abs(curr.X - x) < Math.Abs(closest.X - x))
                    {
                        if (Math.Abs(curr.Y - y) < Math.Abs(closest.Y - y))
                        {
                            index = i;
                        }
                    }
                }
            }
            return index;
        }

        public void FillList()
        {
            while (flagList.Children.Count != 10)
            {
                bool add = true;
                country = countries[r.Next(0, countries.Count)];
                if (!(country.Difficulty <= difficulty))
                {
                    add = false;
                }
                for (int i = 0; i < flagList.Children.Count; i++)
                {
                    FlagControl curr = (FlagControl)flagList.Children[i];
                    if (country.Name.Equals(curr.GetLand()))
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    flag = new FlagControl("Resources/Images/Flags/" + country.Name + "256.png", country.Name);
                    flag.MouseDown += flag_MouseDown;
                    flagList.Children.Add(flag);
                }
            }
        }

        private void controlButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            CheckFlags();
        }

        public void CancelExcercise()
        {
            createNewExercise();
            timer.Stop();
        }


        //*----------------------*
        //Ballonnen methods
        //*----------------------*

        private void timer_Tick(object sender, EventArgs e)
        {
            _time--;
            balloonTimeLabel.Content = String.Format("{0:00}:{1:00}", _time / 60, _time % 60);
            balloonCheckTime();
        }

        private void baloon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BalloonQuestion balloon1 = (BalloonQuestion)e.Source;
            timer2.Stop();
            if (balloon1.GetContent() == operation.getAnswer())
            {
                MessageBox.Show("Correct!");
            }
            else
            {

                MessageBox.Show("Helaas, het juiste antwoord was: " + operation.getAnswer() + ".");
            }
            _time = 20;
            balloonTimeLabel.Content = String.Format("{0:00}:{1:00}", _time / 60, _time % 60);
            CreateQuestion();
            timer2.Start();
        }

        private void balloonCheckTime()
        {
            if (_time == 0 && operation != null)
            {

                MessageBox.Show("Helaas, het juiste antwoord was: " + operation.getAnswer() + ".");

                _time = 20;
                balloonTimeLabel.Content = Convert.ToString(_time);
                CreateQuestion();

            }
        }

        private void SetCursor()
        {
            this.Cursor = null;
            Cursor cursor = curRel("Resources/Images/Balloons/Crosshairs_1.cur");
            this.Cursor = cursor;
        }

        public void CreateQuestion()
        {
            Random r = new Random();
            operation = new Operations(r.Next(1, 5), r.Next(lower, upper), r.Next(lower, upper));
            int next = r.Next(0, 4);

            question.Content = Convert.ToString(operation.Number1 + operation.getOp() + operation.Number2 + " =");

            for (int i = 0; i < 4; i++)
            {
                if (i == next)
                {
                    baloon = new BalloonQuestion("Resources/Images/Balloons/Balloon" + (i + 1) + ".png", Convert.ToString(operation.getAnswer()));
                }
                else
                {
                    int rand = r.Next(1, 10) * r.Next(1, 10);
                    if (operation.getAnswer() != rand)
                    {
                        baloon = new BalloonQuestion("Resources/Images/Balloons/Balloon" + (i + 1) + ".png", Convert.ToString(rand));
                    }
                    else
                    {
                        rand = r.Next(1, 10) * r.Next(1, 10);
                        baloon = new BalloonQuestion("Resources/Images/Balloons/Balloon" + (i + 1) + ".png", Convert.ToString(rand));
                    }
                }
                baloon.Margin = new Thickness(120 * i, 50, 0, 0);
                baloon.MouseDown += baloon_MouseDown;
                test.Children.Add(baloon);
            }
        }

        public Cursor curRel(string relativepath)
        {
            Cursor x = new Cursor(System.IO.Path.GetFullPath(relativepath));
            return x;
        }

        //*----------------------*
        //Woorden methods
        //*----------------------*

        private void NewEx()
        {
            FillList2();
            mainCanvas.Children.Clear();
            for (int i = 0; i < 10; i++)
            {
                Label l = new Label();
                l.Content = randoms[i].Shaken;
                l.Margin = new Thickness(10, i * 30, 0, 0);
                l.ToolTip = randoms[i].Description;
                mainCanvas.Children.Add(l);
                TextBox t = new TextBox();
                t.Margin = new Thickness(100, i * 30 + 5, 0, 0);
                t.Width = 200;
                t.Height = 20;
                mainCanvas.Children.Add(t);
            }
        }

        private void FillList2()
        {
            randoms.Clear();
            while (randoms.Count != 10)
            {
                bool add = true;
                Word n = words[r.Next(0, words.Count - 1)];
                for (int i = 0; i < randoms.Count; i++)
                {
                    Word curr = randoms[i];
                    if (n.Words.Equals(curr.Words))
                    {
                        add = false;
                    }
                }
                if (n.Difficulty > difficulty)
                {
                    add = false;
                }
                if (add)
                {
                    randoms.Add(n);
                }
            }
        }

        private void Check()
        {
            int j = 0;
            for (int i = 1; i < mainCanvas.Children.Count; i += 2)
            {
                TextBox t = (TextBox)mainCanvas.Children[i];
                if (t.Text.Equals(randoms[j].Words))
                {
                    t.Foreground = Brushes.Green;
                    t.IsReadOnly = true;
                    correct++;
                }
                else
                {
                    t.Text = randoms[j].Words;
                    t.Foreground = Brushes.Red;
                    t.IsReadOnly = true;
                }
                j++;
            }
            if (correct == 10)
            {
                MessageBox.Show("Je hebt alles correct!");
            }
            else
            {
                MessageBox.Show("Je hebt er " + correct + " correct!");
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewEx();
        }

        //*----------------------*
        //Zinnen methods
        //*----------------------*

        void timer_Tick3(object sender, EventArgs e)
        {
            int clickedIndex = zinTextBox.GetCharacterIndexFromPoint(Mouse.GetPosition(zinTextBox), true);
            zinTextBox.SelectionBrush = Brushes.Yellow;

            if (!string.IsNullOrWhiteSpace(Convert.ToString(zinTextBox.Text[clickedIndex])))
            {
                int[] array = calculateStartAndLength(clickedIndex);

                zinTextBox.Select(array[0], array[1] - array[0]);
            }
            mark = true;
            timer3.Stop();
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {

            zinTextBox.SelectionBrush = Brushes.Transparent;

            if (currentSentence.WrongWord.Equals(zinTextBox.SelectedText) && currentSentence.CorrectedWord.Equals(antwoordTextBox.Text))
            {

                MessageBox.Show("Juist antwoord!");

            }
            else {

               // MessageBox.Show("fout! " + currentSentence.WrongWord + " " + zinTextBox.SelectedText + " " + currentSentence.CorrectedWord + " " + antwoordTextBox.Text);
               MessageBox.Show("Fout!" + Environment.NewLine + "Foute woord: " + currentSentence.WrongWord + Environment.NewLine + "Correctie: " + currentSentence.CorrectedWord);
            }


           
            nextSentence();
         
           
        }


        private void zinTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mark)
            {
                timer.Start();
            }
            mark = false;

        }


        public int[] calculateStartAndLength(int clickedIndex)
        {

            int[] array = new int[2];

            Boolean testLeft = true, testRight = true;
            int checkLeft = clickedIndex, checkRight = clickedIndex;

            while (testLeft || testRight)
            {
                if (testLeft) checkLeft--;
                if (testRight) checkRight++;

                if (checkLeft <= 0)
                {
                    testLeft = false;
                    checkLeft = 0;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(zinTextBox.Text[checkLeft])))
                    {
                        testLeft = false;
                        checkLeft++;
                    }
                }

                if (checkRight >= zinTextBox.Text.Length)
                {
                    testRight = false;

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(zinTextBox.Text[checkRight])))
                    {
                        testRight = false;

                    }
                }

            }

            array[0] = checkLeft;
            array[1] = checkRight;

            return array;
        }

        private void makeNewSentences()
        {
            questionSentenceCounter = 0;
            randomSentenceList.Clear();

          

            while (randomSentenceList.Count < 10)
            {
                int random = r.Next(0, sentenceList.Count);

                if (!randomSentenceList.Contains(sentenceList[random]) && sentenceList[random].Difficulty <= difficulty)
                {
                    randomSentenceList.Add(sentenceList[random]);
                   
                }
            }
            
            nextSentence();

        }

        private void nextSentence()
        {

            questionSentenceLabel.Content = ++questionSentenceCounter + "/10";
            currentSentence = randomSentenceList[randomSentenceList.Count - 1];
            randomSentenceList.Remove(currentSentence);

            zinTextBox.Text = currentSentence.Sentence;


            antwoordTextBox.Clear();

        }

        private void zinTextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mark)
            {
                timer3.Start();
            }
            mark = false;
        }
   
        void zinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            e.Handled = true;
        }

        //*----------------------*
        //Breuken methods
        //*----------------------*

        private void GenerateExercise(int denominatorMin, int denominatorMax)
        {
            //<Begin> random kiezen of het een optelling of aftrekking wordt.
            actionInt = rnd.Next(1, 3);
            if (actionInt == 1)
            {
                actionString = "+";
            }
            else
            {
                actionString = "-";
            }
            //<Einde>

            //<Begin> De random breuken genereren.
            denominator1 = rnd.Next(denominatorMin, denominatorMax + 1);
            numerator1 = rnd.Next(1, denominator1 + 1);
            multiplier = rnd.Next(1, 4);
            denominator2 = multiplier * denominator1;
            numerator2 = rnd.Next(1, denominator2 + 1);
            //<Einde>



            DrawExercise();
            CalculateResult();

            CakesToDraw(solutionNumerator, solutionDenominator);
        }

        private void DrawExercise()
        {


            actionLabel.Content = actionString;
            lbl1.Content = numerator1;
            lbl2.Content = denominator1;

            lbl3.Content = numerator2;
            lbl4.Content = denominator2;

        }
        private void CalculateResult()
        {
            if ((Convert.ToDouble(numerator2) / multiplier) % 1 == 0)
            {
                solutionDenominator = denominator1;
                if (actionInt == 1)
                {
                    solutionNumerator = numerator1 + numerator2 / multiplier;
                }
                else
                {
                    solutionNumerator = numerator1 - numerator2 / multiplier;
                }

            }
            else
            {
                solutionDenominator = denominator2;
                if (actionInt == 1)
                {
                    solutionNumerator = numerator1 * multiplier + numerator2;
                }
                else
                {
                    solutionNumerator = numerator1 * multiplier - numerator2;
                }

            }

        }
        private void DrawLines()
        {
            Line line1 = new Line();
            Line line2 = new Line();

            line1.X1 = 10;
            line1.Y1 = 10;
            line1.X2 = 60;
            line1.Y2 = 10;
            line1.Stroke = new SolidColorBrush(Colors.Black);

            line2.X1 = 230;
            line2.Y1 = 10;
            line2.X2 = 280;
            line2.Y2 = 10;
            line2.Stroke = new SolidColorBrush(Colors.Black);
            drawingCanvas.Children.Add(line1);
            drawingCanvas.Children.Add(line2);


        }
        private void CakesToDraw(int num, int denom)
        {
            int amountOfCakes, x = 10, y = 10, counter = 1;

            amountOfCakes = (num / denom) + 1;




            while (amountOfCakes > 1)
            {
                Ellipse e = new Ellipse { Width = 100, Height = 100, Margin = new Thickness(x, y, 0, 0) };
                e.Fill = Brushes.Red;
                pieCanvas.Children.Add(e);
                amountOfCakes -= 1;
                x += 110;
                num -= denom;
                counter += 1;
                if (counter == 3)
                {
                    x -= 220;
                    y += 110;
                }
            }

            DrawCakes(num, denom, x, y);

        }
        private void DrawCakes(int num, int denom, int x, int y)
        {

            Ellipse e = new Ellipse { Width = 100, Height = 100, Margin = new Thickness(x, y, 0, 0) };
            var centerx = e.Margin.Left + e.Width / 2;
            var centery = e.Margin.Top + e.Height / 2;
            e.Fill = Brushes.Red;
            double degrees = 360 - ((Convert.ToDouble(num) / denom) * 360.0);

            int rest = (int)degrees % 90;
            int tricount = (int)(degrees / 90) + 1;
            Point Centerpoint = new Point(centerx, centery);
            pieCanvas.Children.Add(e);

            for (int j = 1; j <= tricount; j++)
            {
                Polygon p = new Polygon();
                p.Fill = Brushes.White;

                p.Points.Add(Centerpoint);

                p.Points.Add(new Point(centerx + 100 * Math.Cos(ConvertToRadians((j - 2) * 90)), centery + 100 * Math.Sin(ConvertToRadians((j - 2) * 90))));
                if (j == tricount)
                {
                    p.Points.Add(new Point(centerx + 100 * Math.Cos(ConvertToRadians((j - 2) * 90 + rest)), centery + 100 * Math.Sin(ConvertToRadians((j - 2) * 90 + rest))));
                }
                else
                {
                    p.Points.Add(new Point(centerx + 100 * Math.Cos(ConvertToRadians((j - 1) * 90)), centery + 100 * Math.Sin(ConvertToRadians((j - 1) * 90))));
                }

                pieCanvas.Children.Add(p);

            }







        }
        private void CheckAnswer()
        {

            string answer = txtAnswer.Text;
            string[] fraction = answer.Split('/');
            int answerNumerator = Convert.ToInt32(fraction[0]);
            int answerDenominator = Convert.ToInt32(fraction[1]);
            if (answerNumerator == solutionNumerator && answerDenominator == solutionDenominator)
            {
                MessageBox.Show("Proficiat!! Je hebt het goed!");
            }
            else
            {
                MessageBox.Show("Helaas, dit was niet het juiste antwoord, het juiste antwoord was: " + solutionNumerator + "/" + solutionDenominator);
            }
            GenerateExercise(denominatorMin, denominatorMax);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            CheckAnswer();
        }
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
