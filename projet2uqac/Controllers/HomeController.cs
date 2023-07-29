using projet2uqac.Services;
using Microsoft.AspNetCore.Mvc;
using projet2uqac.Models;
using System.Diagnostics;

namespace projet2uqac.Controllers
{
    public class HomeController : Controller
    {
        //services
        IKeyManagementService _keyService;
        IConfiguration _config;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IKeyManagementService keyService, IConfiguration config)
        {
            _logger = logger;
            _keyService = keyService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View(_keyService.GetKeys());
        }

        [HttpGet]
        public IActionResult GenerateKey()
        {
            string name = "MY PERSONAL KEY";
            _keyService.GenereateMyKeys(name);

            return RedirectToAction("index");
        }

        [HttpGet]   
        public IActionResult Export(int id)
        {
            string res = _keyService.exportKey(id);
            return View((object)res);
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportKey()
        {
            string name = HttpContext.Request.Form["name"];
            string value = HttpContext.Request.Form["key"];
            try
            {
               _keyService.ImportKey(value, name);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("error", new {message = "La clé que tu essaie d'importer ne semble pas fonctionner" } );
            }

        }

        [HttpGet]
        public IActionResult Error(string message)
        {
            string res = message;
            return View((object)res);
        }
    }
}