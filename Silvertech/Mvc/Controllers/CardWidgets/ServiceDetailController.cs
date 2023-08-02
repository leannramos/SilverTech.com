using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "ServiceDetail_MVC", Title = "Service Detail", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class ServiceDetailController : CardController
    {
        public ServiceDetailController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "Card";
        }
    }
}