/*namespace GasField.Models
{
    public class Ukpg
    {
        public int Id { get; set; } 
        public double MonthlyProduction { get; set; } 
        public int WellCount { get; set; } 
        public List<Well>? Wells { get; set; }
    }
}*/
using System.ComponentModel.DataAnnotations.Schema;

namespace GasField.Models
{
    public class Ukpg
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public List<Well>? Wells { get; set; } 
        [NotMapped]
        public int WellCount => Wells?.Count ?? 0;
        [NotMapped]
        public double AverageExtraction
        {
            get
            {
                if (Wells == null || Wells.Count == 0)
                    return 0;
                return Wells.Average(w => w.Extraction);
            }
        }
    }
}
