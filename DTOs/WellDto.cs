namespace GasField.DTOs
{
    public class WellDto
    {
        public int Id { get; set; }
        public double RoofGvk { get; set; }  // Кровля ГВК
        public double BottomGvk { get; set; } // Подошва ГВК
        public double RoofPerforation { get; set; } // Кровля перфорации
        public double BottomPerforation { get; set; } // Подошва перфорации
        public double WaterCut { get; set; }
        public int UkpgId { get; set; }
    }

    public class UpdateWellDto 
    {
        public double RoofGvk { get; set; }  // Кровля ГВК
        public double BottomGvk { get; set; } // Подошва ГВК
        public double RoofPerforation { get; set; } // Кровля перфорации
        public double BottomPerforation { get; set; } // Подошва перфорации
        public int UkpgId { get; set; }

    }

}
