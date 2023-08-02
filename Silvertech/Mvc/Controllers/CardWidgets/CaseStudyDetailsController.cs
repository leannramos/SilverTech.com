using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "CaseStudyDetails_MVC", Title = "Case Study Details", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class CaseStudyDetailsController : CardController
    {
        public CaseStudyDetailsController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "CaseStudyDetails.TwoColumnInfo";
        }
    }
}