using Common.Classes;
using Telerik.Sitefinity.DynamicModules.Model;

namespace BankCore.Mvc.Models.Locations
{
    public class HoursViewModel
    {
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string DaysRepresentation { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public static HoursViewModel Create(DynamicContent item)
        {
            var hvm = new HoursViewModel();
            hvm.StartDay = item.GetStringValue("StartDay");
            hvm.EndDay = item.GetStringValue("EndDay");
            hvm.DaysRepresentation = hvm.StartDay == hvm.EndDay ? hvm.StartDay : $"{hvm.StartDay} - {hvm.EndDay}";

            hvm.OpenTime = item.GetStringValue("OpenTime");
            hvm.CloseTime = item.GetStringValue("CloseTime");
            return hvm;
        }
    }
}
