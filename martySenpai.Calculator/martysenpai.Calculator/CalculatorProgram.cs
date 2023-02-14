using CalculatorLibrary;

namespace CalculatorProgram;

public class Menu
{
    public void OperationMenu()
    {
        bool endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        int totalSessions = 0;
        totalSessions++;

        Calculator calculator = new Calculator(totalSessions);

        int totalOperations = 0;

        while (!endApp)
        {
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not a valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not a valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string operand = Console.ReadLine();
            List<char> allowedOperands = new() { 'a', 's', 'm', 'd' };

            while (string.IsNullOrEmpty(operand) || !operand.All(allowedOperands.Contains))
            {
                Console.Write("Please enter a valid key: ");
                operand = Console.ReadLine();
            }

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, operand);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine($"\nYour result: {result:0.##}");

                    totalOperations++;
                    Console.WriteLine($"\nTotal operations this session: {totalOperations}");

                    calculator.LogOperations(cleanNum1, cleanNum2, operand);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}
