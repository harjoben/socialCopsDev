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
        int SaveUser(userItem user);

        [FaultContract(typeof(Error_Handling.Bug))]
        bool SaveProfilePic(int id, string url);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartyLogin",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int ThirdPartyLogin(userItem user);

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
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByCity/{city}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByCity(string city);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByState/{state}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByState(string state);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByCountry/{country}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByCountry(string country);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComplaintsByPin/{pin}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        complaintItem[] GetComplaintsByPin(string pin);

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

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetComments/{complaintId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        commentItem[] GetComments(string complaintId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetCommentsByUserId/{userId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        commentItem[] GetCommentsByUserId(string userId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetLikes/{complaintId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        likeItem[] GetLikes(string complaintId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetLikesByUserId/{userId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        likeItem[] GetLikesByUserId(string userId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveComment",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SaveComment(commentItem comment);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveLike",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SaveLike(likeItem like);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetSpam/{complaintId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        spamItem[] GetSpam(string complaintId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetSpamByUserId/{userId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        spamItem[] GetSpamByUserId(string userId);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveSpam",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SaveSpam(spamItem spam);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsers",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsers();

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUserById/{id}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem GetUserById(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByName/{name}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByName(string name);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUserByEmail/{email}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem GetUserByEmail(string email);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByPoints/{points}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByPoints(string points);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByRank/{rank}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByRank(string rank);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByCity/{city}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByCity(string city);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByState/{state}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByState(string state);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByCountry/{country}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByCountry(string country);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByPin/{pin}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByPin(string pin);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetUsersByComplaints/{num}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetUsersByComplaints(string num);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveSubscription",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SaveSubscription(subscriptionItem subscription);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetSubscribers/{id}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetSubscribers(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetSubscribedTo/{id}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        userItem[] GetSubscribedTo(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuths",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuths();

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthById/{id}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem GetAuthById(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByName/{name}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByName(string name);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthByEmail/{email}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem GetAuthByEmail(string email);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByCity/{city}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByCity(string city);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByState/{state}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByState(string state);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByCountry/{country}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByCountry(string country);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByPin/{pin}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByPin(string pin);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsByPending/{num}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsByPending(string num);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetAuthsBySolved/{num}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        authorityItem[] GetAuthsBySolved(string num);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "POST", UriTemplate = "SaveMuteAuth",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int SaveMuteAuth(muteAuthorityItem muteAuth);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuths",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuths();

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsById/{id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsById(string id);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByName/{name}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByName(string name);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByCity/{city}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByCity(string city);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByState/{state}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByState(string state);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByCountry/{country}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByCountry(string country);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByEmail/{email}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByEmail(string email);

        [OperationContract]
        [FaultContract(typeof(Error_Handling.Bug))]
        [WebInvoke(Method = "GET", UriTemplate = "GetMuteAuthsByPin/{pin}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        muteAuthorityItem[] GetMuteAuthsByPin(string pin);

    }


}
