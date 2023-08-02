using BankCore.Mvc.Models.Locations;
using BankCore.Mvc.Services.Locations;
using Common.Classes;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace BankCore.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "LocationsDetail_MVC", Title = "Locations Detail", SectionName = "Locations")]
    public class LocationsDetailController : BaseController
    {
        public string locationId { get; set; }
        public string ItemType { get; } = "Telerik.Sitefinity.DynamicTypes.Model.Locations.Location";
        public LocationsDetailController(string name = "LocationsDetail")
        {

        }


        public ActionResult Index()
        {
            var model = new LocationsViewModel();

            Guid locationGuid = Guid.Empty;
            if (Guid.TryParse(locationId.Trim('\"'), out locationGuid))
            {
                var locationsService = new LocationsService();
                locationsService.GetHourSets = true;
                model = locationsService.GetItem(locationGuid);
            }

            return View(TemplateName, model);
        }
    }
}
