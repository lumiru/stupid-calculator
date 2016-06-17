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

        public MainWindow()
        {
            InitializeComponent();
            Erase = false;

            this.PreviewKeyDown += TXT_CALCUL_PreviewKeyDown;
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
        private void TXT_CALCUL_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string temp = GetKeyChar(e.Key);

            e.Handled = true;
            if (TXT_CALCUL.Text.Length > 0)
            {
                TXT_CALCUL.Select(TXT_CALCUL.Text.Length - 1, 1);
            }

            if (temp != null)
            {
                Run(temp);
            }
        }

        private void Run(string str)
        {
            if (str == "EFF" && TXT_CALCUL.Text != "")
            {
                if (!char.IsNumber(TXT_CALCUL.Text[TXT_CALCUL.Text.Length - 1]))
                {
                    Operator.operation = Operation.NONE;
                    Erase = false;
                }
                TXT_CALCUL.Text = TXT_CALCUL.Text.Substring(0, TXT_CALCUL.Text.Length - 1);
            }

            if (TXT_CALCUL.Text.Length < 16)
            {
                if (Operator == null)
                {
                    Operator = new Operator();
                }

                // if erase = true, save current value and clear textbox
                if (Erase)
                {
                    switch (str)
                    {
                        case "+":
                        case "-":
                        case "/":
                        case "X":
                        case "=":
                            if (!char.IsNumber(TXT_CALCUL.Text[TXT_CALCUL.Text.Length - 1]))
                            {
                                TXT_CALCUL.Text = TXT_CALCUL.Text.Substring(0, TXT_CALCUL.Text.Length - 1);
                            }
                            break;
                        default:
                            TXT_CALCUL.Text = "";
                            Erase = false;
                            break;
                    }
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
                        if (str == "0" && TXT_CALCUL.Text != "" && TXT_CALCUL.Text[TXT_CALCUL.Text.Length - 1] == '0')
                        {
                            break;
                        }
                        // add number to textbox
                        TXT_CALCUL.Text += str;
                        break;
                    case ",":
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
                        if (Operator != null && TXT_CALCUL.Text != "")
                        {
                            Operator.operation = Operation.MULTIPLY;
                            Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                            TXT_CALCUL.Text += str;
                            Erase = true;
                        }
                        break;
                    case "-":
                        if (Operator != null && TXT_CALCUL.Text != "")
                        {
                            Operator.operation = Operation.SUBSTRACT;
                            Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                            TXT_CALCUL.Text += str;
                            Erase = true;
                        }
                        break;
                    case "+":
                        if (Operator != null && TXT_CALCUL.Text != "")
                        {
                            Operator.operation = Operation.ADD;
                            Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                            TXT_CALCUL.Text += str;
                            Erase = true;
                        }
                        break;
                    case "/":
                        if (Operator != null && TXT_CALCUL.Text != "")
                        {
                            Operator.operation = Operation.DIVIDE;
                            Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                            TXT_CALCUL.Text += str;
                            Erase = true;
                        }
                        break;
                    case "=":
                        if (Operator != null && TXT_CALCUL.Text != "")
                        {
                            Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                            TXT_CALCUL.Text = Operator.compute().ToString();
                            Erase = true;
                        }
                        break;

                    default:
                        break;
                }

            }
        }

        private string GetKeyChar(Key key)
        {
            switch (key)
            {
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    return key.ToString().Substring(6);
                case Key.Multiply:
                    return "X";
                case Key.Add:
                    return "+";
                case Key.Subtract:
                    return "-";
                case Key.Decimal:
                    return ",";
                case Key.Divide:
                    return "/";
                case Key.Enter:
                    return "=";
                case Key.Delete:
                    return "C";
                case Key.Back:
                    return "EFF";
                default:
                    return null;
            }
        }
    }
}
