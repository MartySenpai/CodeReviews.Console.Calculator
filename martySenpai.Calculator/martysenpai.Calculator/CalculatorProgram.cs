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

            List<double> oldResults = new();
            oldResults.Add(double.NaN);

            switch (optionSelected.Trim().ToLower())
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

        int totalSessions = 0;
        totalSessions++;

        Calculator calculator = new Calculator(totalSessions);

        int totalOperations = 0;

        while (!endOperations)
        {
            string numInputs = "";
            double result = 0;

            Console.Write("Input one or more numbers seperated by spaces, and then press Enter: ");
            numInputs = Console.ReadLine();

            List<double> cleanNums = new();

            // Research LinQ
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
            

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.Write("Your option? ");

            string operant = Console.ReadLine().Trim().ToLower();
            List<char> allowedOperands = new() { 'a', 's', 'm', 'd' };

            while (string.IsNullOrEmpty(operant) || !operant.All(allowedOperands.Contains))
            {
                Console.Write("Please enter a valid key: ");
                operant = Console.ReadLine();
            }

            try
            {
                result = calculator.DoOperation(cleanNums, operant);
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
        Calculator.showHistory();

        // Use LINQ to get results with ID.
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

        // LinQ test/rethink grabbing of old results, grab whole list?

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
