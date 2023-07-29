using Microsoft.AspNetCore.Mvc;
using projet2uqac.Models;
using projet2uqac.Services;
using System.Diagnostics;

namespace projet2uqac.Controllers
{
    public class DecryptController : Controller
    {
        private readonly ILogger<DecryptController> _logger;
        //services
        IKeyManagementService _keyService;
        ICryptoOperationServices _cryptoService;
        public DecryptController(ILogger<DecryptController> logger, IKeyManagementService keyService, ICryptoOperationServices cryptoService)
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
            try
            {
                if ((key.isPrivate))
                {
                    messageResult = "VOICI TON TEXTE : " + _cryptoService.DecryptMessage(message, key);
                }
                else
                {
                    messageResult = "**Je crois que tu as mal compris la théorie... retourne la voir ;) **";
                }
            }
            catch (Exception ex)
            {
                messageResult = "erreur lors de l'importation vérifie que tout est bon...";
            }

            return View((object)messageResult);
        }
    }
}