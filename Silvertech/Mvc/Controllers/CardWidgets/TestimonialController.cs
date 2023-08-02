using System;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers.CardWidgets
{
    [ControllerToolboxItem(Name = "Testimonial_MVC", Title = "Testimonial Widget", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class TestimonialController : CardController
    {
        public TestimonialController() : base()
        {
            TemplateName = !TemplateName.IsNullOrEmpty() ? TemplateName : "CaseStudy.CaseStudyCard";
        }
    }
}