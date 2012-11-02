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
                list = new List<complaintItem>();
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
                    complaint.thumbImage1 = temp.thumbImage1;
                    complaint.thumbImage2 = temp.thumbImage2;
                    complaint.city = temp.city;
                    complaint.country = temp.country;
                    complaint.state = temp.state;
                    complaint.pincode = temp.pincode;

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
                list = new List<authorityItem>();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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
                temp = new authorityItem();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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
                list = new List<authorityItem>();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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

        #region GetAuthsByCity/{city}
        public authorityItem[] GetAuthsByCity(string city)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByCity", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = city + "GetAuthsByCity";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByCity", "Cache found");
                        return list.ToArray();
                    }
                }
                list = new List<authorityItem>();
                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.city == city
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByCity", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByCity", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsByState/{state}
        public authorityItem[] GetAuthsByState(string state)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByState", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = state + "GetAuthsByState";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByState", "Cache found");
                        return list.ToArray();
                    }
                }
                list = new List<authorityItem>();
                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.state == state
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByState", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByState", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsByCountry/{country}
        public authorityItem[] GetAuthsByCountry(string country)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByCountry", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = country + "GetAuthsByCountry";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByCountry", "Cache found");
                        return list.ToArray();
                    }
                }
                list = new List<authorityItem>();
                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.country == country
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByCountry", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByCountry", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetAuthsByPin/{pin}
        public authorityItem[] GetAuthsByPin(string pin)
        {
            try
            {
                logger.LogMethod("jo", "GetAuthsByPin", "Enter");
                List<authorityItem> list = new List<authorityItem>();
                key = pin + "GetAuthsByPin";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<authorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetAuthsByPin", "Cache found");
                        return list.ToArray();
                    }
                }
                list = new List<authorityItem>();
                context = new SocialCopsEntities();
                List<Authority> auths = (from a
                                         in context.Authorities
                                         where a.pincode == pin
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetAuthsByPin", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetAuthsByPin", ex.Message.ToString());
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
                temp = new authorityItem();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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
                list = new List<authorityItem>();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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
                list = new List<authorityItem>();
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
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.pincode = auth.pincode;
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