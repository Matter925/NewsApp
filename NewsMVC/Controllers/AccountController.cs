using Microsoft.AspNetCore.Mvc;
using NewsMVC.ViewModels;

namespace NewsMVC.Controllers
{
    public class AccountController : Controller
    {
        

        
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        
    }
}
