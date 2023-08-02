using System;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using System.Xml;
using System.Text;

namespace SitefinityWebApp
{
    static public class ImageProcessor
    {
        static public string Process(Guid imageId)
        {
            string output = "";


            var librariesManager = LibrariesManager.GetManager();
            var image = librariesManager.GetImage(imageId);

            BlobDownloadStream imageStream = librariesManager.Download(image) as BlobDownloadStream;

            if (imageStream != null)
            {
                var bytesInStream = new byte[imageStream.Length];
                imageStream.Read(bytesInStream, 0, bytesInStream.Length);

                XmlDocument doc = new XmlDocument();
                string xml = Encoding.UTF8.GetString(bytesInStream);

                output = xml;
            }

            return output;
        }
    }
}