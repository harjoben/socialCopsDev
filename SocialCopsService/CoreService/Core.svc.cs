using CoreService.Controllers;
using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Core : ICore
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        UserController uc;
        ComplaintController cc;
        AuthorityController ac;

        #region TestConnection
        public  bool TestConnection()
        {
            
            try
            {
                logger.LogMethod("varun", "TestConnection", "Enter Method", null);

                logger.LogMethod("varun", "TestConnection", "Exit Method", null);
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later.";
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            } 
        }
        #endregion

        #region SaveUser
        public bool SaveUser(userItem user)
        {
            uc = new UserController();
            bool result = uc.SaveUser(user);
            return result;

        }
        #endregion

        #region thirdPartyLogin
        public bool ThirdPartyLogin(userItem user)
        {
            uc = new UserController();
            bool result = uc.thirdPartyLogin(user);
            return result;
        }
        #endregion

        #region SaveComplaint
        public bool SaveComplaint(complaintItem complaint)
        {
            cc = new ComplaintController();
            bool result = cc.SaveComplaint(complaint);
            return result;
        }
        #endregion 

        #region GetComplaints
        public complaintItem[] GetComplaints()
        {
            cc = new ComplaintController();
            complaintItem[] result = cc.GetComplaints();
            return result;
            
        }
        #endregion

        #region GetComplaintsById/{id}
        public complaintItem GetComplaintsById(string id)
        {
            cc = new ComplaintController();
            complaintItem result = cc.GetComplaintsById(id);
            return result;
        }
        #endregion

        #region GetComplaintsByCategory/{category}
        public complaintItem[] GetComplaintsByCategory(string category)
        {
            cc = new ComplaintController();
            complaintItem[] result = cc.GetComplaintsByCategory(category);
            return result;
        }
        #endregion

        #region GetComplaintsByStatus/{complaintStatus}
        public complaintItem[] GetComplaintsByStatus(string complaintStatus)
        {
            cc = new ComplaintController();
            complaintItem[] result = cc.GetComplaintsByStatus(complaintStatus);
            return result;
        }
        #endregion

        #region GetComplaintsByUserId/{userId}
        public complaintItem[] GetComplaintsByUserId(string userId)
        {
            uc = new UserController();
            complaintItem[] result = uc.GetComplaintsByUserId(userId);
            return result;
        }
        #endregion

        #region GetComplaintsByAuthId/{authId}
        public complaintItem[] GetComplaintsByAuthId(string authId)
        {
            ac = new AuthorityController();
            complaintItem[] result = ac.GetComplaintsByAuthId(authId);
            return result;
        }
        #endregion

        #region GetComments/{complaintId}
        public commentItem[] GetComments(string complaintId)
        {
            cc = new ComplaintController();
            commentItem[] result = cc.GetComments(complaintId);
            return result;
        }
        #endregion

        #region GetCommentsByUserId/{userId}
        public commentItem[] GetCommentsByUserId(string userId)
        {
            uc = new UserController();
            commentItem[] result = uc.GetCommentsByUserId(userId);
            return result;
        }
        #endregion

        #region GetLikes/{complaintId}
        public likeItem[] GetLikes(string complaintId)
        {
            cc = new ComplaintController();
            likeItem[] result = cc.GetLikes(complaintId);
            return result;
        }
        #endregion

        #region GetLikesByUserId/{userId}
        public likeItem[] GetLikesByUserId(string userId)
        {
            uc = new UserController();
            likeItem[] result = uc.GetLikesByUserId(userId);
            return result;
        }
        #endregion

        #region SaveComment
        public bool SaveComment(commentItem comment)
        {
            cc = new ComplaintController();
            bool result = cc.SaveComment(comment);
            return result;
        }
        #endregion

        #region SaveLike
        public bool SaveLike(likeItem like)
        {
            cc = new ComplaintController();
            bool result = cc.SaveLike(like);
            return result;
        }
        #endregion

        #region GetSpam/{complaintId}
        public spamItem[] GetSpam(string complaintId)
        {
            cc = new ComplaintController();
            spamItem[] result = cc.GetSpam(complaintId);
            return result;
        }
        #endregion

        #region GetSpamByUserId/{userId}
        public spamItem[] GetSpamByUserId(string userId)
        {
            uc = new UserController();
            spamItem[] result = uc.GetSpamByUserId(userId);
            return result;
        }
        #endregion

        #region SaveSpam
        public bool SaveSpam(spamItem spam)
        {
            cc = new ComplaintController();
            bool result = cc.SaveSpam(spam);
            return result;
        }
        #endregion

        #region GetUsers
        public userItem[] GetUsers()
        {
            uc = new UserController();
            userItem[] result = uc.GetUsers();
            return result;
        }
        #endregion

        #region GetUserById/{id}
        public userItem GetUserById(string id)
        {
            uc = new UserController();
            userItem result = uc.GetUserById(id);
            return result;
        }
        #endregion

        #region GetUsersByName/{name}
        public userItem[] GetUsersByName(string name)
        {
            uc = new UserController();
            userItem[] result = uc.GetUsersByName(name);
            return result;
        }
        #endregion

        #region GetUserByEmail/{email}
        public userItem GetUserByEmail(string email)
        {
            uc = new UserController();
            userItem result = uc.GetUserByEmail(email);
            return result;
        }
        #endregion

        #region GetUsersByPoints/{points}
        public userItem[] GetUsersByPoints(string points)
        {
            uc = new UserController();
            userItem[] result = uc.GetUsersByPoints(points);
            return result;
        }
        #endregion

        #region GetUsersByRank/{rank}
        public userItem[] GetUsersByRank(string rank)
        {
            uc = new UserController();
            userItem[] result = uc.GetUsersByRank(rank);
            return result;
        }
        #endregion

        #region GetUsersByComplaints/{num}
        public userItem[] GetUsersByComplaints(string num)
        {
            uc = new UserController();
            userItem[] result = uc.GetUsersByComplaints(num);
            return result;
        }
        #endregion

        #region SaveSubscription
        public bool SaveSubscription(subscriptionItem subscription)
        {
            uc = new UserController();
            bool result = uc.SaveSubscription(subscription);
            return result;
        }
        #endregion

        #region GetSubscribers/{id}
        public userItem[] GetSubscribers(string id)
        {
            uc = new UserController();
            userItem[] result = uc.GetSubscribers(id);
            return result;
        }
        #endregion

        #region GetSubscribedTo/{id}
        public userItem[] GetSubscribedTo(string id)
        {
            uc = new UserController();
            userItem[] result = uc.GetSubscribedTo(id);
            return result;
        }
        #endregion

        #region SaveAuth
        public bool SaveAuth(authorityItem auth)
        {
            ac = new AuthorityController();
            bool result = ac.SaveAuth(auth);
            return result;
        }
        #endregion

        #region GetAuths
        public authorityItem[] GetAuths()
        {
            ac = new AuthorityController();
            authorityItem[] result = ac.GetAuths();
            return result;
        }
        #endregion

        #region GetAuthById/{id}
        public authorityItem GetAuthById(string id)
        {
            ac = new AuthorityController();
            authorityItem result = ac.GetAuthById(id);
            return result;
        }
        #endregion

        #region GetAuthsByName/{name}
        public authorityItem[] GetAuthsByName(string name)
        {
            ac = new AuthorityController();
            authorityItem[] result = ac.GetAuthsByName(name);
            return result;
        }
        #endregion

        #region GetAuthByEmail/{email}
        public authorityItem GetAuthByEmail(string email)
        {
            ac = new AuthorityController();
            authorityItem result = ac.GetAuthByEmail(email);
            return result;
        }
        #endregion

        #region GetAuthsByPending/{num}
        public authorityItem[] GetAuthsByPending(string num)
        {
            ac = new AuthorityController();
            authorityItem[] result = ac.GetAuthsByPending(num);
            return result;
        }
        #endregion

        #region GetAuthsBySolved/{num}
        public authorityItem[] GetAuthsBySolved(string num)
        {
            ac = new AuthorityController();
            authorityItem[] result = ac.GetAuthsBySolved(num);
            return result;
        }
        #endregion
    }
}
