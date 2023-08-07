using Microsoft.AspNetCore.Mvc;
using projet2uqac.Models;
using projet2uqac.Services;
using System.Diagnostics;

namespace projet2uqac.Controllers
{
    public class EncryptController : Controller
    {
        private readonly ILogger<EncryptController> _logger;
        //services
        IKeyManagementService _keyService;
        ICryptoOperationServices _cryptoService;
        public EncryptController(ILogger<EncryptController> logger, IKeyManagementService keyService, ICryptoOperationServices cryptoService)
        {
            _logger = logger;
            _keyService = keyService;
            _cryptoService = cryptoService;
        }

        public IActionResult Index()
        {
            return View(_keyService.GetKeys());
        }

        public IActionResult Result()
        {
            int keyID = Convert.ToInt32(HttpContext.Request.Form["keyId"]);
            string message = HttpContext.Request.Form["message"];
            string messageResult = "";

            Key key = _keyService.GetKeyById(keyID);

            if ((!key.isPrivate))
            {
                messageResult = "VOICI TON TEXTE : " + _cryptoService.EncryptMessage(message, key);
            }
            else
            {
                messageResult = "**Je crois que tu as mal compris la théorie... retourne la voir ;) **";
            }

            return View((object)messageResult);
        }
    }
}