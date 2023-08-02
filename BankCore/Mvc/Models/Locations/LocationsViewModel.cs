using System.Collections.Generic;
using Common.Classes.ViewModels;

namespace BankCore.Mvc.Models.Locations
{
    public class LocationsViewModel
    {

        public string Url { get; set; }
        public string Title { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string GoogleMapsLink { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool HasAtm { get; set; }
        public bool HasDriveThrough { get; set; }
        public bool HasCoinCounter { get; set; }
        public bool HasDepositBox { get; set; }
        public bool HasInstantIssue { get; set; }
        public bool HasOnlineAppointment { get; set; }
        public bool HasPersonalAutomatedTeller { get; set; }
        public bool HasRepinCards { get; set; }
        public bool HasSharedBranching { get; set; }

        public ImageViewModel LocationThumbnail { get; set; }

        public List<HourSetViewModel> HourSets { get; set; }

        public LocationsViewModel()
        {
            HourSets = new List<HourSetViewModel>();
        }

    }
}
