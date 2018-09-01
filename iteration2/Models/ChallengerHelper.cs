using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iteration2.Models
{
    public static class ChallengerHelper
    {

        public static List<Question> generateDataFromDataTable(DataTable data)
        {
            /*
             * [0]:question id
             * [1]:question desc
             * [2]:related factor
             * [3]:question id
             * [4]:answer id
             * [5]:answer desc
             * [6]:correct -- 1 correct, -- 2 incorrect
             * [7]:explanation
            */

            //loop each row

            List<Question> questions = new List<Question>();
            List<Answer> answers = new List<Answer>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                //create answerlist based on row number
                int aid = Int32.Parse(data.Rows[i].ItemArray[4].ToString());
                string adesc = data.Rows[i].ItemArray[5].ToString();
                int acorr = Int32.Parse(data.Rows[i].ItemArray[6].ToString());
                string aexp = data.Rows[i].ItemArray[7].ToString();

                Answer answer = new Answer(aid, adesc, acorr, aexp);
                answers.Add(answer);

                System.Diagnostics.Debug.WriteLine(answer.answer_desc);

                //loop 3 based on 3 choices
                if (i % 3 == 2)
                {
                    //convert data into types
                    int qid = Int32.Parse(data.Rows[i].ItemArray[0].ToString());
                    string qdesc = data.Rows[i].ItemArray[1].ToString();
                    string qfactor = data.Rows[i].ItemArray[2].ToString();

                    Question ques = new Question(qid, qdesc, qfactor, answers);
                    questions.Add(ques);
                    answers = new List<Answer>();

                    System.Diagnostics.Debug.WriteLine(qid + "", qdesc, qfactor);
                }

            }
            return questions;
        }
    }
}