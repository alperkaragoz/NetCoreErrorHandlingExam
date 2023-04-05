using ErrorHandling.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.Web.Controllers
{
    //[CustomExceptionFilter(ErrorPage = "CustomError2")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("An occured error at database!");
            return View();
        }
        //public IActionResult CustomError2()
        //{
        //    return View();
        //}
    }
}
