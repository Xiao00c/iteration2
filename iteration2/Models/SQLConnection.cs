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
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                string query = "select * from " + DATABASE + ".dbo.question q, " + DATABASE + ".dbo.answer a where q.question_id = a.question_id";

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

                string query = "select * from " + DATABASE + ".dbo.question q, " + DATABASE + ".dbo.answer a " +
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

                string query = "select * from " + DATABASE + ".dbo.question q, " + DATABASE + ".dbo.answer a " +
                    "where q.question_id = a.question_id and q.related_factor = '" + factor + "' ;";

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                data.Load(reader);
            }
            return data;
        }

        //get Drunk numbers
        public static double getAlhocolPercentageByLGA(string lga)
        {
            string query = "";
            if (lga == "")
            {
                //average
                query = "select count(*), (select count(*) from " + DATABASE + ".dbo.crashes) from " + DATABASE + ".dbo.crashes c where c.alcohol_related = 'Yes';";
            }
            else
            {
                //by lga
                query = "select count(*), (select count(*) from " + DATABASE + ".dbo.crashes where lga_name = '" + lga + "') from " + DATABASE + ".dbo.crashes c where c.alcohol_related = 'Yes' and c.lga_name = '" + lga + "';";
            }

            //get count for alcohol related
            double percentage = 0;
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                //"server=localhost;user id=root;password=Peter@1993;database=" + DATABASE + ";"
                //"server=saferchampion.mysql.database.azure.com;user id=peter@saferchampion;password=Xiao00c.xu;database=" + DATABASE + ";SslMode = MySqlSslMode.Preferred;"
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
                "from " + DATABASE + ".dbo.distribution d, " + DATABASE + ".dbo.importanceDistribution i " +
                "where d.distribution_ID = i.distribution_ID and alcohol_imp = '" + alcohol_imp + "' and speeding_imp = '" + speeding_imp + "' and fatigue_imp = '" + fatigue_imp + "';";

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
            string query = "select LGA from " + DATABASE + ".dbo.LGApostcode where postcode = " + postcode + ";";

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


        //insert record into database
        public static void insertScore(Score score)
        {
            int week = int.Parse(score.week) / 7;
            string query = "insert into " + DATABASE + ".dbo.score (postcode, council_name, year, month, day, week, drunk_score, speeding_score, distraction_score, fatigue_score, general_score, total_score) " +
                "values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12);";
                //+ score.postcode + ","
                //+ score.council_name + ","
                //+ score.year + ","
                //+ score.month + ","
                //+ score.day + ","
                //+ week + ","
                //+ score.drunk_score + ","
                //+ score.speeding_score + ","
                //+ score.distraction_score + ","
                //+ score.fatigue_score + ","
                //+ score.general_score + ","
                //+ score.total_score + ");";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 10).Value = score.postcode;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 20).Value = score.council_name;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 10).Value = score.year;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 10).Value = score.month;
                cmd.Parameters.Add("@param5", SqlDbType.VarChar, 10).Value = score.day;
                cmd.Parameters.Add("@param6", SqlDbType.VarChar, 10).Value = week + "";
                cmd.Parameters.Add("@param7", SqlDbType.VarChar, 10).Value = score.drunk_score;
                cmd.Parameters.Add("@param8", SqlDbType.VarChar, 10).Value = score.speeding_score;
                cmd.Parameters.Add("@param9", SqlDbType.VarChar, 10).Value = score.distraction_score;
                cmd.Parameters.Add("@param10", SqlDbType.VarChar, 10).Value = score.fatigue_score;
                cmd.Parameters.Add("@param11", SqlDbType.VarChar, 10).Value = score.general_score;
                cmd.Parameters.Add("@param12", SqlDbType.VarChar, 10).Value = score.total_score;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        //get ranking based on total average
        public static RankingList getRankingByAll()
        {
            List<Ranking> total = new List<Ranking>();
            List<Ranking> week = new List<Ranking>();
            List<Ranking> drunk = new List<Ranking>();
            List<Ranking> speeding = new List<Ranking>();
            List<Ranking> distraction = new List<Ranking>();
            List<Ranking> fatigue = new List<Ranking>();
            List<Ranking> general = new List<Ranking>();

            int weekNum = DateTime.Now.DayOfYear / 7;

            //string query = "select council_name, avg(drunk_score) as drunk_score, avg(speeding_score) as speeding_score, " +
            //    "avg(distraction_score) as distraction_score, avg(fatigue_score) as fatigue_score, " +
            //    "avg(general_score) as general_score, avg(total_score) as total_score, " +
            //    "string_AGG(postcode, ',') within group(order by postcode) as postcodes " +
            //    "from iteration3.dbo.score group by council_name order by total_score desc;";
            
            //total
            string query = "select council_name, avg(total_score) as total_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by total_score desc;";
            string query0 = "select council_name, avg(total_score) as total_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score where week = " + weekNum + " group by council_name order by total_score desc;";
            string query1 = "select council_name, avg(drunk_score) as drunk_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by drunk_score desc;";
            string query2 = "select council_name, avg(speeding_score) as speeding_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by speeding_score desc;";
            string query3 = "select council_name, avg(distraction_score) as distraction_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by distraction_score desc;";
            string query4 = "select council_name, avg(fatigue_score) as fatigue_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by fatigue_score desc;";
            string query5 = "select council_name, avg(general_score) as general_score, string_AGG(postcode, ',') within group(order by postcode) as postcodes from " + DATABASE + ".dbo.score group by council_name order by general_score desc;";

            //total
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "total");
                        total.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        total.Add(null);
                    }
                }
            }
            
            //week
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query0, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "week");
                        week.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        week.Add(null);
                    }
                }
            }

            //drunk
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query1, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "drunk");
                        drunk.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        drunk.Add(null);
                    }
                }
            }

            //speeding
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query2, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "speeding");
                        speeding.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        speeding.Add(null);
                    }
                }
            }

            //distraction
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query3, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "distraction");
                        distraction.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        distraction.Add(null);
                    }
                }
            }

            //fatigue
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query4, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "fatigue");
                        fatigue.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        fatigue.Add(null);
                    }
                }
            }

            //general
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query4, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2] != null)
                    {
                        List<string> temp = reader[2].ToString().Split(',').Distinct().ToList();
                        Ranking ranking = new Ranking(reader[0].ToString(), Double.Parse(reader[1].ToString()), temp, "general");
                        general.Add(ranking);
                    }
                    else
                    {
                        //does not have records
                        general.Add(null);
                    }
                }
            }


            return new RankingList(total, week, drunk, speeding, distraction,fatigue,general);
        }

        //get ranking based on total but this week
        public static List<string> getRankingByTotalAndWeek()
        {
            List<string> result = new List<string>();
            int temp = DateTime.Now.DayOfYear / 7;
            string week = temp + "";

            string query = "select council_name, sum(total_score)/count(*) as average from " + DATABASE + ".dbo.score where week = " + week + " group by council_name order by sum(total_score)/count(*) desc;";

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader[0] + "");
                }
            }
            return result;

        }
    }
}
