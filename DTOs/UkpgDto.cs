using GasField.Models;

namespace GasField.DTOs
{
    public class UkpgDto
    {
        public double MonthlyProduction { get; set; } 
        public int WellCount { get; set; } 
        public List<Well>? Wells { get; set; }
    }
}
