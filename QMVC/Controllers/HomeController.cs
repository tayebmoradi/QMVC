using Microsoft.AspNetCore.Mvc;
using QMVC.Models;
using System.Diagnostics;
using 

namespace QMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpContext httpContext;
        private readonly Database _database;

        public HomeController(ILogger<HomeController> logger,HttpContext httpContext,Database database)
        {
            _logger = logger;
            this.httpContext = httpContext;
           _database = database;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
               
                    var obj = _database.Users.Where(a => a.Name.Equals(objUser.Name) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                     ViewData["id"] = obj;
                    if (obj != null)
                    {
                    HttpContext.Session.SetString("UserID", obj.Id.ToString());
                    HttpContext.Session.SetString("UserName", obj.Id.ToString());
                 
                        return RedirectToAction("UserDashBoard");
                    }
                
            }
            return View(objUser);
        }
        public ActionResult UserDashBoard()
        {
         var id =   ViewData["id"];
            if (HttpContext.Session.SetString("UserID",id ) != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}