namespace GasField.Models
{
    public class Ukpg
    {
        public int Id { get; set; } 
        public double MonthlyProduction { get; set; } 
        public int WellCount { get; set; } 
        public List<Well>? Wells { get; set; }
    }
}
