using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public class Score
    {
        public string postcode { get; set; }
        public string council_name { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string week { get; set; }
        public string drunk_score { get; set; }
        public string speeding_score { get; set; }
        public string distraction_score { get; set; }
        public string fatigue_score { get; set; }
        public string general_score { get; set; }
        public string total_score { get; set; }
    }
}