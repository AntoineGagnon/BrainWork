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
        char[] scroll = new char[100000];
        //head position, loop counter, actual char
        int pointerScroll = 50000, loop, pointerChar = 0;
        // is the Run() function stopped
        bool isStopped = true;
        // char used in the brainfuck langage + ':'
        List<char> operatorChar = new List<char>() { '+', '-', '>', '<', '[', ']', '.', ',', ':' };
        //Store the Cell (Label) used to display value
        List<Label> listCells = new List<Label>();

        /// <summary>
        /// Add the Cell to the list listeCases
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in gridCells.Children)
	        {
                if(item.GetType() == Cell1.GetType()){
                    listCells.Add((Label) item);
                }
	        }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (isStopped && TbIn.Text != "")
            {
                isStopped = false;
                pointerChar = 0;
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

            while (pointerChar < input.Length && !isStopped)
            {
                if (operatorChar.Contains(input[pointerChar]))
                {
                    await Task.Delay((int)sliderDelay.Value + 1); //Timer non bloquant
                    isReadingValues = false;
                }
                switch (input[pointerChar])
                {
                    case '+':
                        scroll[pointerScroll]++;
                        break;
                    case '-':
                        if (scroll[pointerScroll] == 0)
                            scroll[pointerScroll] = (char)255;
                        else
                            scroll[pointerScroll]--;
                        break;
                    case '>':
                        pointerScroll++;
                        break;
                    case '<':
                        pointerScroll--;
                        break;
                    case '[':
                        if (scroll[pointerScroll] == 0)
                        {
                            loop = 1;
                            while (loop > 0)
                            {
                                pointerChar++;
                                if (input[pointerChar] == '[') loop++;
                                if (input[pointerChar] == ']') loop--;
                            }
                        }
                        break;
                    case ']':
                        loop = 1;
                        while (loop > 0)
                        {
                            pointerChar--;
                            if (input[pointerChar] == '[') loop--;
                            if (input[pointerChar] == ']') loop++;
                        }
                        pointerChar--;
                        break;
                    case '.':
                        TbOut.Text += scroll[pointerScroll];
                        TbOut.UpdateLayout();
                        break;
                    case ',':
                        FenetreInput fi = new FenetreInput();
                        fi.Closing += (o, e) => { scroll[pointerScroll] = fi.input[0]; };
                        fi.ShowDialog();
                        break;
                    case ':':
                        scroll[pointerScroll] = (char)value;
                        isReadingValues = false;
                        break;
                    default:
                        if (isReadingValues)
                        {
                            if (Char.IsNumber(input[pointerChar]))
                            {
                                value = (value * 10) + (int)Char.GetNumericValue(input[pointerChar]);
                            }
                            if (Char.IsWhiteSpace(input[pointerChar]))
                            {
                                scroll[pointerScroll] = (char)value;
                                if (input[pointerChar + 1] != ':')
                                    pointerScroll++;
                                value = 0;
                            }
                        }
                        break;
                }
                pointerChar++;
                refreshScroll();
            }
            BtnClear.IsEnabled = true;
            isStopped = true;
            BtnStart.Content = "Start";
        }

        public void refreshScroll()
        {
            int i = -5;
            foreach (Label item in listCells)
            {
                if (scroll[pointerScroll - i] < 33 && scroll[pointerScroll - i] > 0)
                    item.Content = (int)scroll[pointerScroll - i];
                else
                    item.Content = scroll[pointerScroll - i];
                i++;
            }
        }

        /// <summary>
        /// Will clear the output and reset the scroll to its initial state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            scroll = new char[100000];
            pointerScroll = 50000;
            pointerChar = 0;
            TbOut.Clear();
            refreshScroll();
        }
    }
}
