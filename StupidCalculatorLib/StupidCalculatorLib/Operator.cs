using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StupidCalculatorLib
{
    public class Operator
    {
        private List<double> _numberHistory;

        private Operation _operation;

	    public Operation operation
	    {
		    get { return _operation;}
		    set { _operation = value;}
	    }

        public Operator()
        {
            _numberHistory = new List<double>();
        }

        /// <summary>
        /// Save entered number to history
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public void addNumber(double x)
        {
            _numberHistory.Add(x);
        }

        /// <summary>
        /// Use added numbers and operation to return computation
        /// </summary>
        /// <returns>Computed value</returns>
        public double compute() {
            double computed = 0;
            int historyLength = _numberHistory.Count();

            if (historyLength > 1)
            {
                computed = applyOperation(_numberHistory[historyLength - 2], _numberHistory[historyLength - 1]);
                _numberHistory.Add(computed);
            }
            else
            {
                computed = _numberHistory[0];
            }

            _operation = Operation.NONE;

            return computed;
        }

        private double applyOperation(double prev, double current)
        {
            switch (_operation)
            {
                case Operation.ADD:
                    current = prev + current;
                    break;
                case Operation.SUBSTRACT:
                    current = prev - current;
                    break;
                case Operation.DIVIDE:
                    current = prev / current;
                    break;
                case Operation.MULTIPLY:
                    current = prev * current;
                    break;
            }

            return current;
        }
    }
}
