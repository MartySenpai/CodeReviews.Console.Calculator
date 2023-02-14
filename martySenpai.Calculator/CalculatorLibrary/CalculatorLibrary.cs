using Newtonsoft.Json;
using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator
{
    public static List<Operation> operations = new();

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

    public double DoOperation(double num1, double num2, string operand)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (operand.Trim().ToLower())
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    writer.WriteValue("Divide");
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

    public void LogOperations(double cleanNum1, double cleanNum2, string operand, double result)
    {
        operations.Add(new Operation
        {
            Num1 = cleanNum1,
            Num2 = cleanNum2,
            Operand = operand,
            Result = result
        });
    }

    public static void History()
    {
        Console.Clear();
        Console.WriteLine("Operations History");
        Console.WriteLine("------------------------------------");

        foreach (Operation operation in operations)
        {
            Console.WriteLine($"{operation.Num1} {operation.Operand} {operation.Num2} = {operation.Result}");
        }

        Console.WriteLine("------------------------------------\n");
        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadLine();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}