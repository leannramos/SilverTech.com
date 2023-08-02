using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;

namespace SitefinityWebApp.EventHandlers
{
    public class SVGImageActionHandler
    {

        /* 
          * The Content_Action process is run after any file management call is made in the Sitefinity CMS.
          * This event processes and stores any SVG images' contents in the database when uploaded, to enable the use of fully-featured SVG images.
         */
        public static void Content_Action(IDataEvent @event)
        {
            var action = @event.Action;
            var contentType = @event.ItemType;
            var itemId = @event.ItemId;
            var providerName = @event.ProviderName;

            if (contentType.FullName == typeof(Telerik.Sitefinity.Libraries.Model.Image).FullName​) // Check type before getting manager and handling the event further.
            {
                var manager = ManagerBase.GetMappedManager(contentType, providerName) ;

                if (action != "New" || manager == null)
                {
                    return;
                }

                var item = manager.GetItemOrDefault(contentType, itemId);
                var imageMaster = item as Image;

                if (imageMaster.MimeType == "image/svg+xml")
                {
                    var librariesManager = manager as LibrariesManager;

                    try
                    {
                        var markupProperty = imageMaster.GetValue("SvgMarkup");

                        if (markupProperty != null)
                        {
                            imageMaster.SetValue("SvgMarkup", ImageProcessor.Process(itemId));
                        }
                    }
                    catch { }

                    librariesManager.SaveChanges();
                }
            }
        }
    }
}