using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Newsletters.Model;

namespace SitefinityWebApp.Mvc.Models.CustomSubscribe
{
    public class CustomSubscribeFormModel : ISubscribeFormModel
    {
        public CustomSubscribeFormModel() : base()
        {
            Heading = Heading;
            FieldLabel = FieldLabel;
            ButtonText = ButtonText;
        }

        public string Heading { get; set; }
        public string FieldLabel { get; set; }
        public string ButtonText { get; set; }
        public Guid SelectedMailingListId { get; set; }
        public SuccessfullySubmittedForm SuccessfullySubmittedForm { get; set; }
        public string CssClass { get; set; }
        public string Provider { get; set; }
        public Guid PageId { get; set; }

        public bool AddSubscriber(SubscribeFormViewModel viewModel, out string error)
        {
            error = string.Empty;

            if (NewsletterValidator.IsValidEmail(viewModel.Email))
            {
                var newslettersManager = NewslettersManager.GetManager(this.Provider);

                // check if subscriber exists
                var email = viewModel.Email.ToLower();
                IQueryable<Subscriber> matchingSubscribers = newslettersManager.GetSubscribers().Where(s => s.Email == email);
                bool subscriberAlreadyInList = false;
                foreach (Subscriber s in matchingSubscribers)
                {
                    if (s.Lists.Any(ml => ml.Id == this.SelectedMailingListId))
                    {
                        subscriberAlreadyInList = true;
                        break;
                    }
                }

                if (subscriberAlreadyInList)
                {
                    // If the email has already been subscribed, consider it as success.
                    if (this.SuccessfullySubmittedForm == SuccessfullySubmittedForm.OpenSpecificPage)
                    {
                        viewModel.RedirectPageUrl = HyperLinkHelpers.GetFullPageUrl(this.PageId);
                    }
                    return true;
                }
                else
                {
                    Subscriber subscriber = matchingSubscribers.FirstOrDefault();
                    if (subscriber == null)
                    {
                        subscriber = newslettersManager.CreateSubscriber(true);
                        subscriber.Email = viewModel.Email;
                        subscriber.FirstName = viewModel.FirstName != null ? viewModel.FirstName : string.Empty;
                        subscriber.LastName = viewModel.LastName != null ? viewModel.LastName : string.Empty;
                    }

                    // check if the mailing list exists
                    if (newslettersManager.Subscribe(subscriber, this.SelectedMailingListId))
                    {
                        if (this.SuccessfullySubmittedForm == SuccessfullySubmittedForm.OpenSpecificPage)
                        {
                            viewModel.RedirectPageUrl = HyperLinkHelpers.GetFullPageUrl(this.PageId);
                        }

                        newslettersManager.SaveChanges();

                        return true;
                    }
                }
            }
            error = Res.Get<SubscribeFormResources>().EmailAddressErrorMessageResourceName;
            return false;
        }

        public SubscribeFormViewModel CreateViewModel()
        {
            return new CustomSubscribeFormViewModel()
            {
                Heading = this.Heading,
                FieldLabel = this.FieldLabel,
                ButtonText = this.ButtonText,
                CssClass = this.CssClass
            };
        }
    }
}