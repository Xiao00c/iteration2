using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iteration2.Models;

namespace iteration2.Models
{
    public class RankingList
    {
        public RankingList(List<Ranking> total, List<Ranking> week, List<Ranking> drunk, List<Ranking> speeding, List<Ranking> distraction, List<Ranking> fatigue, List<Ranking> general)
        {
            this.total = total;
            this.week = week;
            this.drunk = drunk;
            this.speeding = speeding;
            this.distraction = distraction;
            this.fatigue = fatigue;
            this.general = general;
        }

        public List<Ranking> total { get; set; }
        public List<Ranking> week { get; set; }
        public List<Ranking> drunk { get; set; }
        public List<Ranking> speeding { get; set; }
        public List<Ranking> distraction { get; set; }
        public List<Ranking> fatigue { get; set; }
        public List<Ranking> general { get; set; }
    }
}