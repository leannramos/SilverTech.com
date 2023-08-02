using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "FactCard_MVC", Title = "Fact Card Widget", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class FactCardController : CardController
    {
        public FactCardController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "FactCard.Default";
        }
    }
}