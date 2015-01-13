using System;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;

namespace BrainWork
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //scroll
        char[] liste = new char[100000];
        //head position, loop counter, actual char
        int pointeurBande = 50000, loop, pointeurCaractere = 0;
        // is the Run() function stopped
        bool isStopped = true;
        // char used in the brainfuck langage + ':'
        List<char> listeChar = new List<char>() { '+', '-', '>', '<', '[', ']', '.', ',', ':' };
        //Store the Cell (Label) used to display value
        List<Label> listeCases = new List<Label>();

        /// <summary>
        /// Add the Cell to the list listeCases
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            listeCases.Add(Case1);
            listeCases.Add(Case2);
            listeCases.Add(Case3);
            listeCases.Add(Case4);
            listeCases.Add(Case5);
            listeCases.Add(Case6);
            listeCases.Add(Case7);
            listeCases.Add(Case8);
            listeCases.Add(Case9);
            listeCases.Add(Case10);
            listeCases.Add(Case11);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (isStopped)
            {
                isStopped = false;
                pointeurCaractere = 0;
                run(TbIn.Text);
                BtnClear.IsEnabled = false;
                BtnStart.Content = "Stop";
                
            }
            else {
                BtnStart.Content = "Start";
                isStopped = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private async void run(string input)
        {
            bool isReadingValues = true;
            int value = 0;

            while (pointeurCaractere < input.Length && !isStopped)
            {
                if (listeChar.Contains(input[pointeurCaractere]))
                {
                    await Task.Delay((int)sliderDelay.Value + 1); //Timer non bloquant
                    isReadingValues = false;
                }

                switch (input[pointeurCaractere])
                {
                    case '+':
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
                        isReadingValues = false;
                        break;
                    default:
                        if (isReadingValues)
                        {
                            if (Char.IsNumber(input[pointeurCaractere]))
                            {
                                value = value * 10 + (int)Char.GetNumericValue(input[pointeurCaractere]);
                            }
                            if (Char.IsWhiteSpace(input[pointeurCaractere]))
                            {
                                liste[pointeurBande] = (char)value;
                                if (input[pointeurCaractere + 1] != ':')
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
            isStopped = true;
            BtnStart.Content = "Start";
        }



        public void actualiserBande()
        {
            int i = -5;
            foreach (Label item in listeCases)
            {
                if (liste[pointeurBande - i] < 33 && liste[pointeurBande - i] > 0)
                    item.Content = (int)liste[pointeurBande - i];
                else
                    item.Content = liste[pointeurBande - i];
                i++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            liste = new char[100000];
            pointeurBande = 50000;
            pointeurCaractere = 0;
            TbOut.Clear();
            actualiserBande();

        }
    }
}
