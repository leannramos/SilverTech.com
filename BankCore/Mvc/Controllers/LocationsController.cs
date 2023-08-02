
using Common.Classes;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using System.Collections.Generic;
using BankCore.Mvc.Models.Locations;
using BankCore.Mvc.Services.Locations;

namespace BankCore.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "Locations_MVC", Title = "Locations", SectionName = "Locations")]
    public class LocationsController : BaseController
    {
        public LocationsController(string name = "Locations")
        {

        }

        public ActionResult Index()
        {
            var model = new List<LocationsViewModel>();
            if (string.IsNullOrWhiteSpace(HttpContext.Request.QueryString["searchQuery"]))
            {
                var locationsService = new LocationsService();
                model = locationsService.GetItems();
            }
            
            return View("Locations.SearchDefault", model);
        }
    }
}
