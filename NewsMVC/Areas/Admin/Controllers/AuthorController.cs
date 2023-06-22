using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMVC.Areas.Admin.Services;
using NewsMVC.Areas.Admin.ViewModels;
using System.Text.Json;

namespace NewsMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IConnectRepository _connectRepository;
        public AuthorController(IConnectRepository connectRepository)
        {
                _connectRepository = connectRepository;
        }
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Request.Cookies["MyCookie"];

            // Use the data in your view or perform other actions
            ViewBag.Token = token;


            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            string URLAPI = "https://localhost:7056/api/Authors/GetAllAuthors";

            string token = HttpContext.Request.Cookies["MyCookie"];
            ViewBag.Token = token;

            //-----------------------------------------------------------------------------------------------------



            //----------------------------------------------------------------------------------------------------------
            string Result = await _connectRepository.ConnectAsync(URLAPI);
            if (Result == null)
            {
                return Json(null);
            }

            List<AuthorDto> News = JsonSerializer.Deserialize<List<AuthorDto>>(Result);
            return Json(News);
        }

        

    }
}
