
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMVC.Areas.Admin.Services;
using NewsMVC.Areas.Admin.ViewModels;
using System.Text.Json;

namespace NewsMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly IConnectRepository _connectRepository;
        public NewsController(IConnectRepository connectRepository)
        {
            _connectRepository = connectRepository;
        }
        public async Task <IActionResult> Index()
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

            List<NewVM> News = JsonSerializer.Deserialize<List<NewVM>>(Result);
            return View(News);
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            string URLAPI = "https://localhost:7056/api/News/GetAllNews";

            string token = HttpContext.Request.Cookies["MyCookie"];
            ViewBag.Token = token;

            //-----------------------------------------------------------------------------------------------------



            //----------------------------------------------------------------------------------------------------------
            string Result = await _connectRepository.ConnectAsync(URLAPI);
            if (Result == null)
            {
                return Json(null);
            }

            List<NewVM> News = JsonSerializer.Deserialize<List<NewVM>>(Result);
            return Json(News);
        }
        public async Task<IActionResult> Create()
        {
            string URAuthor = "https://localhost:7056/api/Authors/GetAllAuthors";
            string token = HttpContext.Request.Cookies["MyCookie"];
            ViewBag.Token = token;
            string Authors = await _connectRepository.ConnectAsync(URAuthor);
            if (Authors != null)
            {
                List<AuthorDto> ContentDto = JsonSerializer.Deserialize<List<AuthorDto>>(Authors);

                ViewBag.Authors = new SelectList(ContentDto, "id", "name");
            }

            return View();
        }




    }
}
