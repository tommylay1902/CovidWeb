using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidWebApp.Models
{
    public class CaseData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Cases { get; set; }
        public int Deaths { get; set; }
        public int Tested { get; set; }
        public virtual City City{get; set;}
    }
}
