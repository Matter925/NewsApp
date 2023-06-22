using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace NewsMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index(string token)
        {
            string mytoken = token;

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1) // Set the expiration time for the cookie
            };

            // Store the data in the cookie
            if (mytoken == null)
            {
                string OldToken = HttpContext.Request.Cookies["MyCookie"];
                HttpContext.Response.Cookies.Append("MyCookie", OldToken, options);
                return View();
            }
            HttpContext.Response.Cookies.Append("MyCookie", mytoken, options);

            return View();
        }

        
    }
}
