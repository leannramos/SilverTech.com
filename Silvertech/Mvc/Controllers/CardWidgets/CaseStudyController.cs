using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "CaseStudy_MVC", Title = "Case Study Widget", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class CaseStudyController : CardController
    {
        public CaseStudyController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "CaseStudy.CaseStudyCard";
        }
    }
}