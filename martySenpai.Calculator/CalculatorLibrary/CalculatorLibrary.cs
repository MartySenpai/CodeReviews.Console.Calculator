using Newtonsoft.Json;
using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator
{
    public static List<Operation> operations = new();
    private static int id;

    public Calculator()
    {
        // Json writing.
    }

    public List<double> GetUserNumbers(List<double> oldResults)
    {
        string numInputs = "";

        Console.Write("Input one or more numbers seperated by spaces, and then press Enter: ");
        numInputs = Console.ReadLine();

        List<double> cleanNums = new();
        cleanNums = numInputs.Split(' ').Select(s =>
        {
            double i;
            return double.TryParse(s, out i) ? i : double.NaN;
        }).ToList();
        
        if (!double.IsNaN(oldResults[0]))
        {
            foreach( double oldResult in oldResults)
            {
                cleanNums = cleanNums.Prepend(oldResult).ToList();
            }
        }
        return cleanNums;
    }

    public double DoOperation(List<double> cleanNums, string calculatorOption)
    {
        double result = double.NaN;
        double tempResult = 0;

        switch (calculatorOption.Trim().ToLower())
        {
            case "a":
                foreach (double num in cleanNums)
                {
                    tempResult += num;
                }
                result = tempResult;
                LogOperations(cleanNums, "+", result);
                break;
            case "s":
                tempResult = cleanNums[0]; 

                foreach (double num in cleanNums.Skip(1))
                {
                    tempResult -= num;
                }
                result = tempResult;
                LogOperations(cleanNums, "-", result);
                break;
            case "m":
                tempResult = cleanNums[0];

                foreach (double num in cleanNums.Skip(1))
                {
                    tempResult *= num;
                }
                result = tempResult;
                LogOperations(cleanNums, "*", result);
                break;
            case "d":
                if (cleanNums[1] != 0)
                {
                    result = cleanNums[0] / cleanNums[1];
                }
                LogOperations(cleanNums, "/", result);
                break;
            case "r":
                foreach (double num in cleanNums)
                {
                    tempResult = Math.Sqrt(num);
                }
                result = tempResult;
                LogOperations(cleanNums, "\u221a", result);
                break;
            case "x":
                foreach (double num in cleanNums)
                {
                    tempResult = num * 10;
                }
                result = tempResult;
                LogOperations(cleanNums, "10x", result);
                break;
            case "p":
                tempResult = Math.Pow(cleanNums[0], cleanNums[1]);
                result = tempResult;
                LogOperations(cleanNums, "^", result);
                break;
            default:
                break;
        }
        return result;
    }

    public void LogOperations(List<double> cleanNums, string operand, double result)
    {
        id++;

        operations.Add(new Operation
        {
            Id = id,
            Nums = cleanNums,
            Operand = operand,
            Result = result
        });
    }

    public void showHistory()
    {
        Console.Clear();
        Console.WriteLine("Operations History");
        Console.WriteLine("------------------------------------");

        foreach (Operation operation in operations)
        {
            // Add For loop or similiar to enable list print.
            Console.Write($"{operation.Id}. ");

            for (int i = 0; i < operation.Nums.Count; i++)
            {
                if (operation.Nums.Count == 1)
                    Console.Write($" {operation.Operand}");
                
                Console.Write($"{operation.Nums[i]}");

                if (i < operation.Nums.Count - 1)
                    Console.Write($" {operation.Operand} ");
            }
            Console.Write ($" = {operation.Result}\n");
        }

        Console.WriteLine("------------------------------------\n");
    }

    public void Finish()
    {
    }
}