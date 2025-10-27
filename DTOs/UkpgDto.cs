using GasField.Models;

namespace GasField.DTOs
{
    public class UkpgDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int WellCount { get; set; }

        public double AverageExtraction { get; set; }
        //public double MonthlyProduction { get; set; } 
        //public int WellCount { get; set; } 
        public List<WellDto>? Wells { get; set; }
    }

    public class UpdateUkpgDto
    {
        //public double MonthlyProduction { get; set; }
        //public int WellCount { get; set; }
        public string Name { get; set; } = string.Empty;


    }
}
