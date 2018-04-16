using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCardGenerator.Controllers
{
    public class BirthdayController : Controller
    {
        // GET: Birthday
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BirthdayGenerator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BirthdayGenerator(Models.BirthdayMessageDetails birthdayResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", birthdayResponse);
            }
            else
            {
                return View();
            }
        }
    }
}