using CoreService.Cache;
using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace CoreService.Controllers
{
    public class UserController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        SocialCopsEntities context;
        string key;

        #region SaveUser
        public int SaveUser(userItem user) 
        {
            try
            {
                logger.LogMethod("jo", "SaveUser", "Enter", null);
                context = new SocialCopsEntities();
                User temp = new User();

                List<User> users = (from u
                                    in context.Users
                                    where u.email == user.email
                                    select u).ToList();

                if (users.Count > 0)
                {
                    logger.LogMethod("jo", "SaveUser", "Invalid Sign-up. Email address already exists.");
                    error.ErrorDetails = "Abc";

                    error.Result = false;
                    error.ErrorMessage = "Email address already exists. Cannot register again.";
             
                    throw new WebFaultException<Bug>(error,System.Net.HttpStatusCode.NotAcceptable);
                }

                temp.userName = user.userName;
                temp.email = user.email;
                temp.phone = user.phone.ToString();
                temp.phoneURI = user.phoneUri;
                temp.profilePic = user.profilePic;
                temp.userAddress = user.userAddress;
                temp.points = 0;
                temp.userRank = "Cadet";
                temp.webURI = user.webUri;
                temp.pwd = user.pwd;
                temp.city = user.city;
                temp.state = user.state;
                temp.country = user.country;
                temp.pincode = user.pincode;
                temp.date = System.DateTime.Now;
                context.Users.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveUser", "Exit", null);
                //userItem u = (userItem)context.Entry(temp);
                return temp.userId;
            }
            catch (WebFaultException<Bug> ex)
            {

                throw;
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

        #region SaveProfilePic
        public bool SaveProfilePic(int id, string url)
        {
            try
            {
                logger.LogMethod("jo", "SaveProfilePic", "Enter", null);
                context = new SocialCopsEntities();


                User temp = new User();
                temp = (User)(from c
                                    in context.Users
                                   where c.userId == id
                                   select c);

                if (temp == null)
                {
                    error.Result = false;
                    error.ErrorMessage = "Invalid user Id.";
                    error.ErrorDetails = "The user does not exist.";
                    throw new FaultException<Bug>(error);
                }

                temp.profilePic = url;
                context.SaveChanges();

                logger.LogMethod("jo", "SaveProfilePic", "Exit", null);
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

        #region thirdPartyLogin
        public bool thirdPartyLogin(userItem user)
        {
            try
            {
                logger.LogMethod("jo", "thirdpartylogin", "Enter");
                context = new SocialCopsEntities();
                List<User> users = (from u
                                    in context.Users
                                    where u.email == user.email
                                    select u).ToList();
                if (users.Count == 0)
                {
                    logger.LogMethod("jo", "thirdpartylogin", "New user");
                    User temp = new User();
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = user.phone.ToString();
                    temp.phoneURI = user.phoneUri;
                    temp.profilePic = user.profilePic;
                    temp.userAddress = user.userAddress;
                    temp.points = 0;
                    temp.userRank = "Cadet";
                    temp.webURI = user.webUri;
                    temp.pwd = user.pwd;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    temp.date = System.DateTime.Now;
                    context.Users.Add(temp);
                    context.SaveChanges();
                }

                logger.LogMethod("jo", "thirdpartylogin", "Exit");
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

        #region GetComplaintsByUserId/{userId}
        public complaintItem[] GetComplaintsByUserId(string userId)
        {
            try
            {
                logger.LogMethod("jo", "GetComplaintsByUserId", "Enter");
                key = userId.ToString() + "GetComplaintsByUserId";
                List<complaintItem> list = new List<complaintItem>();
                context = new SocialCopsEntities();

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

                int uid = Convert.ToInt32(userId);

                List<Complaint> complaints = (from c
                                              in context.Complaints
                                              where c.userId == uid
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
                    complaint.city = temp.city;
                    complaint.state = temp.state;
                    complaint.country = temp.country;
                    complaint.pincode = temp.pincode;
                    complaint.thumbImage1 = temp.thumbImage1;
                    complaint.thumbImage2 = temp.thumbImage2;
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

        #region GetUsers
        public userItem[] GetUsers()
        {
            try
            {
                logger.LogMethod("jo", "GetUsers", "Enter");
                List<userItem> list = new List<userItem>();
                key = "GetUsers";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsers", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<User> users = (from u
                                    in context.Users
                                    orderby u.date descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = 0;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsers", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetUsers", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUserById/{id}
        public userItem GetUserById(string id)
        {
            try
            {
                logger.LogMethod("jo", "GetUserById", "Enter");
                //List<userItem> list = new List<userItem>();
                userItem temp = new userItem();
                key = id + "GetUserById";
                if (CachingConfig.CachingEnabled)
                {
                    temp = (userItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("jo", "GetUseById", "Cache Found");
                        return temp;
                    }
                }

                context = new SocialCopsEntities();
                int uid = Convert.ToInt32(id);
                User user = (User)(from u
                                    in context.Users
                                    where u.userId == uid
                                    select u);

                if (user == null)
                {
                    error.Result = false;
                    error.ErrorMessage = "User doesn't exist";
                    logger.LogMethod("jo", "GetUserById", "User doesn't exist");
                    throw new FaultException<Bug>(error);
                }

                temp.userId = user.userId;
                temp.userName = user.userName;
                temp.email = user.email;
                temp.phone = Convert.ToInt32(user.phone);
                temp.userAddress = user.userAddress;
                temp.points = (float)user.points;
                temp.userRank = user.userRank;
                temp.profilePic = user.profilePic;
                temp.webUri = user.webURI;
                temp.phoneUri = user.phoneURI;
                temp.numComplaints = (int)user.numComplaints;
                temp.date = user.date;
                temp.city = user.city;
                temp.state = user.state;
                temp.country = user.country;
                temp.pincode = user.pincode;
                
                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("jo", "GetUserById", "Exit");
                return temp;
            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetUserById", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByName/{name}
        public userItem[] GetUsersByName(string name)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByName", "Enter");
                List<userItem> list = new List<userItem>();
                key = name + "GetUsersByName";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByName", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<User> users = (from u
                                    in context.Users
                                    where u.userName == name
                                    orderby u.date descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByName", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByName", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUserByEmail/{email}
        public userItem GetUserByEmail(string email)
        {
            try
            {
                logger.LogMethod("jo", "GetUserByEmail", "Enter");
                //List<userItem> list = new List<userItem>();
                userItem temp = new userItem();
                key = email + "GetUserByEmail";
                if (CachingConfig.CachingEnabled)
                {
                    temp = (userItem)WCFCache.Current[key];
                    if (temp != null)
                    {
                        logger.LogMethod("jo", "GetUseById", "Cache Found");
                        return temp;
                    }
                }

                context = new SocialCopsEntities();
                
                User user = (User)(from u
                                    in context.Users
                                   where u.email == email
                                   select u);

                if (user == null)
                {
                    error.Result = false;
                    error.ErrorMessage = "User doesn't exist";
                    logger.LogMethod("jo", "GetUserByEmail", "User doesn't exist");
                    throw new FaultException<Bug>(error);
                }

                temp.userId = user.userId;
                temp.userName = user.userName;
                temp.email = user.email;
                temp.phone = Convert.ToInt32(user.phone);
                temp.userAddress = user.userAddress;
                temp.points = (float)user.points;
                temp.userRank = user.userRank;
                temp.profilePic = user.profilePic;
                temp.webUri = user.webURI;
                temp.phoneUri = user.phoneURI;
                temp.numComplaints = (int)user.numComplaints;
                temp.date = user.date;
                temp.city = user.city;
                temp.state = user.state;
                temp.country = user.country;
                temp.pincode = user.pincode;

                Cache.Cache.AddToCache(key, temp);
                logger.LogMethod("jo", "GetUserByEmail", "Exit");
                return temp;
            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetUserByEmail", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByPoints/{points}
        public userItem[] GetUsersByPoints(string points)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByPoints", "Enter");
                List<userItem> list = new List<userItem>();
                key = points + "GetUsersByPoints";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByPoints", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                double pts = Convert.ToDouble(points);
                List<User> users = (from u
                                    in context.Users
                                    where u.points >= pts
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByPoints", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByPoints", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByRank/{rank}
        public userItem[] GetUsersByRank(string rank)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByRank", "Enter");
                List<userItem> list = new List<userItem>();
                key = rank + "GetUsersByRank";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByRank", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                
                List<User> users = (from u
                                    in context.Users
                                    where u.userRank == rank
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByRank", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByRank", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByCity/{city}
        public userItem[] GetUsersByCity(string city)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByCity", "Enter");
                List<userItem> list = new List<userItem>();
                key = city + "GetUsersByCity";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByCity", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();

                List<User> users = (from u
                                    in context.Users
                                    where u.city == city
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByCity", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByCity", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByState/{state}
        public userItem[] GetUsersByState(string state)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByState", "Enter");
                List<userItem> list = new List<userItem>();
                key = state + "GetUsersByState";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByState", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();

                List<User> users = (from u
                                    in context.Users
                                    where u.state == state
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByState", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByState", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByCountry/{country}
        public userItem[] GetUsersByCountry(string country)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByCountry", "Enter");
                List<userItem> list = new List<userItem>();
                key = country + "GetUsersByCountry";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByCountry", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();

                List<User> users = (from u
                                    in context.Users
                                    where u.country == country
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByCountry", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByCountry", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByPin/{pin}
        public userItem[] GetUsersByPin(string pin)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByPin", "Enter");
                List<userItem> list = new List<userItem>();
                key = pin + "GetUsersByPin";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByPin", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();

                List<User> users = (from u
                                    in context.Users
                                    where u.pincode == pin
                                    orderby u.points descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByPin", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByPin", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetUsersByComplaints/{num}
        public userItem[] GetUsersByComplaints(string num)
        {
            try
            {
                logger.LogMethod("jo", "GetUsersByComplaints", "Enter");
                List<userItem> list = new List<userItem>();
                key = num + "GetUsersByComplaints";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetUsersByComplaints", "Cache Found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                double pts = Convert.ToDouble(num);
                List<User> users = (from u
                                    in context.Users
                                    where u.numComplaints >= pts
                                    orderby u.numComplaints descending
                                    select u).ToList();

                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetUsersByComplaints", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetUsersByComplaints", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetCommentsByUserId/{userId}
        public commentItem[] GetCommentsByUserId(String userId)
        {
            try
            {
                logger.LogMethod("jo", "GetCommentsByUserId", "Enter");
                List<commentItem> list = new List<commentItem>();
                key = userId + "GetCommentsByUserId";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<commentItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetCommentsByUserId", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(userId);
                List<Comment> comments = (from c
                                          in context.Comments
                                          where c.userId == cid
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
                logger.LogMethod("jo", "GetCommentsByUserId", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("jo", "GetCommentsByUserId", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }

        }
        #endregion

        #region GetLikesByUserId/{userId}
        public likeItem[] GetLikesByUserId(String userId)
        {
            try
            {
                logger.LogMethod("jo", "GetLikes", "Enter");
                List<likeItem> list = new List<likeItem>();
                key = userId + "GetLikesByUserId";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<likeItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetLikesByUserId", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int cid = Convert.ToInt32(userId);
                List<Like> likes = (from c
                                          in context.Likes
                                    where c.userId == cid
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
                logger.LogMethod("jo", "GetLikesByUserId", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry";
                logger.LogMethod("jo", "GetLikesByUserId", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }

        }
        #endregion

        #region GetSpamByUserId/{userId}
        public spamItem[] GetSpamByUserId(string userId)
        {
            try
            {
                logger.LogMethod("jo", "GetSpamByUserId", "Enter");

                List<spamItem> list = new List<spamItem>();
                key = userId + "GetSpamByUserId";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<spamItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetSpamByUserId", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                //Retrieving records from the database
                int cid = Convert.ToInt32(userId);
                List<Spam> spams = (from s
                                    in context.Spams
                                    where s.userId == cid
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
                logger.LogMethod("jo", "GetSpamByUserId", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.ToString();
                error.Result = false;
                logger.LogMethod("jo", "GetSpamByUserId", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region SaveSubscription
        public bool SaveSubscription(subscriptionItem subscription)
        {
            try
            {
                logger.LogMethod("jo", "SaveSubscription", "Enter");
                context = new SocialCopsEntities();
                Subscription temp = new Subscription();
                temp.subscribedToId = subscription.subscribedToId;
                temp.subscriberId = subscription.subscriberId;
                temp.date = subscription.date;
                context.Subscriptions.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveSubscription", "Exit");
                return true;
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorDetails = ex.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                logger.LogMethod("jo", "SaveSubscription", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetSubscribers/{id}
        public userItem[] GetSubscribers(string id)
        {
            try
            {
                logger.LogMethod("jo", "GetSubscribers", "Enter");
                List<userItem> list = new List<userItem>();
                key = id + "GetSubscribers";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetSubscribers", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int sid = Convert.ToInt32(id);
                var innerquery = (from s
                                  in context.Subscriptions
                                  where s.subscribedToId == sid
                                  select s.subscriberId);
                List<User> users = (from u
                                    in context.Users
                                    where innerquery.Contains(u.userId)
                                    select u).ToList();
                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetSubscribers", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                logger.LogMethod("jo", "GetSubscribers", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetSubscribedTo/{id}
        public userItem[] GetSubscribedTo(string id)
        {
            try
            {
                logger.LogMethod("jo", "GetSubscribedTo", "Enter");
                List<userItem> list = new List<userItem>();
                key = id + "GetSubscribedTo";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<userItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetSubscribedTo", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int sid = Convert.ToInt32(id);
                var innerquery = (from s
                                  in context.Subscriptions
                                  where s.subscriberId == sid
                                  select s.subscribedToId);
                List<User> users = (from u
                                    in context.Users
                                    where innerquery.Contains(u.userId)
                                    select u).ToList();
                foreach (User user in users)
                {
                    userItem temp = new userItem();
                    temp.userId = user.userId;
                    temp.userName = user.userName;
                    temp.email = user.email;
                    temp.phone = Convert.ToInt32(user.phone);
                    temp.userAddress = user.userAddress;
                    temp.points = (float)user.points;
                    temp.userRank = user.userRank;
                    temp.profilePic = user.profilePic;
                    temp.webUri = user.webURI;
                    temp.phoneUri = user.phoneURI;
                    temp.numComplaints = (int)user.numComplaints;
                    temp.date = user.date;
                    temp.city = user.city;
                    temp.state = user.state;
                    temp.country = user.country;
                    temp.pincode = user.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetSubscribedTo", "Exit");
                return list.ToArray();
            }
            catch (Exception ex)
            {
                error.Result = false;
                error.ErrorMessage = "Something happened. Sorry.";
                error.ErrorDetails = ex.Message.ToString();
                logger.LogMethod("jo", "GetSubscribedTo", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion
    }
}