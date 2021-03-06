﻿using System.Windows;
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
            this._binder = new KeyBinder();
            this.PreviewKeyDown += TXT_CALCUL_PreviewKeyDown;
        }

        /// <summary>
        /// Button click events
        /// </summary>
        /// <param name="sender">button clicked</param>
        /// <param name="e"></param>
        private void BT_Click(object sender, RoutedEventArgs e)
        {
            Run(((Button)sender).Tag.ToString());
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
            TXT_CALCUL.ScrollToHorizontalOffset(10000000);

            if (_binder.buffer.Length < 16)
            {
                if (str.Length == 1)
                {
                    TXT_CALCUL.Text = _binder.pushButton(str[0]);
                }
                else if (str == "CE" || str == "EFF")
                {
                    TXT_CALCUL.Text = _binder.pushButton('E');
                }
            }
            else 
            {
                switch (str[0])
                {
                    case '+':
                    case '-':
                    case 'x':
                    case 'X':
                    case '×':
                    case '/':
                    case '÷':
                    case '=':
                    case 'C':
                    case 'E':
                        TXT_CALCUL.Text = _binder.pushButton(str[0]);
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
                    return "×";
                case Key.Add:
                    return "+";
                case Key.Subtract:
                    return "-";
                case Key.Decimal:
                    return ",";
                case Key.Divide:
                    return "÷";
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
