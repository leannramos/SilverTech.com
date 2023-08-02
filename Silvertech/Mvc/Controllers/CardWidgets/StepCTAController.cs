using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "StepCTA_MVC", Title = "Step CTA", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class StepCTAController : CardController
    {
        public StepCTAController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "Steps.ImageLeft";
        }
    }
}