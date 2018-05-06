using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LearningCenter.Website.Models;

namespace LearningCenter.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IClassListRepository classListRepository;
        private readonly IIndividualClassRepository individualClassRepository;

        public HomeController(IUserRepository userRepository,
                              IIndividualClassRepository individualClassRepository,
                              IClassListRepository classListRepository)
        {
            this.userRepository = userRepository;
            this.individualClassRepository = individualClassRepository;
            this.classListRepository = classListRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = userRepository.Register(model.Email, model.Password);
                return Redirect("~/");
            }
            return View(model);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(loginModel.Email, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new LearningCenter.Website.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult ClassList()
        {
            var classes = classListRepository.ClassList
                                .Select(t => 
                                    new LearningCenter.Website.Models.ClassListModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Description = t.Description,
                                        Price = t.Price

                                    }).ToArray();

            var model = new ClassListViewModel
            {
                
                ClassLists = classes
            };

            return View(model);
        }

        public ActionResult EnrollInClass()
        {
            var model = new IndividualClassModel();
            model.classes = GetAllClasses();
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EnrollInClass(IndividualClassModel model)
        {

            var user = (LearningCenter.Website.Models.UserModel)Session["User"];
            model.classes = GetAllClasses();

            if(ModelState.IsValid)
            {
                //individualClassRepository.Add(user.Id, model.ClassId);
                Console.WriteLine(user.Id);
                Console.WriteLine(model.ClassId);
                individualClassRepository.Add(user.Id, model.ClassId);
                return RedirectToAction("Index");
            }

            return View();
        }

        private IEnumerable<SelectListItem> GetAllClasses()
        {
            var classes = classListRepository.ClassList
                                .Select(t =>
                                    new LearningCenter.Website.Models.ClassListModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Description = t.Description,
                                        Price = t.Price

                                    }).ToArray();

            var selectList = new List<SelectListItem>();

            foreach (var element in classes)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }
            

            return selectList;
        }
    }
}