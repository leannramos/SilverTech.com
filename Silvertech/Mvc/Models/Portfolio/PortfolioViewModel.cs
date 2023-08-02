using System;
using System.Collections.Generic;

namespace SitefinityWebApp.Mvc.Models
{
    public class PortfolioViewModel
    {
        public PortfolioViewModel()
        {
            Title = Title;
            Description = Description;
            ActionName = ActionName;
            ActionUrl = ActionUrl;
            ActionTarget = ActionTarget;
            ActionTitle = ActionTitle;
            ImageId = ImageId;
            ImageUrl = ImageUrl;
            SelectedSizeUrl = SelectedSizeUrl;
            ImageTitle = ImageTitle;
            ImageAltText = ImageAltText;
            ImageDescription = ImageDescription;
            PortfolioEntries = PortfolioEntries;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ActionName { get; set; }
        public string ActionUrl { get; set; }
        public string ActionTarget { get; set; }
        public string ActionTitle { get; set; }
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string SelectedSizeUrl { get; set; }
        public string ImageTitle { get; set; }
        public string ImageAltText { get; set; }
        public string ImageDescription { get; set; }
        public List<PortfolioModel.PortfolioEntry> PortfolioEntries {
            get
            {
                return portfolioEntries;
            }
            set
            {
                portfolioEntries = value;
            }
        }
        private List<PortfolioModel.PortfolioEntry> portfolioEntries = new List<PortfolioModel.PortfolioEntry>();
    }
    
}