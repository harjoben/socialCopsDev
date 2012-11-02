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
        string[] asyncResult=new string[2];
        private delegate string[] SaveImageBytes(byte[] Image,string id);
        private SaveImageBytes asynSaveImage;
        private Log log;

        // To lodge Social Complaints
        #region SaveComplaint
        public int SaveComplaint(complaintItem complaint)
        {
            asynSaveImage = new SaveImageBytes(ImageController.SavePicture);
            try
            {
                context = new SocialCopsEntities();
                // Add Log
                logger.LogMethod("ENTER","SaveComplaint","Userid/" + complaint.userId.ToString()+ "/ started SaveComplaint", null);
                // New Complaint
                Complaint temp = new Complaint();
                temp = complaintItem.convertComplaint(complaint);
                //Add a new Complaint
                context.Complaints.Add(temp);
                context.SaveChanges();
                //Image Upload Async
                byte[] image = complaint.ImageByte;
                if (image.Length > 0)
                {
                    IAsyncResult result = asynSaveImage.BeginInvoke(image, temp.complaintId.ToString(), new AsyncCallback(FinishImageUpload), asynSaveImage);
                }
                // Exit Log
                logger.LogMethod("EXIT", "SaveComplaint", "Userid/" + complaint.userId.ToString() + "/ finished SaveComplaint", null);
                return temp.complaintId;
            }
            catch (Exception ex)
            {
                logger.LogMethod("ERROR", "SaveComplaint", "Userid/" + complaint.userId.ToString() + "/ " + ex.ToString(), null);
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later.";
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion

        // Async CallBack when ImageUpload Finishes
        #region FinishImageUpload
        public void FinishImageUpload(IAsyncResult result)
        {
            SaveImageBytes del=(SaveImageBytes)result.AsyncState;
            string[] str = del.EndInvoke(result);
            if(str[0].Length>0)
            {
            int id= int.Parse(str[1]);
            try
            {
                context = new SocialCopsEntities();
                //Entry Log
                logger.LogMethod("DEBUG", "FinishImageUpload", "Complaintid/" + id.ToString() + "/ started FinishImageUpload", null);
                //Find the Complaint
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.complaintId == id
                                              select c).ToList();
                if (complaints.Count > 0)
                {
                    complaints[0].picture = str[0];
                    //Exit Log
                    logger.LogMethod("DEBUG", "FinishImageUpload", "Complaintid/" + id.ToString() + "/ finished FinishImageUpload", null);
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogMethod("ERROR", "FinishImageUpload","Complaintid/" +  id.ToString() + "/ " + ex.ToString(), null);
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later.";
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            }
            }
            
        }
        #endregion

        //GetComplaints
        #region GetComplaints
        public complaintItem[] GetComplaints()
        {
            try
            {
                logger.LogMethod("DEBUG", "GetComplaints", "started GetComplaints", null);
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
                        logger.LogMethod("CACHE", "GetComplaints", "Cache Found - finished GetComplaints", null);
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              orderby c.complaintDate descending
                                              select c).Take(500).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint = complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("DEBUG", "GetComplaints", "finished GetComplaints", null);
                return list.ToArray();

            }
            catch (Exception ex)
            {
                logger.LogMethod("ERROR", "GetComplaints", ex.ToString(), null);
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later.";
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion

        //GetComplaintsByid
        #region GetComplaintsById/{id}
        public complaintItem GetComplaintsById(string id)
        {
            complaintItem temp = new complaintItem();
            key = id + "GetComplaints";
            try
            {
                logger.LogMethod("DEBUG", "GetComplaints/{id}", id+ "/ started GetComplaints/{id}", null);
                context = new SocialCopsEntities();
                if (CachingConfig.CachingEnabled)
                {
                    temp = (complaintItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("CACHE", "GetComplaints/{id}", id + "/ Cache Found - finished GetComplaints/{id}", null);
                        return temp;
                    }
                }
                int cId = Convert.ToInt32(id);
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.complaintId == cId
                                              select c).ToList();
                temp = new complaintItem();
                if (complaints.Count == 0)
                {
                    error.Result = false;
                    error.ErrorMessage = "Invalid request. There is no complaint of the given id.";
                    logger.LogMethod("ERROR", "GetComplaints/{id}", id+  "/ Invalid request. There is no complaint of the given id.", null);
                    throw new FaultException<Bug>(error);
                }

               
                foreach (Complaint complaint in complaints)
                {
                    temp = complaintItem.convertComplaint(complaint);
                }
                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("DEBUG", "GetComplaints/{id}", id + "/ finished GetComplaints/{id}", null);
                return temp;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try again later";
                logger.LogMethod("ERROR", "GetComplaints/{id}", id+ "/ "+ ex.Message);
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion

        //GetComplaintsByCategory
        #region GetComplaintsByCategory/{category}
        public complaintItem[] GetComplaintsByCategory(string category)
        {
            try
            {
                logger.LogMethod("DEBUG", "GetComplaintsByCategory/{category}", category + "/ started GetComplaintsByCategory/{category}", null);
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
                        logger.LogMethod("CACHE", "GetComplaintsByCategory/{category}", category + "/ Cache Found - GetComplaintsByCategory/{category}", null);
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.category == category
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint =complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("DEBUG", "GetComplaintsByCategory/{category}", category + "/ finished GetComplaintsByCategory/{category}", null);
                return list.ToArray();

            }
            catch (Exception ex)
            {
                logger.LogMethod("ERROR", "GetComplaintsByCategory/{category}",category+"/ "+ ex.Message);
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later.";
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion

        //GetComplaintsByStatus
        #region GetComplaintsByStatus/{complaintStatus}
        public complaintItem[] GetComplaintsByStatus(string complaintStatus)
        {
            try
            {
                logger.LogMethod("DEBUG", "GetComplaintsByStatus/{complaintStatus}", complaintStatus + "/ started GetComplaintsByStatus/{complaintStatus}", null);
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
                        logger.LogMethod("CACHE", "GetComplaintsByStatus/{complaintStatus}", complaintStatus + "/ Cache Found - GetComplaintsByStatus/{complaintStatus}", null);
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.complaintStatus == complaintStatus
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint=complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("DEBUG", "GetComplaintsByStatus/{complaintStatus}", complaintStatus + "/ finished GetComplaintsByStatus/{complaintStatus}", null);
                return list.ToArray();

            }
            catch (Exception ex)
            {
                logger.LogMethod("ERROR", "GetComplaintsByStatus/{complaintStatus}", complaintStatus + "/ " + ex.Message);
                error.Result = false;
                error.ErrorMessage = "unforeseen error occured. Please try later."; 
                error.ErrorDetails = ex.ToString();
                throw new FaultException<Bug>(error, ex.ToString());
            }
        }
        #endregion
        
        //GetComplaintsByCity
        #region GetComplaintsByCity/{city}
        public complaintItem[] GetComplaintsByCity(string city)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByCity", "Enter");
                key = city + "GetComplaintsByCity";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByCity", "Cache Found");
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.city == city
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint =complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByCity", "Exit");
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

        #region GetComplaintsByState/{state}
        public complaintItem[] GetComplaintsByState(string state)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByState", "Enter");
                key = state + "GetComplaintsByState";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByState", "Cache Found");
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.state == state
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint = complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByState", "Exit");
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

        #region GetComplaintsByCountry/{country}
        public complaintItem[] GetComplaintsByCountry(string country)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByCountry", "Enter");
                key = country + "GetComplaintsByCountry";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByCountry", "Cache Found");
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.country == country
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                    complaint = complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByCountry", "Exit");
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

        #region GetComplaintsByPin/{pin}
        public complaintItem[] GetComplaintsByPin(string pin)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByPin", "Enter");
                key = pin + "GetComplaintsByPin";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

                //Checking if the complaints exist in cache
                //retrieveing complaints if they do.
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsByPin", "Cache Found");
                        return list.ToArray();
                    }
                }
                list = new List<complaintItem>();
                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.pincode == pin
                                              orderby c.complaintDate descending
                                              select c).ToList();

                foreach (Complaint temp in complaints)
                {
                    complaintItem complaint = new complaintItem();
                   complaint= complaintItem.convertComplaint(temp);
                    list.Add(complaint);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetComplaintsByPin", "Exit");
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

        //GetComments
        #region GetComments/{complaintId}
        public commentItem[] GetComments(String complaintId)
        {
            try
            {
                logger.LogMethod("DEBUG", "GetComments/{complaintId}", complaintId + "/ started GetComments/{complaintId}", null);
                List<commentItem> list = new List<commentItem>();
                key = complaintId + "GetComments";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<commentItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("CACHE", "GetComments/{complaintId}", complaintId + "/ Cache Found - finished GetComments/{complaintId}", null);
                        return list.ToArray();
                    }
                }
                list = new List<commentItem>();
                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(complaintId);
                List<Comment> comments = (from c
                                          in context.Comments
                                          where c.complaintId == cid
                                          orderby c.date descending
                                          select c).ToList();

                foreach (Comment comment in comments)
                {
                    commentItem temp = new commentItem();
                    temp.comment = comment.comment1;
                    temp.complaintId = comment.complaintId;
                    temp.userId = comment.userId;
                    temp.date = comment.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("DEBUG", "GetComments/{complaintId}", complaintId + "/ finished GetComments/{complaintId}", null);
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("ERROR", "GetComments/{complaintId}", complaintId + ex.ToString(), null);
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }

        }
        #endregion

        //GetLikes/{complaintId}
        #region GetLikes/{complaintId}
        public likeItem[] GetLikes(String complaintId)
        {
            try
            {
                logger.LogMethod("DEBUG", "GetLikes/{complaintId}", complaintId + "/ started GetLikes/{complaintId}", null);
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
                list = new List<likeItem>();
                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(complaintId);
                List<Like> likes = (from c
                                          in context.Likes
                                    where c.complaintId == cid
                                    orderby c.date descending
                                    select c).ToList();

                foreach (Like like in likes)
                {
                    likeItem temp = new likeItem();
                    temp.complaintId = like.complaintId;
                    temp.date = like.date;
                    temp.userId = like.userId;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("DEBUG", "GetLikes/{complaintId}", complaintId + "/ finished GetLikes/{complaintId}", null);
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("ERROR", "GetLikes/{complaintId}", complaintId + ex.ToString(), null);
               
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }

        }
        #endregion

        #region SaveComment
        public bool SaveComment(commentItem comment)
        {
            try
            {
                logger.LogMethod("DEBUG", "SaveComment/{complaintId}", comment.complaintId + "/ started SaveComment/{complaintId}", null);
                context = new SocialCopsEntities();
                Comment temp = new Comment();
                temp.comment1 = comment.comment;
                temp.complaintId = comment.complaintId;
                temp.userId = comment.userId;
                temp.date = System.DateTime.Now;
                context.Comments.Add(temp);
                context.SaveChanges();
                logger.LogMethod("DEBUG", "SaveComment/{complaintId}", comment.complaintId + "/ finished SaveComment/{complaintId}", null);
                return true;

            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("ERROR", "SaveComment/{complaintId}", comment.complaintId +ex.ToString(), null);
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region SaveLike
        public bool SaveLike(likeItem like)
        {
            try
            {
                logger.LogMethod("DEBUG", "SaveLike/{complaintId}", like.complaintId + "/ started SaveLike/{complaintId}", null);
                context = new SocialCopsEntities();
                Like temp = new Like();
                temp.complaintId = like.complaintId;
                temp.userId = like.userId;
                temp.date = like.date;
                context.Likes.Add(temp);
                context.SaveChanges();
                logger.LogMethod("DEBUG", "SaveLike/{complaintId}", like.complaintId + "/ finished SaveLike/{complaintId}", null);
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                logger.LogMethod("ERROR", "SaveLike/{complaintId}", like.complaintId + ex.ToString(), null);
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetSpam/{complaintId}
        public spamItem[] GetSpam(string complaintId)
        {
            try
            {
                logger.LogMethod("DEBUG", "GetSpam/{complaintId}", complaintId + "/ started GetSpam/{complaintId}", null);

                List<spamItem> list = new List<spamItem>();
                key = complaintId + "GetSpam";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<spamItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("CACHE", "GetSpam/{complaintId}", complaintId + "/ Cache Found - GetSpam/{complaintId}", null);
                        return list.ToArray();
                    }
                }
                list = new List<spamItem>();
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
                logger.LogMethod("DEBUG", "GetSpam/{complaintId}", complaintId + "/ finished GetSpam/{complaintId}", null);
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.ToString();
                error.Result = false;
                logger.LogMethod("ERROR", "GetSpam/{complaintId}", complaintId + ex.ToString(), null);
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region SaveSpam
        public bool SaveSpam(spamItem spam)
        {
            try
            {
                logger.LogMethod("DEBUG", "SaveSpam/{complaintId}", spam.complaintId + "/ started SaveSpam/{complaintId}", null);
                context = new SocialCopsEntities();
                Spam temp = new Spam();
                temp.date = spam.date;
                temp.complaintId = spam.complaintId;
                temp.userId = spam.userId;
                context.Spams.Add(temp);
                context.SaveChanges();
                logger.LogMethod("DEBUG", "SaveSpam/{complaintId}", spam.complaintId + "/ finished SaveSpam/{complaintId}", null);
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.ToString();
                logger.LogMethod("ERROR", "SaveSpam/{complaintId}", spam.complaintId + ex.ToString(), null);
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion
    }
}