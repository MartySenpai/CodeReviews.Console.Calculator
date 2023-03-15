namespace CalculatorLibrary.Models
{
    public class Operation
    {
        // Ids get reassigned after new operation from old results.
        public int Id { get; set; }

        public List<double> Nums { get; set; }

        public string Operand { get; set; }

        public double Result { get; set; }
    }
}
