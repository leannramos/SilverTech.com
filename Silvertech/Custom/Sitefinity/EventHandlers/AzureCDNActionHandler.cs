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
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.GenericContent.Model;
using System.Threading.Tasks;
using System.Net.Http;
using Telerik.Sitefinity.Abstractions;
using SilverTech.Azure.Cdn.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Common.CustomConfig;

namespace SitefinityWebApp.EventHandlers
{
    public class AzureCdnActionHandler
    {

        /// <summary>
        /// The Content_Action process is run after any content management event ocurs in the Sitefinity CMS.
        /// This handler is looking to react to any content that would require purging the CDN.
        /// </summary>
        /// <param name="event">The event that occured.</param>
        public static void Content_Action(IDataEvent @event)
        {
 
            var config = Config.Get<AzureCdnConfig>();
            if (!config.InvalidateOnPublish)
            {
                Log.Write("CDN Invalidation Turned Off.", ConfigurationPolicy.Trace);
                return;
            }

            Log.Write("CDN Action handler Starting...", ConfigurationPolicy.Trace);

            var action = @event.Action;
            var contentType = @event.ItemType;
            var itemId = @event.ItemId;
            var providerName = @event.ProviderName;
            var itemStatus = "";
            var workflowStatus = "";
            var liveItemIsPublishing = false;
            var needtoPurge = false;
            var contentList = new List<string>();


            //check lifecycle status of item - Live, Draft, Master....
            var lifeCycleEvt = @event as Telerik.Sitefinity.Data.Events.ILifecycleEvent;
            if (lifeCycleEvt != null)
            {

                itemStatus = lifeCycleEvt.Status;
            }

            //Check Approval Workflow Status - Is the item published....
            var workflowEvt = @event as Telerik.Sitefinity.Data.Events.IApprovalWorkflowEvent;
            if (workflowEvt != null)
            {
                workflowStatus = workflowEvt.ApprovalWorkflowState;
            }


            //Only care about New, Updated, or Deleted items that are Live and Published
            if ((action == "Updated" || action == "New" || action != "Deleted")
                    && workflowStatus == "Published"
                    && itemStatus == "Live")
            {
                liveItemIsPublishing = true;
            }

            //Does this content require CDN to purge?
            //library items would be versioned and new - dont need to purge.
            switch (contentType.Name)
            {
                case "PageNode":
                    //really should grab url here and just purge that
                    //but what about menus...
                    //what about referenced content....
                    //maybe best to purge all...
                case "BlogPost":
                case "NewsItem":
                case "Event":
                case "Template":
                case "DynamicContent":

                    //add wildcard path - purge everything
                    needtoPurge = true;
                    contentList.Add("/*");
                    break;
            }



            //dO we care about this contenttype?
            if (needtoPurge && liveItemIsPublishing)
            {
                try
                {
                    Log.Write("Have an Event That requires CDN Purging.", ConfigurationPolicy.Trace);

                    var cdnService = new SitefinityAzureCdnService();
                    cdnService.Purge(contentList);
                }
                catch (Exception e)
                {
                    Log.Write("Error Purging CDN Content: \r\n" + e.ToString());
                }
            }

        }

    }
}