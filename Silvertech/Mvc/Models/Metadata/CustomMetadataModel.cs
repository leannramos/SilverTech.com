using System.Collections.Generic;

namespace SitefinityWebApp
{
    public class CustomMetadataModel
    {
        public string TwitterDescription {
            get {
                return twitterDescription;
            }
            set {
                twitterDescription = value;
            }
        }
        public string TwitterTitle { get; set; }
        public string TwitterImage { get; set; }
        public string TwitterSite { get; set; }
        public string TwitterCard {
            get {
                if (!string.IsNullOrWhiteSpace(twitterDescription))
                {
                    twitterCard = "summary";
                }
                return twitterCard;
            }
            set { twitterCard = value; }
        }

        public static Dictionary<string, string[]> Settings { get; }

        static CustomMetadataModel()
        {
            Settings = new Dictionary<string, string[]>
            {
                {"TwitterDescription", new string[2] {"twitter:description", "name"} },
                {"TwitterTitle",new string[2] { "twitter:title", "name"} },
                {"TwitterImage", new string[2] { "twitter:image", "name"} },
                {"TwitterSite", new string[2] { "twitter:site", "name"} },
                {"TwitterCard", new string[2] { "twitter:card", "name"} }
            };
        }

        public bool IsEmpty()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if ((property.GetValue(this) != null && property.GetValue(this).ToString() != "") && property.Name != "Settings")
                {
                    return false;
                }
            }

            return true;
        }

        private string twitterDescription;
        private string twitterCard;
    }
}
