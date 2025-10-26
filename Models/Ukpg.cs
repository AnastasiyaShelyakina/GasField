namespace GasField.Models
{
    public class Ukpg
    {
        public int Id { get; set; } 
        public double MonthlyProduction { get; set; } // добыча за месяц
        public int WellCount { get; set; } // число скважин, принадлежащих этому УКПГ

        public List<Well>? Wells { get; set; }
    }
}
