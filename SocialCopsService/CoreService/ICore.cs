using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICore
    {

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        bool TestConnection();

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveUser",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SaveUser(userItem user);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartyLogin",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool ThirdPartyLogin(userItem user);

       
    }


}
