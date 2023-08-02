using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "TextCTA_MVC", Title = "Text CTA", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class TextCTAController : CardController
    {
        public TextCTAController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "Card.HighlightLeft";
        }
    }
}