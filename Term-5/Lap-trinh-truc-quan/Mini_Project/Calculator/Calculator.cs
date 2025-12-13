using System;

namespace Calculator
{
    internal class Calculator
    {
        private double _firstOperand = 0;
        private string _currentOperation = string.Empty;

        public void SetFirstOperand(double value)
        {
            _firstOperand = value;
        }

        public void SetOperation(string operation)
        {
            if (string.IsNullOrEmpty(operation))
            {
                throw new ArgumentException("Operation cannot be null or empty.", nameof(operation));
            }
            _currentOperation = operation;
        }

        public double Calculate(double secondOperand)
        {
            double result = 0;

            switch (_currentOperation)
            {
                case "+":
                    result = _firstOperand + secondOperand;
                    break;
                case "-":
                    result = _firstOperand - secondOperand;
                    break;
                case "*":
                    result = _firstOperand * secondOperand;
                    break;
                case "/":
                    if (secondOperand == 0)
                        throw new DivideByZeroException("Cannot divide by zero.");
                    result = _firstOperand / secondOperand;
                    break;
                case "%":
                    if (secondOperand == 0)
                        throw new DivideByZeroException("Cannot calculate modulus with zero.");
                    result = _firstOperand % secondOperand;
                    break;
            }

            _firstOperand = result;
            return result;
        }

        public void Clear()
        {
            _firstOperand = 0;
            _currentOperation = string.Empty;
        }

        public string GetOperation() => _currentOperation;
        public double GetFirstOperand() => _firstOperand;
    }
}
