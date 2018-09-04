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

        //reuse view.
        public ActionResult Challenge(string factor)
        {
            //give different questions based on given factor.
            //Distraction, Speeding, Fatigue, Drunk, General

            //test
            System.Diagnostics.Debug.WriteLine(factor);

            List<Question> questions = new List<Question>();
            if (factor == "Drunk")
            {
                questions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsByFactor("alcohol"));
                ViewBag.factor_desc = "Driving after drinking alcohol is against the Law!";
                ViewBag.factor = "Drunk";
                System.Diagnostics.Debug.WriteLine(factor);
            }
            else if (factor == "Speeding")
            {
                questions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsByFactor("speeding"));
                ViewBag.factor_desc = "the Faster the Cooler?";
                ViewBag.factor = "Speeding";
                System.Diagnostics.Debug.WriteLine(factor);
            }
            else if (factor == "Fatigue")
            {
                questions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsByFactor("fatigue"));
                ViewBag.factor_desc = "A tired and sleepy driver is bound to crash his car!";
                ViewBag.factor = "Fatigue";
                System.Diagnostics.Debug.WriteLine(factor);
            }
            else if (factor == "Distraction")
            {
                questions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsByFactor("distraction"));
                ViewBag.factor_desc = "Did you know distracted drivers cause the most accidents in Australia?";
                ViewBag.factor = "Distraction";
                System.Diagnostics.Debug.WriteLine(factor);
            }
            else
            {
                questions = ChallengerHelper.generateDataFromDataTable(SQLConnection.getQuestionsByFactor("general"));
                ViewBag.factor_desc = "Be Smart, Be Safe!";
                ViewBag.factor = "General";
                System.Diagnostics.Debug.WriteLine(factor);
            }

            return View(questions);
        }

        //generate pdf for challenge
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }
    }
}