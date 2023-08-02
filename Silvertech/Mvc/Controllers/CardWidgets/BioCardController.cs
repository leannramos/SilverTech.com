using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "BioCard_MVC", Title = "Bio Card Widget", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class BioCardController : CardController
    {
        public BioCardController() : base() { }
    }
}