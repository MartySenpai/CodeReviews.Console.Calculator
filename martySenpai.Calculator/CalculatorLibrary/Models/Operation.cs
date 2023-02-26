namespace CalculatorLibrary.Models
{
    public class Operation
    {
        public int Id { get; set; }

        public List<double> Nums { get; set; }

        public char Operand { get; set; }

        public double Result { get; set; }
    }
}
