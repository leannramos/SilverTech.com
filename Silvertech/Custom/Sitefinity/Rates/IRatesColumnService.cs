using BankCore.Mvc.Models.Rates;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SitefinityWebApp.Sitefinity.Services.Rates
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRatesColumnService" in both code and config file together.
    [ServiceContract]
    public interface IRatesColumnService
    {
        [WebInvoke(Method = "POST", UriTemplate = "/GetColumns/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        List<RowDataViewModel> GetColumns(TableRequestModel tableRequest);
    }
}
