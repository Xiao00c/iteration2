using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public class MappedQuestions
    {
        public MappedQuestions(List<List<Question>> questionLists, string[] descs)
        {
            Level1 = questionLists[0];
            Level2 = questionLists[1];
            Level3 = questionLists[2];
            Level4 = questionLists[3];
            Level5 = questionLists[4];

            Level1Desc = descs[0];
            Level2Desc = descs[1];
            Level3Desc = descs[2];
            Level4Desc = descs[3];
            Level5Desc = descs[4];
        }

        public List<Question> Level1 { get; set; }
        public List<Question> Level2 { get; set; }
        public List<Question> Level3 { get; set; }
        public List<Question> Level4 { get; set; }
        public List<Question> Level5 { get; set; }
        public string Level1Desc { get; set; }
        public string Level2Desc { get; set; }
        public string Level3Desc { get; set; }
        public string Level4Desc { get; set; }
        public string Level5Desc { get; set; }
    }
}