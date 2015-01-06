using System.Windows;
using System.Windows.Controls;

namespace BrainWork
{
    /// <summary>
    /// Logique d'interaction pour FenetreInput.xaml
    /// </summary>
    public partial class FenetreInput : Window
    {
        public string input;

        public FenetreInput()
        {
            InitializeComponent();
            TBInput.Focus();
            //TBInput.AcceptsReturn = BtnValider;
            BtnValider.IsDefault = true;
            
        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            input = TBInput.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
