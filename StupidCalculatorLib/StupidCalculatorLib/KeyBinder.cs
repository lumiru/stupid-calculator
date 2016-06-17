﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StupidCalculatorLib
{
    public class KeyBinder
    {
        private bool _isResult;

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
            _op.operation = op;
            _op.addNumber(double.Parse(_buffer));
            _buffer = "";
            _text += " " + op2char(op) + " ";
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

            _buffer += n;
            _text += n;

            _isResult = false;
        }

        private void compute()
        {
            _op.addNumber(double.Parse(_buffer));
            _buffer = _op.compute().ToString();
            _text = _buffer;
            _isResult = true;
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
