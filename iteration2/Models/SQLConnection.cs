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
        public static string CONNECTION_STRING = "Server = carcrashes.database.windows.net; Initial Catalog = carcrashes;User ID = peter; Password = Xiao00c.xu;";


        //get all questions
        public static DataTable getQuestions()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
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

        //get questions ordered by factors
        public static DataTable getQuestionsOrderByFactor()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                string query = "select * from carcrashes.dbo.question q, carcrashes.dbo.answer a " +
                    "where q.question_id = a.question_id order by q.related_factor;";

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                data.Load(reader);
            }
            return data;
        }


        public static DataTable getQuestionsByFactor(string factor)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
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

        public static double getAlhocolPercentageByLGA(string lga)
        {
            string query = "";
            if (lga == "")
            {
                //average
                query = "select count(*), (select count(*) from carcrashes.dbo.crashes) from carcrashes.dbo.crashes c where c.alcohol_related = 'Yes';";
            }
            else
            {
                //by lga
                query = "select count(*), (select count(*) from carcrashes.dbo.crashes where lga_name = '" + lga + "') from carcrashes.dbo.crashes c where c.alcohol_related = 'Yes' and c.lga_name = '" + lga + "';";
            }

            //get count for alcohol related
            double percentage = 0;
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                //"server=localhost;user id=root;password=Peter@1993;database=carcrashes;"
                //"server=saferchampion.mysql.database.azure.com;user id=peter@saferchampion;password=Xiao00c.xu;database=carcrashes;SslMode = MySqlSslMode.Preferred;"
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    percentage = Double.Parse(reader[0].ToString()) / Double.Parse(reader[1].ToString());
                }
            }
            return percentage * 100;
        }


        

        //get distribution
        public static string getDistribution(string alcohol_imp, string speeding_imp, string fatigue_imp)
        {
            string result = "";
            string query = "select alcohol_weight, speeding_weight, distraction_weight, fatigue_weight, general_weight " +
                "from carcrashes.dbo.distribution d, carcrashes.dbo.importanceDistribution i " +
                "where d.distribution_ID = i.distribution_ID and alcohol_imp = '" + alcohol_imp + "' and speeding_imp = '" + speeding_imp + "';";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = Int32.Parse(reader[0].ToString()) + "," +
                    Int32.Parse(reader[1].ToString()) + "," +
                    Int32.Parse(reader[2].ToString()) + "," +
                    Int32.Parse(reader[3].ToString()) + "," +
                    Int32.Parse(reader[4].ToString());
                }
            }

            return result;
        }

        //get lga by postcode, multiple lga included.
        public static string getLGAByPostcode(string postcode)
        {
            string result = "";
            string query = "select LGA from carcrashes.dbo.LGApostcode where postcode = " + postcode + ";";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = result + reader[0] + ",";
                }
            }

            return result;
        }

        //get speeding accidents from Queensland, before, current
        public static string getSpeedingCrashes()
        {
            string result = "";
            string query = "select sum(Count_Crashes)/(select count(distinct(Crash_Year)) - 1 from carcrashes.dbo.factorsinroadcrashes)  as beforeAverage, " +
                "(select sum(Count_Crashes) from carcrashes.dbo.factorsinroadcrashes where Crash_Year = (select Max(distinct(Crash_Year)) from carcrashes.dbo.factorsinroadcrashes) and Involving_Driver_Speed = 'Yes') as lastYear " +
                "from carcrashes.dbo.factorsinroadcrashes " +
                "where Involving_Driver_Speed = 'Yes' " +
                "and Crash_Year < (select Max(distinct(Crash_Year)) from carcrashes.dbo.factorsinroadcrashes);";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader[0] + "," + reader[1];
                }
            }

            return result;
        }

        //get Fatigue accident from Queensland, before current
        public static string getFatigueCrashes()
        {
            string result = "";
            string query = "select sum(Count_Crashes)/(select count(distinct(Crash_Year)) - 1 from carcrashes.dbo.factorsinroadcrashes)  as beforeAverage, " +
                "(select sum(Count_Crashes) from carcrashes.dbo.factorsinroadcrashes where Crash_Year = (select Max(distinct(Crash_Year)) from carcrashes.dbo.factorsinroadcrashes) and Involving_Fatigued_Driver = 'Yes') as lastYear " +
                "from carcrashes.dbo.factorsinroadcrashes " +
                "where Involving_Fatigued_Driver = 'Yes' " +
                "and Crash_Year < (select Max(distinct(Crash_Year)) from carcrashes.dbo.factorsinroadcrashes);";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader[0] + "," + reader[1];
                }
            }

            return result;
        }

        //get accident by years by factor
        public static string getCrashesByFactor(string factor)
        {
            string result = "";
            string query = "";
            //branch if factor is speeding or fatigue
            if (factor == "speeding")
            {
                query = "select sum(Count_Crashes) as crashes from dbo.factorsinroadcrashes where Involving_Driver_Speed = 'Yes' group by Crash_Year order by Crash_Year;";
            }
            else
            {
                query = "select sum(Count_Crashes) as crashes from dbo.factorsinroadcrashes where Involving_Fatigued_Driver = 'Yes' group by Crash_Year order by Crash_Year;";
            }
            

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = result + "," + reader[0];
                }
            }

            return result;
        }
    }
}
