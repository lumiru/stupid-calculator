using System.Windows;
using System.Windows.Controls;
using StupidCalculatorLib;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public KeyBinder _binder;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button click events
        /// </summary>
        /// <param name="sender">button clicked</param>
        /// <param name="e"></param>
        private void BT_Click(object sender, RoutedEventArgs e)
        {
            Run(((Button)sender).Content.ToString());
        }

        /// <summary>
        /// KeyDown event on TXT_CALCUL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT_CALCUL_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsAllowedChar(e.Key))
            {
                //Run();
            }
        }

        private void Run(string str)
        {
            if (str.Length == 1)
            {
                TXT_CALCUL.Text = _binder.pushButton(str[0]);
            }
        }

        private bool IsAllowedChar(Key key)
        {
            if (key.ToString().Contains("NumPad"))
            {
                return true;
            }

            return false;
        }
    }
}
