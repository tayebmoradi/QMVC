using Microsoft.AspNetCore.Mvc;
using QMVC.Models;
using System.Diagnostics;

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
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.Name.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                
            }
            return View(objUser);
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
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