using System;
using Telerik.Sitefinity.Web;

namespace Common.Helpers
{
    public class CustomPageHelpers
    {

        public bool HasCustomPageValue<T>(string fieldName, out T fieldValue)
        {
            fieldValue = default(T);
            try
            {
                PageSiteNode currentNode = SiteMapBase.GetActualCurrentNode() as PageSiteNode;
                //Templates are not nodes so design mode will throw exceptions otherwise.
                if (currentNode == null)
                    return false;

                var requestedField = currentNode.GetCustomFieldValue(fieldName);

                if (requestedField != null)
                {
                    fieldValue = (T)requestedField;
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Telerik.Sitefinity.Abstractions.Log.Write(Ex, Telerik.Sitefinity.Abstractions.ConfigurationPolicy.ErrorLog);
                return false;
            }
            return false;
        }

        public bool HasCustomPageObject<T>(string fieldName, out T fieldValue) where T : class
        {
            fieldValue = default(T);

            PageSiteNode currentNode = SiteMapBase.GetActualCurrentNode() as PageSiteNode;
            //Templates are not nodes so design mode will throw exceptions otherwise.
            if (currentNode == null)
                return false;

            T requestedField = currentNode.GetCustomFieldValue(fieldName) as T;

            if (requestedField != null)
            {
                fieldValue = requestedField;
                return true;
            }

            return false;
        }

        public bool GetCustomPage(out PageSiteNode fieldValue)
        {
            fieldValue = null;
            try
            {
                PageSiteNode currentNode = SiteMapBase.GetActualCurrentNode() as PageSiteNode;
                //Templates are not nodes so design mode will throw exceptions otherwise.
                if (currentNode == null)
                {
                    return false;
                }
                else
                {
                    fieldValue = currentNode;
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Telerik.Sitefinity.Abstractions.Log.Write(Ex, Telerik.Sitefinity.Abstractions.ConfigurationPolicy.ErrorLog);
                return false;
            }
        }
    }
}
