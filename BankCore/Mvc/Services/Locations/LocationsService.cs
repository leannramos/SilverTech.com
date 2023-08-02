using BankCore.Mvc.Models.Locations;
using Common.Classes;
using Common.Classes.Services;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Model;

namespace BankCore.Mvc.Services.Locations
{
    public class LocationsService : DynamicContentService<LocationsViewModel>
    {
        public bool GetHourSets { get; set; }
        public LocationsService(string type = "Telerik.Sitefinity.DynamicTypes.Model.Locations.Location", string providerName = "", bool AutoInitialize = true) : base(type, providerName, AutoInitialize)
        {
            
        }

        public IEnumerable<string> GetCities()
        {
            var cities = new List<string>();
            var items = this.GetDynamicContentItems();
            foreach(var item in items)
            {
                cities.Add(item.GetStringValue("City"));
            }
            return cities.Distinct();
        }

        protected override LocationsViewModel BuildModel(DynamicContent item)
        {
            var lvm = new LocationsViewModel();
            lvm.Url  = $"/locations{item.ItemDefaultUrl}";
            lvm.Title = item.GetStringValue("Title");
            lvm.StreetAddress = item.GetStringValue("StreetAddress");
            lvm.City = item.GetStringValue("City");
            lvm.State = item.GetStringValue("State");
            lvm.Zip = item.GetStringValue("Zip");
            lvm.PhoneNumber = item.GetStringValue("PhoneNumber");
            lvm.GoogleMapsLink = item.GetStringValue("GoogleMapsLink");
            //lvm.Latitude = item.GetStringValue("Latitude");
            //lvm.Longitude = item.GetStringValue("Longitude");
            lvm.HasAtm = item.GetBooleanValue("HasAtm");
            lvm.HasDriveThrough = item.GetBooleanValue("HasDriveThrough");
            lvm.HasCoinCounter = item.GetBooleanValue("HasCoinCounter");
            lvm.HasDepositBox = item.GetBooleanValue("HasDepositBox");
            lvm.HasInstantIssue = item.GetBooleanValue("HasInstantIssue");
            lvm.HasOnlineAppointment = item.GetBooleanValue("HasOnlineAppointment");
            lvm.HasPersonalAutomatedTeller = item.GetBooleanValue("HasPersonalAutomatedTeller");
            lvm.HasRepinCards = item.GetBooleanValue("HasRepinCards");
            lvm.HasSharedBranching = item.GetBooleanValue("HasSharedBranching");
            lvm.LocationThumbnail = new ImageService().GetFirstImage(item, "LocationThumbnail");
            if (GetHourSets)
            {
                var hourSetService = new HourSetService();
                var hoursets = hourSetService.GetChildren(item);
                lvm.HourSets = hoursets;
            }

            return lvm;
        }
    }
}
