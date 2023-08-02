using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "InnerHero_MVC", Title = "Inner Page Hero", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class InnerHeroController : CardController
    {
        public InnerHeroController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "InnerHero.Default";
        }
    }
}