using Microsoft.AspNetCore.Mvc;
using NewsMVC.Areas.Admin.Services;
using NewsMVC.Areas.Admin.ViewModels;
using NewsMVC.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace NewsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConnectRepository _connectRepository;
        public HomeController(ILogger<HomeController> logger , IConnectRepository connectRepository)
        {
            _logger = logger;
            _connectRepository = connectRepository;
        }

        public async Task< IActionResult> Index()
        {
            string URLAPI = "https://localhost:7056/api/News/GetAllNews";

            string token = HttpContext.Request.Cookies["MyCookie"];
            ViewBag.Token = token;

            //-----------------------------------------------------------------------------------------------------



            //----------------------------------------------------------------------------------------------------------
            string Result = await _connectRepository.ConnectAsync(URLAPI);
            if (Result == null)
            {
                return View(null);
            }

            List<NewDto> News = JsonSerializer.Deserialize<List<NewDto>>(Result);

            return View(News);
        }
        public async Task<IActionResult> NewsDetails(int id)
        {
            string URLAPI = "https://localhost:7056/api/News/GetNewByID/" + id;

            string token = HttpContext.Request.Cookies["MyCookie"];
            ViewBag.Token = token;

            //-----------------------------------------------------------------------------------------------------



            //----------------------------------------------------------------------------------------------------------
            string Result = await _connectRepository.ConnectAsync(URLAPI);
            if (Result == null)
            {
                return View(null);
            }

            NewDto News = JsonSerializer.Deserialize<NewDto>(Result);

            return View(News);
           
        }


            public IActionResult Privacy()
        {
            return View();
        }

        
    }
}