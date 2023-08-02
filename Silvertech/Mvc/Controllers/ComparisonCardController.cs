using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;


namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "ComparisonCard_MVC", Title = "ComparisonCard", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class ComparisonCardController : CardController
    {
        public ComparisonCardController() : base() { }
    }
}