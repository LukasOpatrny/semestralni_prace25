using System.Collections.Generic;

namespace SemestralniPrace
{
    public class FilteredRecordsSummary
    {
        public List<ConsumptionRecord> Records { get; set; } = new List<ConsumptionRecord>();
        public Dictionary<string, double> SumByType { get; set; } = new Dictionary<string, double>();
        public int TotalCount { get; set; }
    }


}
