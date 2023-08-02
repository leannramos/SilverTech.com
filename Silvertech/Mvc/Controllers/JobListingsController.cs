using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "JobListings_MVC", Title = "Job Listings", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    [EnhanceViewEngines]
    public class JobListingsController : Controller
    {
        [Display(Name = "Javascript File")]
        public string Javascript { get; set; }
        [Display(Name = "Javascript File 2")]
        public string Javascript2 { get; set; }
        [Display(Name = "Heading")]
        public string Heading { get; set; }
        [Display(Name = "Heading 2")]
        public string Heading2 { get; set; }
        public string Description { get; set; }

        public string GetResult(string Uri)
        {
            string results = "";

            if (Uri != null && Uri.EndsWith(".js"))
            {
                var request = WebRequest.Create(Uri);

                var response = request.GetResponse();

                Stream resultsStream = response.GetResponseStream();

                using (StreamReader reader = new StreamReader(resultsStream))
                {
                    results += WebUtility.HtmlDecode(reader.ReadToEnd());
                }
            }

            return results;
        }

        private List<HtmlString> ParseResult(string originalResult)
        {
            if (originalResult.IsNullOrEmpty())
            {
                return new List<HtmlString>();
            }

            List<HtmlString> finalResults = new List<HtmlString>();
            string[] writtenResults = originalResult.Split(new string[] { "document.write(\"" }, StringSplitOptions.None);
            string openPattern = "<h.*?(>){1}";
            string closePattern = "<\\/h.*?(>){1}";

            foreach (var writtenResult in writtenResults)
            {
                string newline = writtenResult;

                newline = Regex.Replace(newline, openPattern, "");
                newline = Regex.Replace(newline, closePattern, "");
                if (!newline.Contains("class="))
                {
                    newline = Regex.Replace(newline, "<a", "<a class=\"btn btn-quaternary\"");
                }
                else
                {
                    newline = Regex.Replace(newline, "class=\"", "class=\"btn btn-quaternary ");
                }

                if (newline.Contains("<a"))
                {
                    finalResults.Add(new HtmlString(newline.Replace("\\\"", "\"")
                        .Replace("\");", "")));
                }
            }

            return finalResults;
        }
        public ActionResult Index()
        {
            string results = Javascript != "" ? GetResult(Javascript) : "";
            string results2 = Javascript2 != "" ? GetResult(Javascript2) : "";

            var vm = new JobListingsViewModel {
                Listings = ParseResult(results),
                Listings2 = ParseResult(results2),
                Heading = this.Heading,
                Heading2 = this.Heading2,
                Description = this.Description
            };

            if (vm.Heading == null || vm.Heading2 == null)
            {
                return View("Single", vm);
            }
            else
            {
                return View("Double", vm);
            }  
        }
    }
}