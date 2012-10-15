using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Configuration;
using CoreService.Cache;

namespace CoreService.Controllers
{
    public class ComplaintController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        socialcopsentity context;
        string key;

        #region SaveComplaint
        public int SaveComplaint(complaintItem complaint)
        {
            try
            {
                logger.LogMethod("jo", "SaveComplaint", "Enter", null);
                context = new socialcopsentity();

                Complaint temp = new Complaint();
                temp.complaintId = complaint.complaintId;
                temp.userId = complaint.userId;
                temp.title = complaint.title;
                temp.details = complaint.details;
                temp.numLikes = (int)complaint.numLikes;
                temp.numComments = (int)complaint.numComments;
                temp.picture = complaint.picture;
                temp.complaintDate = (DateTime)complaint.complaintDate;
                temp.location = complaint.location;
                temp.latitude = (float)complaint.latitude;
                temp.longitude = (float)complaint.longitude;
                temp.category = complaint.category;
                temp.complaintStatus = complaint.complaintStatus;
                temp.date = complaint.date;
                temp.isAnonymous = complaint.isAnonymous;
                
                context.Complaints.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveUser", "Exit", null);
                return complaint.complaintId;
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

        #region GetComplaints
        public complaintItem[] GetComplaints()
        {
            try
            {
                logger.LogMethod("jo", "GetComplaints", "Enter");
                key = "GetComplaints";
                List<complaintItem> list = new List<complaintItem>();
                context = new socialcopsentity();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaints", "Cache Found");
                        return list.ToArray();
                    }
                }

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              orderby c.complaintDate descending
                                              select c).Take(50).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint.complaintId = temp.complaintId;
                    complaint.userId = temp.userId;
                    complaint.title = temp.title;
                    complaint.details = temp.details;
                    complaint.numLikes = (int)temp.numLikes;
                    complaint.numComments = (int)temp.numComments;
                    complaint.picture = temp.picture;
                    complaint.complaintDate = (DateTime)temp.complaintDate;
                    complaint.location = temp.location;
                    complaint.latitude = (float)temp.latitude;
                    complaint.longitude = (float)temp.longitude;
                    complaint.category = temp.category;
                    complaint.complaintStatus = temp.complaintStatus;
                    complaint.date = temp.date;
                    complaint.isAnonymous = temp.isAnonymous;

                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaints", "Exit");
                return list.ToArray();

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

        #region GetComplaints/{id}
        //public complaintItem GetComplaints(string id)
        //{
        //    complaintItem temp = new complaintItem();
           
        //    key = id.ToString() + "GetComplaints";
        //    try
        //    {
               
        //        logger.LogMethod("jo", "GetComplaints/{id}", "Enter");
        //        context = new socialcopsentity();
        //        if (CachingConfig.CachingEnabled)
        //        {
        //            temp = (complaintItem)WCFCache.Current[key];
        //            if (temp != null)
        //            {
        //                logger.LogMethod("jo", "GetComplaints/{id}", "Cache found");
        //                return temp;
        //            }
        //        }
        //        List<Complaint> complaints = (from c
        //                                      in context.Complaints
        //                                      where c.complaintId == id
        //                                      select c).ToList();
        //        if (complaints.Count == 0)
        //        {
        //            error.Result = false;
        //            error.ErrorMessage = "Invalid request. There is no complaint of the given id.";
        //            logger.LogMethod("jo", "GetComplaints/{id}", "Invalid request. There is no complaint of the given id.");
        //            throw new FaultException<Bug>(error);                  
        //        }

        //        temp = new complaintItem();
        //        foreach (Complaint complaint in complaints)
        //        {
        //            temp.complaintId = complaint.complaintId;
        //            temp.userId = complaint.userId;
        //            temp.title = complaint.title;
        //            temp.details = complaint.details;
        //            temp.numLikes = (int)complaint.numLikes;
        //            temp.numComments = (int)complaint.numComments;
        //            temp.picture = complaint.picture;
        //            temp.complaintDate = (DateTime)complaint.complaintDate;
        //            temp.location = complaint.location;
        //            temp.latitude = (float)complaint.latitude;
        //            temp.longitude = (float)complaint.longitude;
        //            temp.category = complaint.category;
        //            temp.complaintStatus = complaint.complaintStatus;
        //            temp.date = complaint.date;
        //            temp.isAnonymous = complaint.isAnonymous;
        //        }

        //        Cache.Cache.AddToCache(key, temp);
        //        logger.LogMethod("jo", "GetComplaints/{id}", "Exit");
        //        return temp;
        //    }
        //    catch (Exception ex)
        //    {
        //        error.Result = false;
        //        error.ErrorMessage = "unforeseen error occured. Please try again later";
        //        logger.LogMethod("jo", "GetComplaints/{id}", ex.Message);
        //        throw new FaultException<Bug>(error, ex.ToString());
        //    }
        //}
        #endregion

        #region GetComplaintsByCategory/{category}
        public complaintItem[] GetComplaintsByCategory(string category)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByCategory", "Enter");
                key = category + "GetComplaintsByCategory";
                List<complaintItem> list = new List<complaintItem>();
                context = new socialcopsentity();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByCategory", "Cache found");
                        return list.ToArray();
                    }
                }

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.category == category
                                              orderby c.complaintDate descending
                                              select c).Take(50).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint.complaintId = temp.complaintId;
                    complaint.userId = temp.userId;
                    complaint.title = temp.title;
                    complaint.details = temp.details;
                    complaint.numLikes = (int)temp.numLikes;
                    complaint.numComments = (int)temp.numComments;
                    complaint.picture = temp.picture;
                    complaint.complaintDate = (DateTime)temp.complaintDate;
                    complaint.location = temp.location;
                    complaint.latitude = (float)temp.latitude;
                    complaint.longitude = (float)temp.longitude;
                    complaint.category = temp.category;
                    complaint.complaintStatus = temp.complaintStatus;
                    complaint.date = temp.date;
                    complaint.isAnonymous = temp.isAnonymous;

                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByCategory", "Exit");
                return list.ToArray();

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

        #region GetComplaintsByStatus/{complaintStatus}
        public complaintItem[] GetComplaintsByStatus(string complaintStatus)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByStatus", "Enter");
                key = complaintStatus + "GetComplaintsByStatus";
                List<complaintItem> list = new List<complaintItem>();
                context = new socialcopsentity();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByStatus", "Cache Found");
                        return list.ToArray();
                    }
                }

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.complaintStatus == complaintStatus
                                              orderby c.complaintDate descending
                                              select c).Take(50).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint.complaintId = temp.complaintId;
                    complaint.userId = temp.userId;
                    complaint.title = temp.title;
                    complaint.details = temp.details;
                    complaint.numLikes = (int)temp.numLikes;
                    complaint.numComments = (int)temp.numComments;
                    complaint.picture = temp.picture;
                    complaint.complaintDate = (DateTime)temp.complaintDate;
                    complaint.location = temp.location;
                    complaint.latitude = (float)temp.latitude;
                    complaint.longitude = (float)temp.longitude;
                    complaint.category = temp.category;
                    complaint.complaintStatus = temp.complaintStatus;
                    complaint.date = temp.date;
                    complaint.isAnonymous = temp.isAnonymous;

                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByStatus", "Exit");
                return list.ToArray();

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

        #region GetComplaintsByUserId/{userId}
        public complaintItem[] GetComplaintsByUserId(int userId)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByUserId", "Enter");
                key = userId.ToString() + "GetComplaintsByUserId";
                List<complaintItem> list = new List<complaintItem>();
                context = new socialcopsentity();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByUserId", "Cache Found");
                        return list.ToArray();
                    }
                }

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.userId == userId
                                              orderby c.complaintDate descending
                                              select c).Take(50).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint.complaintId = temp.complaintId;
                    complaint.userId = temp.userId;
                    complaint.title = temp.title;
                    complaint.details = temp.details;
                    complaint.numLikes = (int)temp.numLikes;
                    complaint.numComments = (int)temp.numComments;
                    complaint.picture = temp.picture;
                    complaint.complaintDate = (DateTime)temp.complaintDate;
                    complaint.location = temp.location;
                    complaint.latitude = (float)temp.latitude;
                    complaint.longitude = (float)temp.longitude;
                    complaint.category = temp.category;
                    complaint.complaintStatus = temp.complaintStatus;
                    complaint.date = temp.date;
                    complaint.isAnonymous = temp.isAnonymous;

                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByUserId", "Exit");
                return list.ToArray();

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

        #region GetComplaintsByAuthId/{authId}
        public complaintItem[] GetComplaintsByAuthId(int authId)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByAuthId", "Enter");
                List<complaintItem> list = new List<complaintItem>();
                key = authId.ToString() + "GetComplaintsByAuthId";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsbyAuthId", "Cache found");
                        return list.ToArray();
                    }
                }

                //Getting the complaintIds from jurisdiction table where id is authId
                List<Jurisdiction> complaintIds = (from c
                                                   in context.Jurisdictions
                                                   where c.authId == authId
                                                   select c).ToList();
                return null;
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "An error occurred. Sorry!";
                error.Result = true;
                logger.LogMethod("jo", "GetComplaintsByAuthId", ex.Message);
                throw new FaultException<Bug>(error, ex.Message);
            }
        }
        #endregion

    }
}