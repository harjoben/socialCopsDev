using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Configuration;
using CoreService.Cache;
using System.IO;

namespace CoreService.Controllers
{
    public class ComplaintController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        SocialCopsEntities context;
        string key;

        #region SaveComplaint
        public int SaveComplaint(complaintItem complaint)
        {
            try
            {
                logger.LogMethod("jo", "SaveComplaint", "Enter", null);
                context = new SocialCopsEntities();

                Complaint temp = new Complaint();
                //temp.complaintId = complaint.complaintId;
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

                byte[] image = complaint.ImageByte;
               // ImageController.SavePicture(image, "Test");
                MemoryStream ms=new MemoryStream(image);
                ImageController.UploadImageinBlob(ms,"test");

                context.Complaints.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveComplaint", "Exit", null);
                return temp.complaintId;
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
                context = new SocialCopsEntities();

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
                                              select c).ToList();

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

        #region GetComplaintsById/{id}
        public complaintItem GetComplaintsById(string id)
        {
            complaintItem temp = new complaintItem();

            key = id + "GetComplaints";
            try
            {

                logger.LogMethod("jo", "GetComplaints/{id}", "Enter");
                context = new SocialCopsEntities();
                if (CachingConfig.CachingEnabled)
                {
                    temp = (complaintItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("jo", "GetComplaints/{id}", "Cache found");
                        return temp;
                    }
                }
                int cId = Convert.ToInt32(id);

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.complaintId == cId
                                              select c).ToList();
                if (complaints.Count == 0)
                {
                    error.Result = false;
                    error.ErrorMessage = "Invalid request. There is no complaint of the given id.";
                    logger.LogMethod("jo", "GetComplaints/{id}", "Invalid request. There is no complaint of the given id.");
                    throw new FaultException<Bug>(error);
                }

                temp = new complaintItem();
                foreach (Complaint complaint in complaints)
                {
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
                }

                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("jo", "GetComplaints/{id}", "Exit");
                return temp;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try again later";
                logger.LogMethod("jo", "GetComplaints/{id}", ex.Message);
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion

        #region GetComplaintsByCategory/{category}
        public complaintItem[] GetComplaintsByCategory(string category)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByCategory", "Enter");
                key = category + "GetComplaintsByCategory";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

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
                                              select c).ToList();

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
                context = new SocialCopsEntities();

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
                                              select c).ToList();

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

        #region GetComments/{complaintId}
        public commentItem[] GetComments(String complaintId)
        {
            try
            {
                logger.LogMethod("jo", "GetComments", "Enter");
                List<commentItem> list = new List<commentItem>();
                key = complaintId + "GetComments";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<commentItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComments", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(complaintId);
                List<Comment> comments = (from c
                                          in context.Comments
                                          where c.complaintId == cid
                                          orderby c.date descending
                                          select c).ToList();

                foreach(Comment comment in comments)
                {
                    commentItem temp = new commentItem();
                    temp.comment = comment.comment1;
                    temp.complaintId = comment.complaintId;
                    temp.userId = comment.userId;
                    temp.date = comment.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComments", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("jo", "GetComments", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
            
        }
        #endregion

        #region GetLikes/{complaintId}
        public likeItem[] GetLikes(String complaintId)
        {
            try
            {
                logger.LogMethod("jo", "GetLikes", "Enter");
                List<likeItem> list = new List<likeItem>();
                key = complaintId + "GetLikes";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<likeItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetLikes", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(complaintId);
                List<Like> likes = (from c
                                          in context.Likes
                                          where c.complaintId == cid
                                          orderby c.date descending
                                          select c).ToList();

                foreach(Like like in likes)
                {
                    likeItem temp = new likeItem();
                    temp.complaintId = like.complaintId;
                    temp.date = like.date;
                    temp.userId = like.userId;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetLikes", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("jo", "GetLikes", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
            
        }
        #endregion

        #region SaveComment
        public bool SaveComment(commentItem comment)
        {
            try
            {
                logger.LogMethod("jo", "SaveComment", "Enter");
                context = new SocialCopsEntities();
                Comment temp = new Comment();
                temp.comment1 = comment.comment;
                temp.complaintId = comment.complaintId;
                temp.userId = comment.userId;
                temp.date = System.DateTime.Now;
                context.Comments.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveComment", "Exit");
                return true;

            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "SaveComment", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region SaveLike
        public bool SaveLike(likeItem like)
        {
            try
            {
                logger.LogMethod("jo", "SaveLike", "Enter");
                context = new SocialCopsEntities();
                Like temp = new Like();
                temp.complaintId = like.complaintId;
                temp.userId = like.userId;
                temp.date = like.date;
                context.Likes.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveLike", "Exit");
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                logger.LogMethod("jo", "SaveLike", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetSpam/{complaintId}
        public spamItem[] GetSpam(string complaintId)
        {
            try
            {
                logger.LogMethod("jo", "GetSpam", "Enter");

                List<spamItem> list = new List<spamItem>();
                key = complaintId + "GetSpam";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<spamItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetSpam", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                //Retrieving records from the database
                int cid = Convert.ToInt32(complaintId);
                List<Spam> spams = (from s
                                    in context.Spams
                                    where s.complaintId == cid
                                    orderby s.date descending
                                    select s).ToList();

                foreach (Spam spam in spams)
                {
                    spamItem temp = new spamItem();
                    temp.complaintId = spam.complaintId;
                    temp.userId = spam.userId;
                    temp.date = spam.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetSpam", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetSpam", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region SaveSpam
        public bool SaveSpam(spamItem spam)
        {
            try
            {
                logger.LogMethod("jo", "SaveSpam", "Enter");
                context = new SocialCopsEntities();
                Spam temp = new Spam();
                temp.date = spam.date;
                temp.complaintId = spam.complaintId;
                temp.userId = spam.userId;
                context.Spams.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveSpam", "Exit");
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.ToString();
                logger.LogMethod("jo", "SaveSpam", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion
    }
}