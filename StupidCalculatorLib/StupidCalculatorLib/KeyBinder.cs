﻿namespace StupidCalculatorLib
{
    public class KeyBinder
    {
        private bool _isResult;
        private bool _hasSign;

        public bool isResult
        {
            get { return _isResult; }
        }

        private string _text;

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        private string _buffer;

        private Operator _op;

        public KeyBinder()
        {
            _op = new Operator();
            _buffer = "";
            _isResult = false;
            _hasSign = false;
        }

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
            if (_hasSign && _text.Length > 2)
            {
                if (_text[_text.Length - 1] == ' ')
                {
                    _text = _text.Substring(0, _text.Length - 3);
                }
                else
                {
                    compute();
                }
            }

            _op.operation = op;
            if (_buffer.Length > 0)
            {
                if (!isResult)
                {
                    _op.addNumber(double.Parse(_buffer));
                }

                _buffer = "";
            }
            _text += " " + op2char(op) + " ";

            _hasSign = true;
            _isResult = false;
        }

        private void putNumber(char n)
        {
            if ((_isResult || _buffer.Length == 0) && (n == ',' || n == '.'))
            {
                if (_isResult)
                {
                    _buffer = "";
                    _text = "";
                }

                _buffer += "0";
                _text += "0";
            }
            else if (_buffer == "0" && char.IsDigit(n))
            {
                _buffer = "";
                _text = _text.Substring(0, _text.Length - 1);
            }

            _buffer += n;
            _text += n;

            if(_isResult) {
                _op.operation = Operation.NONE;
                _isResult = false;
            }
        }

        private void compute()
        {
            if (_buffer.Length > 0)
            {
                _op.addNumber(double.Parse(_buffer));
            }
            else
            {
                _op.addNumber(0);
            }

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
                _text = _text.Substring(0, _text.Length - 1);
                _buffer = _buffer.Substring(0, _buffer.Length - 1);
            }
        }

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
