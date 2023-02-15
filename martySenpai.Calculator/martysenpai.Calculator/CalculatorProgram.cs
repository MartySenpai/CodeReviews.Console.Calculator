using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace CalculatorProgram;

public class Menu
{
    public void MainMenu()
    {
        bool endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tH - History");
            Console.WriteLine("\tC - Calculator");
            Console.WriteLine("\tQ - Quit");
            Console.Write("\nYour option? ");

            string optionSelected = Console.ReadLine();

            switch (optionSelected.Trim().ToLower())
            {
                case "h":
                    Calculator.showHistory();
                    break;
                case "c":
                    OperationMenu();
                    break;
                case "q":
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid input, press enter to continue and enter a valid key.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // Implement accept array/lists as parameter.
    public void OperationMenu()
    {
        bool endOperations = false;

        Console.Clear();
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        int totalSessions = 0;
        totalSessions++;

        Calculator calculator = new Calculator(totalSessions);

        int totalOperations = 0;

        while (!endOperations)
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
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.Write("Your option? ");

            string operand = Console.ReadLine().Trim().ToLower();
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'B' and Enter to return to the main menu, or press any other key and Enter to continue operations: ");
            if (Console.ReadLine().Trim().ToLower() == "b") endOperations = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }

    public void OperationMenu(double cleanNum1, double cleanNum2)
    {
        bool endOperations = false;

        Console.Clear();
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        int totalSessions = 0;
        totalSessions++;

        Calculator calculator = new Calculator(totalSessions);

        int totalOperations = 0;

        while (!endOperations)
        {
            //string numInput1 = "";
            //string numInput2 = "";
            double result = 0;

            //Console.Write("Type a number, and then press Enter: ");
            //numInput1 = Console.ReadLine();

            //double cleanNum1 = 0;
            //while (!double.TryParse(numInput1, out cleanNum1))
            //{
            //    Console.Write("This is not a valid input. Please enter an integer value: ");
            //    numInput1 = Console.ReadLine();
            //}

            //Console.Write("Type another number, and then press Enter: ");
            //numInput2 = Console.ReadLine();

            //double cleanNum2 = 0;
            //while (!double.TryParse(numInput2, out cleanNum2))
            //{
            //    Console.Write("This is not a valid input. Please enter an integer value: ");
            //    numInput2 = Console.ReadLine();
            //}

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.Write("Your option? ");

            string operand = Console.ReadLine().Trim().ToLower();
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'B' and Enter to return to the main menu, or press any other key and Enter to continue operations: ");
            if (Console.ReadLine().Trim().ToLower() == "b") endOperations = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }

    public void HistoryMenu()
    {
        // Call ShowHistory.
        // Use LINQ to get results with ID.
        Console.WriteLine("Enter an ID to reuse the result in a new calculation, or press any other key and Enter to continue");
        Console.WriteLine("Enter ID: ");
        string selectedId1 = Console.ReadLine().Trim();

        int cleanId1 = 0;
        while (!Int32.TryParse(selectedId1, out cleanId1))
        {
            Console.Write("This is not a valid ID. Please enter an valid number: ");
            selectedId1 = Console.ReadLine();
        }

        Console.WriteLine("Enter a second ID to reuse the result in a new calculation, or press any other key and Enter to continue");
        Console.WriteLine("Enter another ID: ");
        string selectedId2 = Console.ReadLine().Trim();

        int cleanId2 = 0;
        while (!Int32.TryParse(selectedId2, out cleanId2))
        {
            Console.Write("This is not a valid ID. Please enter an valid number: ");
            selectedId2 = Console.ReadLine();
        }

        double oldResult1 = double.NaN;
        double oldResult2 = double.NaN;

        foreach (Operation operation in Calculator.operations)
        {
            if (cleanId1 == operation.Id)
            {
                oldResult1 = operation.Result;
            }

            if (cleanId2 == operation.Id)
            {
                oldResult2 = operation.Result;
            }
        }

        try
        {
            if (double.IsNaN(oldResult1))
            {
                Console.WriteLine("No IDs selected, continuing...");
            }
            else
            {
                OperationMenu(oldResult1, oldResult2);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }


        Console.Write("Press 'D' to delete history, or press any other key and Enter to go back to the main menu: ");
        if (Console.ReadLine().Trim().ToLower() == "d")
            Calculator.operations.Clear();

        // allow the user to type the ID of two calculations to reuse them in a new calculation.
    }
}
