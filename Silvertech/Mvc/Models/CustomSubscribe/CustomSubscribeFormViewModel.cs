using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;

namespace SitefinityWebApp.Mvc.Models.CustomSubscribe
{
    public class CustomSubscribeFormViewModel : SubscribeFormViewModel
    {
        public CustomSubscribeFormViewModel() : base()
        {
            Heading = Heading;
            FieldLabel = FieldLabel;
            ButtonText = ButtonText;
        }

        public string Heading { get; set; }
        public string FieldLabel { get; set; }
        public string ButtonText { get; set; }
    }
}