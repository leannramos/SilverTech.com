using BankCore.Mvc.Models.Locations;
using Common.Classes;
using Telerik.Sitefinity.DynamicModules.Model;

namespace BankCore.Mvc.Services.Locations
{
    public class HourSetService : DynamicContentService<HourSetViewModel>
    {
        public HourSetService(string type = "Telerik.Sitefinity.DynamicTypes.Model.Locations.HourSet", string providerName = "", bool AutoInitialize = true) : base(type, providerName, AutoInitialize)
        {
        }

        protected override HourSetViewModel BuildModel(DynamicContent item)
        {
            var hsvm = new HourSetViewModel();
            hsvm.Title = item.GetStringValue("Title");

            var hoursService = new HoursService();
            hsvm.Hours = hoursService.GetChildren(item);
            return hsvm;
        }
    }
}
