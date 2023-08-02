using System;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace SitefinityWebApp.Mvc.Models.Document
{
    public class CustomDownloadDocumentModel : DocumentModel
    {
        public string LinkTitle { get; set; }
        public string LinkSubTitle { get; set; }
        public string LinkText { get; set; }

        public override DocumentViewModel GetViewModel()
        {
            var viewModel = new CustomDownloadViewModel()
            {
                CssClass = this.CssClass
            };

            if (this.Id != Guid.Empty)
            {
                SfDocument document;
                viewModel.DocumentWasNotFound = !this.TryGetDocument(out document);

                if (viewModel.DocumentWasNotFound)
                    return viewModel;

                viewModel.MediaUrl = this.ResolveMediaUrl(document);
                viewModel.Title = this.GetTitle(document);
                viewModel.FileSize = this.GetFileSize(document);
                viewModel.Extension = this.GetExtension(document);
                viewModel.LinkTitle = this.LinkTitle;
                viewModel.LinkSubTitle = this.LinkSubTitle;
                viewModel.LinkText = this.LinkText;
            }
            else
            {
                viewModel.DocumentWasNotFound = true;
            }

            return viewModel;
        }
    }
}