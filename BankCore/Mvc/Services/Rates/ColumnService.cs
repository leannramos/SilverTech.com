using BankCore.Mvc.Models.Rates;
using Common.Classes;
using Telerik.Sitefinity.DynamicModules.Model;

namespace BankCore.Mvc.Services.Rates
{
    public class ColumnService : DynamicContentService<ColumnViewModel>
    {
        public ColumnService(string type = "Telerik.Sitefinity.DynamicTypes.Model.Rates.Column", string providerName = "", bool AutoInitialize = true) : base(type, providerName, AutoInitialize)
        {
        }

        protected override ColumnViewModel BuildModel(DynamicContent item)
        {
            var cvm = new ColumnViewModel();
            cvm.Title = item.GetStringValue("Title");
            cvm.Id = item.Id;
            cvm.SubTitle = item.GetStringValue("Subtitle","");
            cvm.OriginalContentId = item.OriginalContentId;
            return cvm;
        }
    }
}
