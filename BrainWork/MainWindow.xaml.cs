using System;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BrainWork
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] liste = new char[100000];
        int pointeurBande = 50000, loop, pointeurCaractere = 0;

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            
            InitializeComponent();
        }

        /// <summary>
        ///  Initialise le tableau représentant la bande, place le pointeur en son centre et lance l'interpretation du code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            
            run(TbIn.Text);
            BtnClear.IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private async void run(string input)
        {
            bool isReadingValues = true;
            int value = 0;
            
            while (pointeurCaractere < input.Length)
            {
                await Task.Delay((int) sliderDelay.Value);
                switch (input[pointeurCaractere])
                {
                    case '+':
                        //if (liste[pointeurBande] == 0)
                        //    liste[pointeurBande] = (char)32;
                        liste[pointeurBande]++;
                        break;
                    case '-':
                        if (liste[pointeurBande] == 0)
                            liste[pointeurBande] = (char)255;
                        else
                            liste[pointeurBande]--;
                        break;
                    case '>':
                        pointeurBande++;
                        break;
                    case '<':
                        pointeurBande--;
                        break;
                    case '[':
                        if (liste[pointeurBande] == 0)
                        {
                            loop = 1;
                            while (loop > 0)
                            {
                                pointeurCaractere++;
                                if (input[pointeurCaractere] == '[') loop++;
                                if (input[pointeurCaractere] == ']') loop--;
                            }
                        }
                        break;
                    case ']':
                        loop = 1;
                        while (loop > 0)
                        {
                            pointeurCaractere--;
                            if (input[pointeurCaractere] == '[') loop--;
                            if (input[pointeurCaractere] == ']') loop++;
                        }
                        pointeurCaractere--;
                        break;
                    case '.':
                        TbOut.Text += liste[pointeurBande];
                        TbOut.UpdateLayout();
                        break;
                    case ',':
                        FenetreInput fi = new FenetreInput();
                        fi.Closing += (o, e) => { liste[pointeurBande] = fi.input[0]; };
                        fi.ShowDialog();
                        break;
                    case ':':
                        liste[pointeurBande] = (char)value;
                        isReadingValues = false ;
                        break;
                    default:
                        if (isReadingValues)
                        {
                            if (Char.IsNumber(input[pointeurCaractere]))
                            {
                                value = value * 10 + (int) Char.GetNumericValue(input[pointeurCaractere]);
                            }
                            if (Char.IsWhiteSpace(input[pointeurCaractere]))
                            {
                                liste[pointeurBande] =(char) value;
                                if(input[pointeurCaractere + 1] != ':')
                                    pointeurBande++;
                                value = 0;
                            }
                        }
                        break;
                        
                }

                pointeurCaractere++;
                actualiserBande();

            }
            BtnClear.IsEnabled = true;
        }



        public void actualiserBande()
        {
            if (liste[pointeurBande - 5] < 33 && liste[pointeurBande -5] > 0)
                Case1.Content = (int)liste[pointeurBande - 5];
            else
                Case1.Content = liste[pointeurBande - 5];

            if (liste[pointeurBande - 4] < 33 && liste[pointeurBande - 4] > 0)
                Case2.Content = (int)liste[pointeurBande - 4];
            else
                Case2.Content = liste[pointeurBande - 4];

            if (liste[pointeurBande - 3] < 33 && liste[pointeurBande - 3] > 0)
                Case3.Content = (int)liste[pointeurBande - 3];
            else
                Case3.Content = liste[pointeurBande - 3];

            if (liste[pointeurBande - 2 ] < 33 && liste[pointeurBande - 2 ] > 0)
                Case4.Content = (int)liste[pointeurBande - 2 ];
            else
                Case4.Content = liste[pointeurBande - 2 ];

            if (liste[pointeurBande - 1] < 33 && liste[pointeurBande - 1] > 0)
                Case5.Content = (int)liste[pointeurBande - 1];
            else
                Case5.Content = liste[pointeurBande - 1];


            if (liste[pointeurBande] < 33 && liste[pointeurBande] > 0)
                Case6.Content = (int)liste[pointeurBande];
            else
                Case6.Content = liste[pointeurBande];


            if (liste[pointeurBande + 1] < 33 && liste[pointeurBande + 1] > 0)
                Case7.Content = (int)liste[pointeurBande + 1];
            else
                Case7.Content = liste[pointeurBande + 1];

            if (liste[pointeurBande +2] < 33 && liste[pointeurBande +2] > 0)
                Case8.Content = (int)liste[pointeurBande +2];
            else
                Case8.Content = liste[pointeurBande +2];

            if (liste[pointeurBande + 3] < 33 && liste[pointeurBande + 3] > 0)
                Case9.Content = (int)liste[pointeurBande + 3];
            else
                Case9.Content = liste[pointeurBande + 3];

            if (liste[pointeurBande + 4] < 33 && liste[pointeurBande + 4] > 0)
                Case10.Content = (int)liste[pointeurBande + 4];
            else
                Case10.Content = liste[pointeurBande + 4];

            if (liste[pointeurBande + 5] < 33 && liste[pointeurBande + 5] > 0)
                Case11.Content = (int)liste[pointeurBande + 5];
            else
                Case11.Content = liste[pointeurBande + 5];

            


        }

        public void nettoyerBande()
        {
            Case1.Content = 0;
            Case2.Content = 0;
            Case3.Content = 0;
            Case4.Content = 0;
            Case5.Content = 0;
            Case6.Content = 0;
            Case7.Content = 0;
            Case8.Content = 0;
            Case9.Content = 0;
            Case10.Content = 0;
            Case11.Content = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            //TbIn.Clear();
            TbOut.Clear();
            liste = new char[100000];
            actualiserBande();
            pointeurBande = 50000;
            pointeurCaractere = 0;

        }
    }
}
