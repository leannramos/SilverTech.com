using Common.Classes.ViewModels;
using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Libraries;

namespace Common.Classes.Services
{
    public class ImageService : MediaService
    {

        public ImageService()
        {

        }
        public ImageViewModel GetFirstImage(object item, string columnName)
        {
            if (item != null)
            {
                var relatedImage = GetFirstRelatedItem(item, columnName);
                if (relatedImage != null)
                {
                    return GetImage(relatedImage.Id);
                }
            }

            return new ImageViewModel();
        }

        public ImageViewModel GetImage(Guid imageGuid)
        {
            var result = new ImageViewModel();

            var manager = LibrariesManager.GetManager();

            // Get the master version of the image
            var image = manager.GetImages().FirstOrDefault(i => i.Id == imageGuid);

            if (image != null)
            {
                result.ImageAlt = image.Description.ToString();
                result.ImageUrl = image.Url.ToString();
                var domainLength = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Length - System.Web.HttpContext.Current.Request.Url.AbsolutePath.Length;
                result.ImageRelativeUrl = image.Url.ToString().Substring(domainLength, image.Url.Length - domainLength);
            }

            return result;
        }
    }
}
