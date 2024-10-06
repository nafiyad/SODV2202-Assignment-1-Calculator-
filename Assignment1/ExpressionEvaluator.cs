using System;
using System.Collections.Generic;

namespace Assignment1
{
    public class ExpressionEvaluator
    {
        private List<string> tokens;
        private double previousAnswer;

        public ExpressionEvaluator(string expression, double prevAnswer)
        {
            previousAnswer = prevAnswer;
            tokens = SplitExpression(expression.Replace(" ", "").Replace("ans", prevAnswer.ToString()));
        }

        public double Evaluate()
        {
            return CalculateExpression(tokens);
        }

        private List<string> SplitExpression(string expression)
        {
            List<string> parts = new List<string>();
            string currentNumber = "";

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsDigit(c) || c == '.')
                {
                    currentNumber += c;
                }
                else if (c == '-' && (i == 0 || expression[i - 1] == '(' || "+-*/".Contains(expression[i - 1].ToString())))
                {
                    currentNumber += c;
                }
                else
                {
                    if (currentNumber != "")
                    {
                        parts.Add(currentNumber);
                        currentNumber = "";
                    }
                    parts.Add(c.ToString());
                }
            }

            if (currentNumber != "")
            {
                parts.Add(currentNumber);
            }

            return parts;
        }

        private double CalculateExpression(List<string> parts)
        {
            // Handle parentheses
            while (parts.Contains("("))
            {
                int openIndex = parts.LastIndexOf("(");
                int closeIndex = parts.IndexOf(")", openIndex);

                if (closeIndex == -1)
                {
                    throw new ArgumentException("Mismatched parentheses");
                }

                List<string> subExpression = parts.GetRange(openIndex + 1, closeIndex - openIndex - 1);
                double subResult = CalculateExpression(subExpression);

                parts.RemoveRange(openIndex, closeIndex - openIndex + 1);
                parts.Insert(openIndex, subResult.ToString());
            }

            // Handle multiplication and division
            for (int i = 1; i < parts.Count - 1; i++)
            {
                if (parts[i] == "*" || parts[i] == "/")
                {
                    double left = double.Parse(parts[i - 1]);
                    double right = double.Parse(parts[i + 1]);
                    double result;

                    if (parts[i] == "*")
                        result = left * right;
                    else
                        result = left / right;

                    parts[i - 1] = result.ToString();
                    parts.RemoveRange(i, 2);
                    i--;
                }
            }

            // Handle addition and subtraction
            double finalResult = double.Parse(parts[0]);
            for (int i = 1; i < parts.Count - 1; i += 2)
            {
                double nextNumber = double.Parse(parts[i + 1]);
                if (parts[i] == "+")
                    finalResult += nextNumber;
                else if (parts[i] == "-")
                    finalResult -= nextNumber;
            }

            return finalResult;
        }
    }
}