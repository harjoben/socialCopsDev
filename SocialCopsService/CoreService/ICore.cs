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

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveComplaint",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int SaveComplaint(complaintItem complaint);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaints",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaints();

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintById/{id}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem GetComplaintsById(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByCategory/{category}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByCategory(string category);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByStatus/{complaintStatus}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByStatus(string complaintStatus);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByUserId/{userId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByUserId(string userId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByAuthId/{authId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByAuthId(string authId);
    }


}
