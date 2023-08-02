using System;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace ConservationInternational.Sitefinity.Public.Admin
{
    public partial class CustomSiteSettings : SimpleView
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                //we use div wrapper tag to make easier common styling
                return HtmlTextWriterTag.Div;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return CustomSiteSettings.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        protected virtual TextField GTMHead
        {
            get
            {
                return this.Container.GetControl<TextField>("gTMHead", true);
            }
        }
        protected virtual TextField GTMBody
        {
            get
            {
                return this.Container.GetControl<TextField>("gTMBody", true);
            }
        }
        #endregion

        protected virtual TextField TwitterCard
        {
            get
            {
                return this.Container.GetControl<TextField>("twitterCard", true);
            }
        }

        protected virtual TextField TwitterSite
        {
            get
            {
                return this.Container.GetControl<TextField>("twitterSite", true);
            }
        }

        protected virtual TextField TwitterTitle
        {
            get
            {
                return this.Container.GetControl<TextField>("twitterTitle", true);
            }
        }

        protected virtual TextField TwitterDescription
        {
            get
            {
                return this.Container.GetControl<TextField>("twitterDescription", true);
            }
        }

        protected virtual TextField TwitterImage
        {
            get
            {
                return this.Container.GetControl<TextField>("twitterImage", true);
            }
        }
        #region Methods
        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
        }



        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/Sitefinity/Public/Admin/SiteSettings.ascx";
        #endregion
    }

}