using Newtonsoft.Json;
using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator
{
    public static List<Operation> operations = new();
    private int id = 0;

    // Read log and assign corresponding data.
    JsonReader reader;
    JsonWriter writer;

    public Calculator(int totalSessions)
    {
        //StreamReader logfile = File.("Calculatorlog.json");
        //reader = new JsonTextReader("Calculatorlog.json");

        StreamWriter logFile = File.AppendText("calculatorlog.json");
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
        // writer.WriteStartObject();
        // writer.WritePropertyName("Operand1");
        // writer.WriteValue(num1);
        // writer.WritePropertyName("Operand2");
        // writer.WriteValue(num2);
        // writer.WritePropertyName("Operation");

        switch (operant.Trim().ToLower())
        {
            case "a":
                foreach (double num in cleanNums)
                {
                    result += num;
                }
                writer.WriteValue("Add");
                // LogOperations(num1, num2, '+', result);
                break;
            case "s":
                foreach (double num in cleanNums)
                {
                    result -= num;
                }
                writer.WriteValue("Subtract");
                // LogOperations(num1, num2, '-', result);
                break;
            case "m":
                foreach (double num in cleanNums)
                {
                    result += num;
                }
                writer.WriteValue("Multiply");
                // LogOperations(num1, num2, '*', result);
                break;
            case "d":
                if (cleanNums[1] != 0)
                {
                    result = cleanNums[0] / cleanNums[1];
                    writer.WriteValue("Divide");
                    // LogOperations(num1, num2, '/', result);
                }
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public double DoOperation(double cleanNum1, double cleanNum2, string operant)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(cleanNum1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(cleanNum2);
        writer.WritePropertyName("Operation");

        switch (operant.Trim().ToLower())
        {
            case "a":
                result = cleanNum1 + cleanNum2;
                writer.WriteValue("Add");
                // LogOperations(num1, num2, '+', result);
                break;
            case "s":
                result = cleanNum1 + cleanNum2;
                writer.WriteValue("Subtract");
                // LogOperations(num1, num2, '-', result);
                break;
            case "m":
                result = cleanNum1 + cleanNum2;
                writer.WriteValue("Multiply");
                // LogOperations(num1, num2, '*', result);
                break;
            case "d":
                if (cleanNum2!= 0)
                {
                    result = cleanNum1 / cleanNum2;
                    writer.WriteValue("Divide");
                    // LogOperations(num1, num2, '/', result);
                }
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void LogOperations(double num1, double num2, char operand, double result)
    {
        id++;

        operations.Add(new Operation
        {
            Id = id,
            Num1 = num1,
            Num2 = num2,
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
            Console.WriteLine($"{operation.Id}. {operation.Num1} {operation.Operand} {operation.Num2} = {operation.Result}");
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