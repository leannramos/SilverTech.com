using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;


namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "HomePageHero_MVC", Title = "HomePageHero", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class HomePageHeroController : CardController
    {
        public HomePageHeroController() : base() { }
    }
}