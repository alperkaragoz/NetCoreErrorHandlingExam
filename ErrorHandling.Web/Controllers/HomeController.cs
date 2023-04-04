using ErrorHandling.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ErrorHandling.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int val1 = 5;
            int val2 = 0;
            int result = val1 / val2;
            return View();
        }

        public IActionResult Privacy()
        {
            //throw new FileNotFoundException();
            return View();
        }

        [AllowAnonymous]
        // Hata sayfaları cachlenmeyeceği için attribute aşağıdaki şekilde default olarak geliyor.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Alınan hatayı yakalıyoruz.
            // HttpContext üzerinden request ve response erişim sağlayabiliyoruz.
            // Aşağıdaki kod satırıyla, uygulamanın herhangi bir yerinde alınan hatayı yakalayabiliyoruz.
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.Path = exception.Path;
            ViewBag.Message = exception.Error.Message;

            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}