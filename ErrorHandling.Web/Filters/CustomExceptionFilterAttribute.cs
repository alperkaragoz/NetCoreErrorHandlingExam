using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ErrorHandling.Web.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }
        public override void OnException(ExceptionContext context)
        {
            // Loglama örneği

            if (ErrorPage == "CustomError") {/*Custom Error loglama;*/ }
            if (ErrorPage == "CustomError2") {/*Custom Error 2 loglama;*/ }

            var result = new ViewResult() { ViewName = ErrorPage };
            // Oluşturulan CustomError viewına data göndermemiz için aşağıdaki kodu ekliyoruz.
            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState);


            result.ViewData.Add("Exception", context.Exception);
            result.ViewData.Add("Url", context.HttpContext.Request.Path.Value);

            context.Result = result;
        }
    }
}
