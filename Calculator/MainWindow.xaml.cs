﻿using System.Windows;
using System.Windows.Controls;
using StupidCalculatorLib;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool Erase;
        public static Operator Operator;
        public static Operation lastOperator;

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
            if (Operator == null)
            {
                Operator = new Operator();
            }

            // if erase = true, save current value and clear textbox
            if (Erase)
            {
                TXT_CALCUL.Text = "";
                Erase = false;
            }

            switch (((Button)sender).Content.ToString())
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
                    // add number to textbox
                    TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    break;
                case ",":
                    // if textbox not already contains decimal, and minimum one number exist
                    if (TXT_CALCUL.Text == "")
                    {
                        TXT_CALCUL.Text = "0";
                    }
                    if (!TXT_CALCUL.Text.Contains(","))
                    {
                        TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    }
                    break;

                case "X":
                    Operator.operation = Operation.MULTIPLY;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    Erase = true;
                    break;
                case "-":
                    Operator.operation = Operation.SUBSTRACT;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    Erase = true;
                    break;
                case "+":
                    Operator.operation = Operation.ADD;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    Erase = true;
                    break;
                case "/":
                    Operator.operation = Operation.DIVIDE;
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text += ((Button)sender).Content.ToString();
                    Erase = true;
                    break;
                case "=":
                    Operator.addNumber(double.Parse(TXT_CALCUL.Text));
                    TXT_CALCUL.Text = Operator.compute().ToString();
                    break;

                default:
                    break;
            }
        }
    }
}
