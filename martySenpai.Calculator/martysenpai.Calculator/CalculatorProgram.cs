using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace CalculatorProgram;

public class Menu
{
    Calculator calculator = new Calculator();

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

            string menuOptionSelected = Console.ReadLine();

            // Should I declare oldResults here?
            List<double> oldResults = new();
            oldResults.Add(double.NaN);

            switch (menuOptionSelected.Trim().ToLower())
            {
                case "h":
                    HistoryMenu();
                    break;
                case "c":
                    OperationMenu(oldResults);
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

    private void OperationMenu(List<double> oldResults)
    {
        bool endOperations = false;

        Console.Clear();
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endOperations)
        {

            List<double> cleanNums = calculator.GetUserNumbers(oldResults);

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.WriteLine("\tR - Square Root");
            Console.WriteLine("\tX - 10x");
            Console.WriteLine("\tP - Power");
            Console.Write("Your option? ");

            string calculatorOption = Console.ReadLine().Trim().ToLower();
            List<char> allowedOptions = new() { 'a', 's', 'm', 'd', 'r', 'x', 'p' };

            while (string.IsNullOrEmpty(calculatorOption) || !calculatorOption.All(allowedOptions.Contains))
            {
                Console.Write("Please enter a valid key: ");
                calculatorOption = Console.ReadLine();
            }

            try
            {
                double result = calculator.DoOperation(cleanNums, calculatorOption);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine($"\nYour result: {result:0.##}");
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
        calculator.showHistory();

        Console.WriteLine("Enter one or more IDs seperated by space to reuse results in a new calculation, or press any other key and Enter to continue");
        Console.WriteLine("Enter IDs: ");
        string selectedIDs = Console.ReadLine();

        while (!int.TryParse(selectedIDs.Replace(" ", ""), out _))
        {
            Console.WriteLine("IDs invalid, Please enter valid integers");
        }

        List<string> inputIDs = selectedIDs.Split(' ').ToList();
        List<int> numberIDs = inputIDs.Select(s => int.Parse(s)).ToList();

        List<double> oldResults = new();
        List<Operation> oldOperations = new();

        foreach (double numberID in numberIDs)
        {
            oldOperations.AddRange(Calculator.operations.Where(o => o.Id == numberID).ToList());
        }

        foreach (Operation oldOperation in oldOperations)
        {
            oldResults.Add(oldOperation.Result);
        }

        if (!oldResults.Any())
        {
            oldResults.Add(double.NaN);
        }

        try
        {
            if (double.IsNaN(oldResults[0]))
            {
                Console.WriteLine("No IDs selected, continuing...");
            }
            else
            {
                // add array parameter or list parameter.
                OperationMenu(oldResults);
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
