
using System.ComponentModel.DataAnnotations.Schema;

namespace GasField.Models
{
    public class Well
    {
        public int Id { get; set; }
        public double RoofGvk { get; set; }  // Кровля ГВК
        public double BottomGvk { get; set; } // Подошва ГВК
        public double RoofPerforation { get; set; } // Кровля перфорации
        public double BottomPerforation { get; set; } // Подошва перфорации
        public double Extraction { get; set; }
        public int UkpgId { get; set; } // Код УКПГ

        [ForeignKey("UkpgId")]
        public Ukpg? Ukpg { get; set; }

        [NotMapped]
        public double WaterCut
        {
            get
            {
                // 100% — если кровля ГВК ниже кровли перфорации (вода полностью в зоне перфорации)
                if (RoofGvk < RoofPerforation)
                    return 100.0;

                // 0% — если кровля ГВК выше подошвы перфорации (в зоне перфорации нет воды)
                else if (RoofGvk > BottomPerforation)
                    return 0.0;

                // Пропорциональное значение — частичная обводненность
                else
                {
                    double denominator = BottomPerforation - RoofPerforation;
                    if (denominator == 0)
                        return 0.0; // защита от деления на ноль

                    double ratio = (BottomPerforation - RoofGvk) / denominator;
                    double result = ratio * 100.0;

                    // ограничим диапазон на случай погрешностей расчёта
                    if (result < 0) result = 0;
                    if (result > 100) result = 100;

                    return result;
                }
            }
        }


    }
}
