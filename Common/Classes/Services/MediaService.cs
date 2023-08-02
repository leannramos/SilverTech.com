using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;

namespace Common.Classes.Services
{
    public class MediaService
    {
        public IDataItem GetFirstRelatedItem(object item, string columnName)
        {
            return GetRelatedItems(item, columnName).FirstOrDefault();
        }

        public IEnumerable<IDataItem> GetRelatedItems(object item, string columnName)
        {
            IEnumerable<IDataItem> result = new List<IDataItem>();

            try
            {
                result = item.GetRelatedItems(columnName);
            }
            catch (System.Exception e)
            {
                string errorMessage = e.Message + "\r\nAttempted to access column: " + columnName + " from item: " + item.ToString();
                Telerik.Sitefinity.Abstractions.Log.Write(errorMessage);
            }

            return result;
        }
    }
}

