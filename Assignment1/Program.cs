using System;
using System.Collections.Generic;

namespace Assignment1
{
    // TODO Add supporting classes here

    public class Program
    {
        // Store the previous answer for the 'ans' feature
        private static double previousAnswer = 0;

        public static string ProcessCommand(string input)
        {
            try
            {
                // TODO Evaluate the expression and return the result
                ExpressionEvaluator evaluator = new ExpressionEvaluator(input, previousAnswer);
                double result = evaluator.Evaluate();
                previousAnswer = result; // Update the previous answer
                return result.ToString();
            }
            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }
        }

        static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                Console.WriteLine($"{input}={ProcessCommand(input)}");
            }
        }
    }
}