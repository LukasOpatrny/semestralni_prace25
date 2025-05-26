using System;

namespace SemestralniPrace
{
    public class ConsumptionRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ConsumptionType { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
    }


}
