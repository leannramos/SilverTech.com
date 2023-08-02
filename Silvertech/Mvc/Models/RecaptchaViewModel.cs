﻿namespace SitefinityWebApp.Mvc.Models
{
    public class RecaptchaViewModel
	{
		/// <summary>
		/// Gets or sets the  color theme of the widget.
		/// </summary>
		/// <value>
		/// The theme.
		/// </value>
		public string Theme { get; set; }

		/// <summary>
		/// Gets or sets the type of CAPTCHA to serve.
		/// </summary>
		/// <value>
		/// The type of the media.
		/// </value>
		public string DataType { get; set; }

		/// <summary>
		/// Gets or sets the size of the widget.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public string Size { get; set; }

		/// <summary>
		/// Gets or sets the data sitekey.
		/// </summary>
		/// <value>
		/// The data sitekey.
		/// </value>
		public string DataSitekey { get; set; }

		public bool ShowError { get; set; }
	}
}