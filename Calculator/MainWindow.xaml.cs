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
        public static bool Erase;
        public static Operator Operator;
        public static Operation lastOperation;

        public MainWindow()
        {
            InitializeComponent();
            Erase = false;
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
            if (Operator == null)
            {
                Operator = new Operator();
            }

            switch (str)
            {
                case "CE":
                    TXT_CALCUL.Text = "";
                    break;
                case "C":
                    TXT_CALCUL.Text = "";
                    break;

                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    // if erase = true, save current value and clear textbox
                    if (Erase)
                    {
                        TXT_CALCUL.Text = "";
                        Erase = false;
                    }
                    // add number to textbox
                    TXT_CALCUL.Text += str;
                    break;
                case ",":
                    // if erase = true, save current value and clear textbox
                    if (Erase)
                    {
                        TXT_CALCUL.Text = "";
                        Erase = false;
                    }
                    // if textbox not already contains decimal, and minimum one number exist
                    if (TXT_CALCUL.Text == "")
                    {
                        TXT_CALCUL.Text = "0";
                    }
                    if (!TXT_CALCUL.Text.Contains(","))
                    {
                        TXT_CALCUL.Text += str;
                    }
                    break;

                case "X":
                    Operator.operation = Operation.MULTIPLY;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += str;
                    Erase = true;
                    break;
                case "-":
                    Operator.operation = Operation.SUBSTRACT;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += str;
                    Erase = true;
                    break;
                case "+":
                    Operator.operation = Operation.ADD;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += str;
                    Erase = true;
                    break;
                case "/":
                    Operator.operation = Operation.DIVIDE;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += str;
                    Erase = true;
                    break;
                case "=":
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text = Operator.compute().ToString();
                    Erase = true;
                    break;

                default:
                    break;
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
