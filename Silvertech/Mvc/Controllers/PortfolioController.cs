using SitefinityWebApp.Mvc.Models;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.Portfolio
{
    [ControllerToolboxItem(Name = "Portfolio_MVC", Title = "Portfolio Widget", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class PortfolioController : Controller
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string ActionName { get; set; }
        public string ActionUrl { get; set; }
        public string ActionTarget { get; set; }
        public string ActionTitle { get; set; }
        public Guid? LinkedPageId { get; set; }
        public string LinkedUrl { get; set; }
        public bool IsPageSelectMode { get; set; }
        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string SelectedSizeUrl { get; set; }
        public string ImageTitle { get; set; }
        public string ImageAltText { get; set; }
        public string ImageDescription { get; set; }
        public Guid? ImageId1 { get; set; }
        public Guid? ImageId2 { get; set; }
        public Guid? ImageId3 { get; set; }
        public Guid? ImageId4 { get; set; }
        public Guid? ImageId5 { get; set; }
        public string ImageDescription1 { get; set; }
        public string ImageDescription2 { get; set; }
        public string ImageDescription3 { get; set; }
        public string ImageDescription4 { get; set; }
        public string ImageDescription5 { get; set; }
        public string ImageTitle1 { get; set; }
        public string ImageTitle2 { get; set; }
        public string ImageTitle3 { get; set; }
        public string ImageTitle4 { get; set; }
        public string ImageTitle5 { get; set; }
        public string ImageAltText1 { get; set; }
        public string ImageAltText2 { get; set; }
        public string ImageAltText3 { get; set; }
        public string ImageAltText4 { get; set; }
        public string ImageAltText5 { get; set; }

        public string TemplateName
        {
            get
            {
                return templateName;
            }

            set
            {
                templateName = value;
            }
        }

        public ActionResult Index()
        {
            var model = new PortfolioModel
            {
                Title = Title,
                Description = Description,
                ActionName = ActionName,
                ActionUrl = ActionUrl,
                ActionTarget = ActionTarget,
                ActionTitle = ActionTitle,
                LinkedPageId = LinkedPageId ?? Guid.Empty,
                LinkedUrl = LinkedUrl,
                IsPageSelectMode = IsPageSelectMode,
                ImageId = ImageId ?? Guid.Empty,
                ImageUrl = ImageUrl,
                SelectedSizeUrl = SelectedSizeUrl,
                ImageTitle = ImageTitle,
                ImageAltText = ImageAltText,
                ImageDescription = ImageDescription,
                ImageIds = new Guid[5] { ImageId1 ?? Guid.Empty, ImageId2 ?? Guid.Empty, ImageId3 ?? Guid.Empty, ImageId4 ?? Guid.Empty, ImageId5 ?? Guid.Empty },
                ImageDescriptions = new string[5] { ImageDescription1, ImageDescription2, ImageDescription3, ImageDescription4, ImageDescription5 },
                ImageTitles = new string[5] { ImageTitle1, ImageTitle2, ImageTitle3, ImageTitle4, ImageTitle5 },
                ImageAltTexts = new string[5] { ImageAltText1, ImageAltText2, ImageAltText3, ImageAltText4, ImageAltText5, }
            };

            return View(templateName, model.GetViewModel());
        }

        private string templateName = "Portfolio.Default";
    }
}