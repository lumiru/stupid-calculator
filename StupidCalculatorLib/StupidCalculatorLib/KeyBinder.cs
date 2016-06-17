using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StupidCalculatorLib
{
    public class KeyBinder
    {
        private bool _isResult;
        private bool _hasSign;

        /// <summary>
        /// True if text value is a compute result
        /// </summary>
        public bool isResult
        {
            get { return _isResult; }
        }

        private string _text;

        /// <summary>
        /// Current showed text
        /// </summary>
        public string text
        {
            get { return _text; }
        }

        private string _buffer;

        /// <summary>
        /// Buffer to prepare next compute
        /// </summary>
        public string buffer
        {
            get { return _buffer; }
        }

        private Operator _op;

        public KeyBinder()
        {
            _op = new Operator();
            _text = "";
            _buffer = "";
            _isResult = false;
            _hasSign = false;
        }

        /// <summary>
        /// To call each time a button is pushed
        /// </summary>
        /// <param name="c">The pushed button</param>
        /// <returns>The value to show</returns>
        public string pushButton(char c)
        {
            Operation op = Operation.NONE;

            if (char.IsNumber(c) || c == ',' || c == '.')
            {
                putNumber(c);
            }
            else
            {
                switch (c)
                {
                    case '+':
                        op = Operation.ADD;
                        break;
                    case '-':
                        op = Operation.SUBSTRACT;
                        break;
                    case 'x':
                    case 'X':
                    case '×':
                        op = Operation.MULTIPLY;
                        break;
                    case '/':
                    case '÷':
                        op = Operation.DIVIDE;
                        break;
                    case '=':
                        compute();
                        break;
                    case 'C':
                        clear();
                        break;
                    case 'E':
                        popChar();
                        break;
                }

                if (op != Operation.NONE)
                {
                    putOperation(op);
                }
            }

            return _text;
        }

        private void putOperation(Operation op)
        {
            // If an compute is shown
            if (_hasSign && _text.Length > 2)
            {
                // If there is no digit after the sign, we can replace it
                if (_text[_text.Length - 1] == ' ')
                {
                    _text = _text.Substring(0, _text.Length - 3);
                }
                // Else, the compute is run
                else
                {
                    compute();
                }
            }

            _op.operation = op;
            // If their is a number shown, save it
            if (_buffer.Length > 0)
            {
                if (!isResult)
                {
                    _op.addNumber(double.Parse(_buffer));
                }

                _buffer = "";
            }
            _text += " " + op2char(op) + " ";

            _hasSign = true; // There is a sign in the shown text
            _isResult = false; // The shown text is not a result anymore
        }

        private void putNumber(char n)
        {
            // Dots in numbers are commas in French
            if (n == '.')
            {
                n = ',';
            }
            // No need to edit a result so we reset it
            if (_isResult)
            {
                _buffer = "";
                _text = "";
            }
            // If comma is placed at first, we add a zero
            if (_buffer.Length == 0 && n == ',')
            {
                _buffer += "0";
                _text += "0";
            }
            // Replace first zero if possible
            else if (_buffer == "0" && char.IsNumber(n))
            {
                _buffer = "";
                _text = _text.Substring(0, _text.Length - 1);
            }

            // Nothing to do if there is already one
            if (n != ',' || _buffer.IndexOf(',') < 0)
            {
                _buffer += n;
                _text += n;

                if(_isResult) {
                    _op.operation = Operation.NONE;
                    _isResult = false;
                }
            }
        }

        private void compute()
        {
            if (_buffer.Length <= 0)
            {
                _buffer = "0";
            }
            _op.addNumber(double.Parse(_buffer));

            _text = _op.compute().ToString();
            _isResult = true;
            _hasSign = false;
        }

        private void clear()
        {
            _op.operation = Operation.NONE;
            _text = "";
            _buffer = "";
        }

        private void popChar()
        {
            if (_buffer.Length > 0)
            {
                if (_isResult)
                {
                    _op.operation = Operation.NONE;
                    _buffer = _text;
                    _isResult = false;
                }

                _text = _text.Substring(0, _text.Length - 1);
                _buffer = _buffer.Substring(0, _buffer.Length - 1);
            }
        }

        /// <summary>
        /// Translate Operation constante to its char value
        /// </summary>
        /// <param name="op">The Operation constant value</param>
        /// <returns>Operation sign</returns>
        public static char op2char(Operation op)
        {
            char c = '\0';

            switch (op)
            {
                case Operation.ADD:
                    c = '+';
                    break;
                case Operation.SUBSTRACT:
                    c = '-';
                    break;
                case Operation.MULTIPLY:
                    c = '×';
                    break;
                case Operation.DIVIDE:
                    c = '÷';
                    break;
            }

            return c;
        }
    }
}
