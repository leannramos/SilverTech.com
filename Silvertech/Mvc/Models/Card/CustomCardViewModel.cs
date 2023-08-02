using Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card;

namespace SitefinityWebApp.Mvc.Models
{
    public class CustomCardViewModel : CardViewModel
    {
        public CustomCardViewModel()
          : base()
        {
            ActionTitle = this.ActionTitle;
            ActionTarget = this.ActionTarget;
            HeadingTwo = this.HeadingTwo;
            ActionTwoName = this.ActionTwoName;
            ActionTwoTitle = this.ActionTwoTitle;
            ActionTwoTarget = this.ActionTwoTarget;
            ActionTwoUrl = this.ActionTwoUrl;
            ActionThreeUrl = this.ActionThreeUrl;
            ActionThreeName = this.ActionThreeName;
            ActionThreeTitle = this.ActionThreeTitle;
            ActionThreeTarget = this.ActionThreeTarget;
            SvgMarkup = this.SvgMarkup;
        }

        public string ActionTitle { get; set; }
        public string ActionTarget { get; set; }

        public string ActionTwoName { get; set; }
        public string ActionTwoTitle { get; set; }
        public string ActionTwoTarget { get; set; }
        public string ActionTwoUrl { get; set; }

        public bool IsActionSpeedbump { get; set; }
        public string ActionSpeedbumpMessage { get; set; }
        public bool IsActionTwoSpeedbump { get; set; }
        public string ActionTwoSpeedbumpMessage { get; set; }

        public string ActionThreeName { get; set; }
        public string ActionThreeTitle { get; set; }
        public string ActionThreeTarget { get; set; }
        public string ActionThreeUrl { get; set; }
        public bool IsPageSelectModeThree { get; set; }
        public bool IsActionThreeSpeedbump { get; set; }
        public string ActionThreeSpeedbumpMessage { get; set; }

        public string HeadingTwo { get; set; }
        public string ImageTwoAlternativeText { get; set; }
        public string ImageTwoTitle { get; set; }
        public string ImageTwoUrl { get; set; }
        public string ClientSpotlightHeading { get; set; }
        public string SvgMarkup { get; set; }
    }
}