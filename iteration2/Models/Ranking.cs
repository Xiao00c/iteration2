using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public class Ranking
    {
        public Ranking(string council,  double score, List<string> newPostcodes, string desc)
        {
            council_name = council;
            this.score = score;
            postcodes = newPostcodes;
            this.desc = desc;
        }

        public string desc { get; set; }
        public string council_name { get; set; }
        public double score { get; set; }
        public List<string> postcodes { get; set; }
    }
}