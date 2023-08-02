using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;


namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "PageNotFound_MVC", Title = "PageNotFound", SectionName = "Custom")]
    public class PageNotFoundController : Controller
    {
        public ActionResult Index()
        {
            Response.Status = "404 Not Found";
            Response.StatusCode = 404;
            Response.StatusDescription = "Not Found!";

            return Content("");
        }
    }
}