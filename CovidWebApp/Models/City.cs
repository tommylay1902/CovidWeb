using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidWebApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public int Population { get; set; }
        public string CityName { get; set; }
        public virtual List<CaseData> Data { get; set; }
        public void AddData(CaseData data) => Data.Add(data);
    }
}
