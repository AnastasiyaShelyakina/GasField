namespace GasField.DTOs
{
    public class WellDto
    {
        public double RoofGvk { get; set; }  // Кровля ГВК
        public double BottomGvk { get; set; } // Подошва ГВК
        public double RoofPerforation { get; set; } // Кровля перфорации
        public double BottomPerforation { get; set; } // Подошва перфорации
        public double? WaterCut { get; set; } // Обводненность
        public int UkpgId { get; set; }
    }
}
