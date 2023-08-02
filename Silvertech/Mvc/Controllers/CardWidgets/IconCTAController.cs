using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "IconCTA_MVC", Title = "Icon CTA", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class IconCTAController : CardController
    {
        public IconCTAController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "Card";
        }
    }
}