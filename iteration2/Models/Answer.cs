using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public class Answer
    {
        public Answer(int id, string desc, int corr, string exp)
        {
            answer_id = id;
            answer_desc = desc;
            correct = corr;
            explanation = exp;
        }
        public int answer_id { get; set; }
        public string answer_desc { get; set; }
        public int correct { get; set; }
        public string explanation { get; set; }
    }
}