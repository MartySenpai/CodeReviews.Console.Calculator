namespace CalculatorLibrary.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public double Num1 { get; set; }

        public double Num2 { get; set; }

        public char Operand { get; set; }

        public double Result { get; set; }
    }
}
