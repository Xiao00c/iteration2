using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace iteration2.Models
{
    public static class SQLConnection
    {

        //get all questions
        public static DataTable getQuestions()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(""))
            {
                conn.Open();

                string query = "select * from carcrashes.dbo.question q, carcrashes.dbo.answer a where q.question_id = a.question_id";

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                data.Load(reader);
            }
            //System.Diagnostics.Debug.WriteLine(data.Rows.Count);
            //foreach (DataRow dataRow in data.Rows)
            //{
            //    // each row for total result
            //    /*
            //     * [0]:question id
            //     * [1]:question desc
            //     * [2]:related factor
            //     * [3]:question id
            //     * [4]:answer id
            //     * [5]:answer desc
            //     * [6]:correct -- 1 correct, -- 0 incorrect
            //     * [7]:explanation
            //    */
            //    foreach (var item in dataRow.ItemArray)
            //    {
            //        //each item for single column
            //        System.Diagnostics.Debug.WriteLine(item);
            //    }
            //}
            return data;
        }

        public static DataTable getAlcoholQuestions()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(""))
            {
                conn.Open();

                string query = "select * from carcrashes.dbo.question q, carcrashes.dbo.answer a " +
                    "where q.question_id = a.question_id and q.related_factor = 'alcohol' ;";

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                data.Load(reader);
            }
            return data;
        }

        public static DataTable getQuestionsByFactor(string factor)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(""))
            {
                conn.Open();

                string query = "select * from carcrashes.dbo.question q, carcrashes.dbo.answer a " +
                    "where q.question_id = a.question_id and q.related_factor = '" + factor + "' ;";

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                data.Load(reader);
            }
            return data;
        }


    }
}
