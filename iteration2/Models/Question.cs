using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public class Question
    {
        public Question(int id, string desc, string factor, List<Answer> answerlist)
        {
            question_id = id;
            question_desc = desc;
            related_factor = factor;
            answers = answerlist;
        }

        public int question_id { get; set; }
        public string question_desc { get; set; }
        public string related_factor { get; set; }
        public List<Answer> answers { get; set; }
    }
}