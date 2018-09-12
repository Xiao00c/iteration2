using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iteration2.Models;
using Rotativa;

namespace iteration2.Controllers
{
    public class HomeController : Controller
    {
        //home page
        public ActionResult Index()
        {
            //test
            System.Diagnostics.Debug.WriteLine(SQLConnection.getLGAByPostcode("3008"));
            return View();
        }

        public ActionResult Explore()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            SQLConnection.getQuestions();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Factors()
        {


            return View();
        }

        //new version: map, challenge all in one page.
        public ActionResult Challenge(string postcode)
        {
            //if no postcode
            if (null == postcode)
            {
                //default
                postcode = "3162";
            }

            double alcohol_lga = 0.00;
            double alcohol_total = SQLConnection.getAlhocolPercentageByLGA("");

            //get LGA by postcode
            string[] LGAs = SQLConnection.getLGAByPostcode(postcode).Split(',');
            
            //get each alcohol related by each lga.
            for (int i = 0; i < LGAs.Length; i++)
            {
                alcohol_lga = alcohol_lga + SQLConnection.getAlhocolPercentageByLGA(LGAs[i]);
            }
            alcohol_lga = alcohol_lga / LGAs.Length;

            string alcohol_imp = ChallengerHelper.comparePercentage(alcohol_lga, alcohol_total);

            //get Speeding from Queensland
            string[] speeding = SQLConnection.getSpeedingCrashes().Split(',');
            
            string speeding_imp =  ChallengerHelper.calcualteWeightBasedOnYear(speeding);

            //get Fatigue from Queensland
            string[] fatigue = SQLConnection.getFatigueCrashes().Split(',');
            string fatigue_imp = ChallengerHelper.calcualteWeightBasedOnYear(fatigue);


            //get distribution
            string distributions = SQLConnection.getDistribution(alcohol_imp, speeding_imp, fatigue_imp);

            //put data in viewbag
            ViewBag.LGAs = string.Join(",", LGAs);
            ViewBag.alcohol_lga = String.Format("{0:0.00}", alcohol_lga) + "%";
            ViewBag.alcohol_total = String.Format("{0:0.00}", alcohol_total) + "%";
            ViewBag.alcohol_imp = ChallengerHelper.getFullImportanceName(alcohol_imp);
            ViewBag.speeding_imp = ChallengerHelper.getFullImportanceName(speeding_imp);
            ViewBag.speedingPrevious = speeding[0];
            ViewBag.speedingCurrent = speeding[1];
            ViewBag.fatigue_imp = ChallengerHelper.getFullImportanceName(fatigue_imp);
            ViewBag.fatiguePrevious = fatigue[0];
            ViewBag.fatigueCurrent = fatigue[1];


            //four factors and get weight for specific factor.
            string[] weights = distributions.Split(',');
        
            //sort factor distributions with value of factor name.
            //SortedList<int, string> disDic = new SortedList<int, string>();
            //disDic.Add(Int32.Parse(weights[0]) / 10, "drunk");
            //disDic.Add(Int32.Parse(weights[1]) / 10, "speeding");
            //disDic.Add(Int32.Parse(weights[2]) / 10, "distraction");
            //disDic.Add(Int32.Parse(weights[3]) / 10, "fatigue");
            //disDic.Add(Int32.Parse(weights[4]) / 10, "general");

            //get questions based on distribution
            List<Question> orderedQuestions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsOrderByFactor());
            List<Question> drunkQuestions = new List<Question>();
            List<Question> speedingQuestions = new List<Question>();
            List<Question> fatigueQuestions = new List<Question>();
            List<Question> generalQuestions = new List<Question>();
            List<Question> distractionQuestions = new List<Question>();

            //separate questions.
            for (int i = 0; i < orderedQuestions.Count; i++)
            {
                if(orderedQuestions[i].related_factor == "alcohol")
                {
                    drunkQuestions.Add(orderedQuestions[i]);
                }
                else if (orderedQuestions[i].related_factor == "speeding")
                {
                    speedingQuestions.Add(orderedQuestions[i]);
                }
                else if (orderedQuestions[i].related_factor == "distraction")
                {
                    distractionQuestions.Add(orderedQuestions[i]);
                }
                else if (orderedQuestions[i].related_factor == "general")
                {
                    generalQuestions.Add(orderedQuestions[i]);
                }
                else if (orderedQuestions[i].related_factor == "fatigue")
                {
                    fatigueQuestions.Add(orderedQuestions[i]);
                }
            }

            //shuffle all questions lists.
            drunkQuestions = ChallengerHelper.shuffleQuestions(drunkQuestions);
            speedingQuestions = ChallengerHelper.shuffleQuestions(speedingQuestions);
            distractionQuestions = ChallengerHelper.shuffleQuestions(distractionQuestions);
            generalQuestions = ChallengerHelper.shuffleQuestions(generalQuestions);
            fatigueQuestions = ChallengerHelper.shuffleQuestions(fatigueQuestions);

            //get top distribution number of question from each question lists.

            drunkQuestions = drunkQuestions.GetRange(0,Int32.Parse(weights[0]) / 10);
            speedingQuestions = speedingQuestions.GetRange(0, Int32.Parse(weights[1]) / 10);
            distractionQuestions = distractionQuestions.GetRange(0, Int32.Parse(weights[2]) / 10);
            fatigueQuestions = fatigueQuestions.GetRange(0, Int32.Parse(weights[3]) / 10);
            generalQuestions = generalQuestions.GetRange(0, Int32.Parse(weights[4]) / 10);

            //sort all questionlists
            //List<List<Question>> shuffledQuestions = new List<List<Question>>();
            //string[] descs = { };
            //for (int i = 4; i > -1; i--)
            //{
            //    if (disDic.Values[i] == "drunk")
            //    {
            //        shuffledQuestions.Add(drunkQuestions);
            //        descs[5 - i] = "drunk";
            //    }
            //    else if (disDic.Values[i] == "speeding")
            //    {
            //        shuffledQuestions.Add(speedingQuestions);
            //        descs[5 - i] = "speeding";
            //    }
            //    else if (disDic.Values[i] == "distraction")
            //    {
            //        shuffledQuestions.Add(distractionQuestions);
            //        descs[5 - i] = "distraction";
            //    }
            //    else if (disDic.Values[i] == "fatigue")
            //    {
            //        shuffledQuestions.Add(fatigueQuestions);
            //        descs[5 - i] = "fatigue";
            //    }
            //    else if (disDic.Values[i] == "general")
            //    {
            //        shuffledQuestions.Add(generalQuestions);
            //        descs[5 - i] = "general";
            //    }
            //}

            //hardcoded
            List<List<Question>> shuffledQuestions = new List<List<Question>>();
            shuffledQuestions.Add(drunkQuestions);
            shuffledQuestions.Add(speedingQuestions);
            shuffledQuestions.Add(distractionQuestions);
            shuffledQuestions.Add(fatigueQuestions);
            shuffledQuestions.Add(generalQuestions);
            string[] descs = {"drunk", "speeding", "distraction", "fatigue", "general" };

            //create MappedQuestions
            MappedQuestions mappedQuestions = new MappedQuestions(shuffledQuestions, descs);


            return View(mappedQuestions);
        }

        //generate pdf for challenge
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Certificate")
            {
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                FileName = "Safety_Champion_Certificate.pdf"
            };
            return report;
        }

        public ActionResult Certificate()
        {
            return View();
        }
    }
}