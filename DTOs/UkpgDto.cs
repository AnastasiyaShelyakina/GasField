using GasField.Models;

namespace GasField.DTOs
{
    public class UkpgDto
    {
        public int Id { get; set; }
        public double MonthlyProduction { get; set; } 
        public int WellCount { get; set; } 
        public List<Well>? Wells { get; set; }
    }

    public class UpdateUkpgDto
    {
        public double MonthlyProduction { get; set; }
        public int WellCount { get; set; }

    }
}
