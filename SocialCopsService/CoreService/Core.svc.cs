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
        public int SaveComplaint(complaintItem complaint)
        {
            cc = new ComplaintController();
            int result = cc.SaveComplaint(complaint);
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

        #region GetComplaints/{id}
        public complaintItem GetComplaintsById(string id)
        {
            cc = new ComplaintController();
            //complaintItem result = cc.GetComplaints(id);
            return null;
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
            cc = new ComplaintController();
            //complaintItem[] result = cc.GetComplaintsByUserId(userId);
            return null;
        }
        #endregion

        #region GetComplaintsByAuthId/{authId}
        public complaintItem[] GetComplaintsByAuthId(string authId)
        {
            cc = new ComplaintController();
            //complaintItem[] result = cc.GetComplaintsByAuthId(authId);
            return null;
        }
        #endregion
    }
}
