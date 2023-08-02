using System;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
    public static class UserExtensionMethods
    {
        public static string DefaultAvatar = "/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png";

        public static string GetAvatar(this User user, string defaultImageUrl = ""){
            Image image;
            UserProfilesHelper.GetAvatarImageUrl(user.Id, out image);

            if (image != null)
            {
                return image.Url;
            }
            else
            {
                return (String.IsNullOrEmpty(defaultImageUrl)) ? DefaultAvatar : defaultImageUrl;
            }
        }

    }
}