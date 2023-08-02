using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Common.CustomConfig;

namespace SitefinityWebApp.Mvc.Models
{
	public class RecaptchaModel : FormElementModel, IRecaptchaModel
	{
		internal const string GRecaptchaParameterDataSiteKey = "GRecaptchaDataSiteKey";
		internal const string GRecaptchaParameterSecretKey = "GRecaptchaSecretKey";
		private string dataSitekey = string.Empty;
		private string secret = string.Empty;

		/// <inheritDocs/>
		public string Theme { get; set; } = "light";

		/// <inheritDocs/>
		public string DataType { get; set; } = "image";

		public bool ShowError { get; set; } = false;

        /// <inheritDocs/>
        public string Size { get; set; } = "normal";		

		/// <inheritDocs/>
		public string DataSitekey
		{
			get
			{
				if (string.IsNullOrEmpty(this.dataSitekey))
				{
					var GlobalConfig = Config.Get<GlobalConfig>();
					this.dataSitekey = GlobalConfig.RecaptchaSiteKey;
				}

				return this.dataSitekey;
			}
			set
			{
				this.dataSitekey = value;
			}
		}

		/// <inheritDocs/>
		public string Secret
		{
			get
			{
				if (string.IsNullOrEmpty(this.secret))
				{
					var GlobalConfig = Config.Get<GlobalConfig>();
					this.secret = GlobalConfig.RecaptchaSecretKey;
				}

				return this.secret;
			}
			set
			{
				this.secret = value;
			}
		}

        /// <inheritDocs/>
        public long ValidationTimeoutMiliseconds { get; set; } = 10000;

		/// <inheritDocs/>
		public bool DisplayOnlyForUnauthenticatedUsers
		{
			get;
			set;
		}

		/// <inheritDocs/>
		public override object Value
		{
			get
			{
				if (base.Value == null)
					base.Value = System.Web.HttpContext.Current.Request["g-recaptcha-response"];

				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		/// <inheritDocs/>
		public override object GetViewModel(object value)
		{
			var viewModel = new RecaptchaViewModel()
			{
				DataType = this.DataType,
				Size = this.Size,
				Theme = this.Theme,
				DataSitekey = this.DataSitekey,
				ShowError = this.ShowError
			};

			return viewModel;
		}

		/// <inheritDocs/>
		public bool ShouldRenderCaptcha()
		{
			var isVisible = !(SystemManager.CurrentHttpContext.User != null &&
					SystemManager.CurrentHttpContext.User.Identity != null &&
					SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated &&
					this.DisplayOnlyForUnauthenticatedUsers &&
					!SystemManager.IsDesignMode);

			return isVisible;
		}

		/// <inheritDocs/>
		public override bool IsValid(object value)
		{
			if (!this.ShouldRenderCaptcha())
			{
				return true;
			}

			if (string.IsNullOrEmpty(value as string))
			{
				this.ShowError = true;
				//throw new Telerik.Sitefinity.Services.Events.ValidationException("Error");
				return false;
			}

			try
			{
				using (var client = new HttpClient())
				{
					client.Timeout = TimeSpan.FromMilliseconds(this.ValidationTimeoutMiliseconds);

					var values = new Dictionary<string, string>
					{
					   { "secret", this.Secret },
					   { "response", value as string }
					};

					var response = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(values)).Result;
					var responseString = response.Content.ReadAsStringAsync().Result;
					var responseModel = JsonConvert.DeserializeObject<GRecaptchaValidationResponseModel>(responseString);

					if (responseModel.ErrorCodes != null && responseModel.ErrorCodes.Contains("invalid-input-secret"))
						Log.Write("Invalid input secret for reCaptcha. Please configure in advanced settings parameters for Forms.");

					this.ShowError = !responseModel.Success;

					

					return responseModel.Success;
				}
			}
			catch (Exception ex)
			{
				Log.Write("Exception while validating reCaptcha field: " + ex.Message);
				this.ShowError = true;
				return false;
			}
		}

		private class GRecaptchaValidationResponseModel
		{
			[JsonProperty("success")]
			public bool Success { get; set; }

			[JsonProperty("error-codes")]
			public IEnumerable<string> ErrorCodes { get; set; }
		}
	}
}