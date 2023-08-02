using Common.Classes.Services;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.PublishingSystem;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.DynamicModules.Builder;


namespace SitefinityWebApp.Custom.Sitefinity.Search
{
    public class LocationsInboundPipe : DynamicContentInboundPipe
    {

        protected override void SetProperties(WrapperObject wrapperObject, DynamicContent contentItem)
        {
            base.SetProperties(wrapperObject, contentItem);

            //get the related document in  related data field named: Document.
            var imageUrl = "";
            var imageAlt = "";
            var image = new ImageService().GetFirstImage(contentItem, "LocationThumbnail");
            if (image != null)
            {
                imageUrl = image.ImageUrl;
                imageAlt = image.ImageAlt;
            }
            wrapperObject.SetOrAddProperty("Link", contentItem.ItemDefaultUrl);
            wrapperObject.SetOrAddProperty("LocationThumbnailImageUrl", imageUrl);
            wrapperObject.SetOrAddProperty("LocationThumbnailImageAlt", imageAlt);
        }

        public static void Register()
        {
            var modules = ModuleBuilderManager.GetManager().Provider.GetDynamicModuleTypes().Where(t => t.ModuleName == "Locations" && t.DisplayName== "Location").FirstOrDefault();

            if(modules != null)
            {
                var pipeName = string.Format("{0}Pipe", modules.GetFullTypeName());

                PublishingSystemFactory.UnregisterPipe(pipeName);
                PublishingSystemFactory.RegisterPipe(pipeName, typeof(LocationsInboundPipe));
            }
        }
    }
}