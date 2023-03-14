using Newtonsoft.Json;
using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator
{
    public static List<Operation> operations = new();
    private static int id;

    // Read log and assign corresponding data.
    JsonReader reader;
    JsonWriter writer;

    public Calculator(int totalSessions)
    {
        //StreamReader logfile = File.("Calculatorlog.json");
        //reader = new JsonTextReader("Calculatorlog.json");

        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;

        writer.WriteStartObject();
        writer.WritePropertyName("Total Sessions");
        writer.WriteValue(totalSessions);

        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    // Change parameter to array to allow 1 or 2 numbers for operation.
    public double DoOperation(List<double> cleanNums, string operant)
    {
        double result = double.NaN;
        double tempResult = 0;
        // writer.WriteStartObject();
        // writer.WritePropertyName("Operand1");
        // writer.WriteValue(cleanNum1);
        // writer.WritePropertyName("Operand2");
        // writer.WriteValue(cleanNum2);
        // writer.WritePropertyName("Operation");

        switch (operant.Trim().ToLower())
        {
            case "a":
                foreach (double num in cleanNums)
                {
                    tempResult += num;
                }
                result = tempResult;
                LogOperations(cleanNums, '+', result);
                // writer.WriteValue("Add");
                break;
            case "s":
                tempResult = cleanNums[0]; 

                foreach (double num in cleanNums.Skip(1))
                {
                    tempResult -= num;
                }
                result = tempResult;
                // writer.WriteValue("Subtract");
                // LogOperations(cleanNum1, cleanNum2, '-', result);
                break;
            case "m":
                tempResult = cleanNums[0];

                foreach (double num in cleanNums.Skip(1))
                {
                    tempResult *= num;
                }
                result = tempResult;
                // writer.WriteValue("Multiply");
                // LogOperations(cleanNum1, cleanNum2, '*', result);
                break;
            case "d":
                if (cleanNums[1] != 0)
                {
                    result = cleanNums[0] / cleanNums[1];
                    // writer.WriteValue("Divide");
                    // LogOperations(cleanNum1, cleanNum2, '/', result);
                }
                break;
            case "r":

                // Add rules for advanced operations.
                foreach (double num in cleanNums)
                {
                    tempResult = Math.Sqrt(num);
                }
                result = tempResult;
                break;
            case "p":
                tempResult = Math.Pow(cleanNums[0], cleanNums[1]);
                result = tempResult;
                break;
            default:
                break;
        }
        // writer.WritePropertyName("Result");
        // writer.WriteValue(result);
        // writer.WriteEndObject();

        return result;
    }

    public void LogOperations(List<double> cleanNums, char operand, double result)
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

    public static void showHistory()
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
                Console.Write($"{operation.Nums[i]}");

                if (i < operation.Nums.Count - 1)
                {
                    Console.Write($" {operation.Operand} ");
                }
            }
            
            Console.Write ($" = {operation.Result}\n");
        }

        Console.WriteLine("------------------------------------\n");
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}