using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "InstagramFeed_MVC", Title = "Instagram Feed", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class InstagramFeedController : CardController
    {
        public InstagramFeedController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "InstagramFeed.Default";
        }
    }
}