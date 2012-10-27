using CoreService.Cache;
using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace CoreService.Controllers
{
    public class AuthorityController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        SocialCopsEntities context;
        
        string key;

        #region GetComplaintsByAuthId/{authId}
        public complaintItem[] GetComplaintsByAuthId(string authId)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByAuthId", "Enter");
                List<complaintItem> list = new List<complaintItem>();
                key = authId + "GetComplaintsByAuthId";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<complaintItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetComplaintsbyAuthId", "Cache found");
                        return list.ToArray();
                    }
                }

                int aid = Convert.ToInt32(authId);
                context = new SocialCopsEntities();
                //Getting the complaintIds from jurisdiction table where id is authId
                var complaintIds = (from c
                                                   in context.Jurisdictions
                                    where c.authId == aid
                                    select c.complaintId);

                if (complaintIds == null)
                {
                    logger.LogMethod("jo", "GetComplaintsByAuthId", "No Complaints exist under the given authority");
                    return null;
                }

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where complaintIds.Contains(c.complaintId)
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
                logger.LogMethod("jo", "GetComplaintsByAuthId", "Exit");
                return list.ToArray();
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

        #region SaveAuth
        public bool SaveAuth(authorityItem auth)
        {
            try
            {
                logger.LogMethod("jo", "SaveAuth", "Enter");
                return false;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                error.ErrorDetails = ex.Message.ToString();
                logger.LogMethod("jo", "SaveAuth", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuths
        public authorityItem[] GetAuths()
        {
            try
            {
                logger.LogMethod("jo", "GetAuths", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = "GetAuths";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuths", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         orderby a.date descending
                                         select a).ToList();

                foreach (Authority auth in auths)
                {
                    authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuths", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuths", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthById/{id}
        public authorityItem GetAuthById(string id)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthById", "Enter");
                //List<authorityItem> list = new List<authorityItem>();
                authorityItem temp = new authorityItem();
                key = id + "GetAuthById";
                if (CachingConfig.CachingEnabled)
                {
                    temp = (authorityItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("jo", "GetAuthById", "Cache found");
                        return temp;
                    }
                }

                context = new SocialCopsEntities();
                int aid = Convert.ToInt32(id);
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.authId == aid
                                         orderby a.date descending
                                         select a).ToList();

                if (auths.Count == 0)
                {
                    error.ErrorMessage = "The auth doesn't exist.";
                    error.ErrorDetails = "The auth doesn't exist.";
                    error.Result = false;
                    logger.LogMethod("jo", "GetAuthById", "The auth doesn't exist.");
                    throw new FaultException<Bug>(error);
                }

                foreach (Authority auth in auths)
                {
                    //authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    //list.Add(temp);
                }
                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("jo", "GetAuthById", "Exit");
                return temp;

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthById", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsByName/{name}
        public authorityItem[] GetAuthsByName(string name)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByName", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = name + "GetAuthsByName";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByName", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.authName == name
                                         orderby a.date descending
                                         select a).ToList();

                foreach (Authority auth in auths)
                {
                    authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByName", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByName", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthByEmail/{email}
        public authorityItem GetAuthByEmail(string email)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthByEmail", "Enter");
                //List<authorityItem> list = new List<authorityItem>();
                authorityItem temp = new authorityItem();
                key = email + "GetAuthByEmail";
                if (CachingConfig.CachingEnabled)
                {
                    temp = (authorityItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("jo", "GetAuthByEmail", "Cache found");
                        return temp;
                    }
                }

                context = new SocialCopsEntities();
                //int aid = Convert.ToInt32(id);
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.email == email
                                         orderby a.date descending
                                         select a).ToList();

                if (auths.Count == 0)
                {
                    error.ErrorMessage = "The auth doesn't exist.";
                    error.ErrorDetails = "The auth doesn't exist.";
                    error.Result = false;
                    logger.LogMethod("jo", "GetAuthByEmail", "The auth doesn't exist.");
                    throw new FaultException<Bug>(error);
                }

                foreach (Authority auth in auths)
                {
                    //authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    //list.Add(temp);
                }
                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("jo", "GetAuthByEmail", "Exit");
                return temp;

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthByEmail", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsByPending/{num}
        public authorityItem[] GetAuthsByPending(string num)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByPending", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = num + "GetAuthsByPending";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByPending", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int pts = Convert.ToInt32(num);
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.numPending >= pts
                                         orderby a.numPending descending
                                         select a).ToList();

                foreach (Authority auth in auths)
                {
                    authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByPending", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByPending", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsBySolved/{num}
        public authorityItem[] GetAuthsBySolved(string num)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsBySolved", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = num + "GetAuthsBySolved";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsBySolved", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int pts = Convert.ToInt32(num);
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.numResolved >= pts
                                         orderby a.numResolved descending
                                         select a).ToList();

                foreach (Authority auth in auths)
                {
                    authorityItem temp = new authorityItem();
                    temp.authId = auth.authId;
                    temp.authName = auth.authName;
                    temp.authAddress = auth.authAddress;
                    temp.email = auth.email;
                    temp.phone = Convert.ToInt32(auth.phone);
                    temp.numPending = (int)auth.numPending;
                    temp.numResolved = (int)auth.numResolved;
                    temp.latitude = temp.latitude;
                    temp.longitude = temp.longitude;
                    temp.website = auth.website;
                    temp.profilePic = auth.profilePic;
                    temp.flag = (int)auth.flag;
                    temp.date = auth.date;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsBySolved", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsBySolved", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion
    }
}