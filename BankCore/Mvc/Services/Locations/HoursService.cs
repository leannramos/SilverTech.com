using BankCore.Mvc.Models.Locations;
using Common.Classes;
using Telerik.Sitefinity.DynamicModules.Model;

namespace BankCore.Mvc.Services.Locations
{
    public class HoursService : DynamicContentService<HoursViewModel>
    {
        public HoursService(string type = "Telerik.Sitefinity.DynamicTypes.Model.Locations.Hour", string providerName = "", bool AutoInitialize = true) : base(type, providerName, AutoInitialize)
        {
        }

        protected override HoursViewModel BuildModel(DynamicContent item)
        {
            return HoursViewModel.Create(item);
        }
    }
}
