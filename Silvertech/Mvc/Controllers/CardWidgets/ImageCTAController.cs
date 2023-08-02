using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "ImageCTA_MVC", Title = "Image CTA", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class ImageCTAController : CardController
    {
        public ImageCTAController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "Card.ImageLeft";
        }
    }
}