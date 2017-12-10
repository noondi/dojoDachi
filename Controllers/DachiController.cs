using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dojodachi.Controllers
{
    public class Dachi : Controller
    {
        [HttpGetAttribute]
        [Route("")]
        public IActionResult Index()
        {
            // retrieving data from session
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            System.Console.WriteLine("Retriving data");
            // Is each retrieved data from session equal to zero??
            if (fullness == null && happiness == null && meals == null && fullness == null && energy == null)
            {
                // if retrieved data from session is equal to zero, initialize data to values below
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetInt32("energy", 50);
            }
            System.Console.WriteLine("Initializing data");
            // show retrieved data to page
            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");

            if (ViewBag.fullness > 99 && ViewBag.happiness > 99 && ViewBag.energy > 99)
                {
                    TempData["action"] = "Congrats! You won....... ";
                }
            if (fullness < 1 || happiness < 1)
                {
                    TempData["action"] = "O no! Your dojodachi is ded";
                }
            ViewBag.Action = TempData["action"]; 
            ViewBag.Example = "Hello World!";    
            return View("index");
        }
        [HttpPost]
        [Route("feed")]
        public IActionResult Feed()
        {
            // retrieving data from session
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            // Is each retrieved data from session equal to zero??
            if (fullness == null && happiness == null && meals == null && fullness == null && energy == null)
            {
                // if yes set data to values below.. otherwise jump to next step..
                fullness = 20;
                happiness = 20;
                meals = 3;
                energy = 50;
            }
            Random random = new Random();
            int fullnessAmount = random.Next(5, 11);
            int chance = random.Next(1, 5);
            if (chance == 1)
            {
                if (meals > 0)
                {
                    meals--;
                    TempData["action"] = $"You fed your Dojodachi! But he didn't like it. Fullness +0, Meals -1";
                }
            }
            else
            {
                if (meals > 0)
                {
                    meals--;
                    fullness = fullness + fullnessAmount;
                    TempData["action"] = $"You fed your Dojodachi! Fullness +{fullnessAmount}, Meals -1";
                }
            }
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("meals", (int)meals);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("play")]
        public IActionResult Play()
        {
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            if (fullness == null && happiness == null && meals == null && fullness == null && energy == null)
            {
                fullness = 20;
                happiness = 20;
                meals = 3;
                energy = 50;
            }
            Random random = new Random();
            int happinessAmount = random.Next(5, 11);
            int chance = random.Next(1, 5);
            if (chance == 1)
            {
                if (energy >= 5)
                {
                    energy -= 5;
                    TempData["action"] = $"You played with your Dojodachi! But he didn't like it. Happiness +0, Energy -5";
                }
            }
            else
            {
                if (energy >= 5)
                {
                    energy -= 5;
                    happiness = happiness + happinessAmount;
                    TempData["action"] = $"You played with your Dojodachi! Happiness +{happinessAmount}, Energy -5";
                }
            }
            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("energy", (int)energy);
            return RedirectToAction("Index");
        }



        [HttpPost]
        [Route("work")]
        public IActionResult Work()
        {
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            if (fullness == null && happiness == null && meals == null && fullness == null && energy == null)
            {
                fullness = 20;
                happiness = 20;
                meals = 3;
                energy = 50;
            }
            Random random = new Random();
            int mealsAmount = random.Next(1, 4);
            if (energy >= 5)
            {
                energy -= 5;
                meals = meals + mealsAmount;
            }
            HttpContext.Session.SetInt32("meals", (int)meals);
            HttpContext.Session.SetInt32("energy", (int)energy);
            TempData["action"] = $"You sent your Dojodachi to work! Meals + {mealsAmount}, Energy -5";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            if (fullness == null && happiness == null && meals == null && fullness == null && energy == null)
            {
                fullness = 20;
                happiness = 20;
                meals = 3;
                energy = 50;
            }
            fullness -= 5;
            happiness -= 5;
            energy += 15;
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("energy", (int)energy);
            TempData["action"] = $"Your Dojodachi is sleeping! Energy +15, Happiness -5, Fullness -5";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
