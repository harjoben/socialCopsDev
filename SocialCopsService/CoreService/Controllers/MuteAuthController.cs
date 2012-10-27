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
    public class MuteAuthController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        SocialCopsEntities context;
        string key;

        #region SaveMuteAuth
        public int SaveMuteAuth(muteAuthorityItem muteAuth)
        {
            try
            {
                logger.LogMethod("jo", "SaveMuteAuth", "Enter", null);
                context = new SocialCopsEntities();
                MuteAuthority temp = new MuteAuthority();

                List<MuteAuthority> muteAuths = (from m
                                    in context.MuteAuthorities
                                    where m.email == muteAuth.email
                                    select m).ToList();

                if (muteAuths.Count > 0)
                {
                    logger.LogMethod("jo", "SaveMuteAuth", "Invalid Sign-up. Email address already exists.");
                    error.ErrorDetails = "Email address already exists.";
                    error.Result = false;
                    error.ErrorMessage = "Email address already exists. Cannot register again.";
                    throw new WebFaultException<Bug>(error, System.Net.HttpStatusCode.NotAcceptable);
                }

                temp.muteAuthName = muteAuth.muteAuthName;
                temp.muteAuthAddress = muteAuth.muteAuthAddress;
                temp.city = muteAuth.city;
                temp.state = muteAuth.state;
                temp.country = muteAuth.country;
                temp.email = muteAuth.email;
                temp.phone = muteAuth.phone;
                temp.latitude = muteAuth.latitude;
                temp.longitude = muteAuth.longitude;
                temp.website = muteAuth.website;
                temp.pic = muteAuth.pic;
                temp.date = System.DateTime.Now;
                temp.flag = muteAuth.flag;
                temp.pincode = muteAuth.pincode;

                context.MuteAuthorities.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveMuteAuth", "Exit", null);
                //userItem u = (userItem)context.Entry(temp);
                return temp.muteAuthId;
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

        #region GetMuteAuths
        public muteAuthorityItem[] GetMuteAuths()
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuths", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = "GetMuteAuths";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuths", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                         in context.MuteAuthorities
                                         orderby a.date descending
                                         select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuths", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuths", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsById\{id}
        public muteAuthorityItem[] GetMuteAuthsById(string id)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsById", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = id.ToString() + "GetMuteAuthsById";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsById", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                int mid = Convert.ToInt32(id);
                List<MuteAuthority> muteAuths = (from a
                                                 in context.MuteAuthorities
                                                 where a.muteAuthId == mid
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsById", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsById", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByName/{name}
        public muteAuthorityItem[] GetMuteAuthsByName(string name)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByName", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = name + "GetMuteAuthsByName";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByName", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                where a.muteAuthName == name
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByName", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByName", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByCity/{city}
        public muteAuthorityItem[] GetMuteAuthsByCity(string city)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByCity", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = city + "GetMuteAuthsByCity";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByCity", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                 where a.city == city
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByCity", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByCity", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByState/{state}
        public muteAuthorityItem[] GetMuteAuthsByState(string state)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByState", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = state + "GetMuteAuthsByState";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByState", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                 where a.state == state
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByState", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByState", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByCountry/{country}
        public muteAuthorityItem[] GetMuteAuthsByCountry(string country)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByCountry", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = country + "GetMuteAuthsByCountry";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByCountry", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                 where a.country == country
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByCountry", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByCountry", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByEmail/{email}
        public muteAuthorityItem[] GetMuteAuthsByEmail(string email)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByEmail", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = email + "GetMuteAuthsByEmail";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByEmail", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                 where a.email == email
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByEmail", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByEmail", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion

        #region GetMuteAuthsByPin/{pin}
        public muteAuthorityItem[] GetMuteAuthsByPin(string pin)
        {
            try
            {
                logger.LogMethod("jo", "GetMuteAuthsByPin", "Enter");
                List<muteAuthorityItem> list = new List<muteAuthorityItem>();
                key = pin + "GetMuteAuthsByPin";
                if (CachingConfig.CachingEnabled)
                {
                    list = (List<muteAuthorityItem>)WCFCache.Current[key];
                    if (list != null)
                    {
                        logger.LogMethod("jo", "GetMuteAuthsByPin", "Cache found");
                        return list.ToArray();
                    }
                }

                context = new SocialCopsEntities();
                List<MuteAuthority> muteAuths = (from a
                                                in context.MuteAuthorities
                                                 where a.pincode == pin
                                                 orderby a.date descending
                                                 select a).ToList();

                foreach (MuteAuthority auth in muteAuths)
                {
                    muteAuthorityItem temp = new muteAuthorityItem();
                    temp.muteAuthId = auth.muteAuthId;
                    temp.muteAuthName = auth.muteAuthName;
                    temp.muteAuthAddress = auth.muteAuthAddress;
                    temp.city = auth.city;
                    temp.state = auth.state;
                    temp.country = auth.country;
                    temp.email = auth.email;
                    temp.phone = auth.phone;
                    temp.latitude = (float)auth.latitude;
                    temp.longitude = (float)auth.longitude;
                    temp.website = auth.website;
                    temp.pic = auth.pic;
                    temp.date = (DateTime)auth.date;
                    temp.flag = (int)auth.flag;
                    temp.pincode = auth.pincode;
                    list.Add(temp);
                }
                Cache.Cache.AddToCache(key, list);
                logger.LogMethod("jo", "GetMuteAuthsByPin", "Exit");
                return list.ToArray();

            }
            catch (Exception ex)
            {
                error.ErrorDetails = ex.Message.ToString();
                error.ErrorMessage = "Something happened. Sorry.";
                error.Result = false;
                logger.LogMethod("jo", "GetMuteAuthsByPin", ex.Message.ToString());
                throw new FaultException<Bug>(error, ex.Message.ToString());
            }
        }
        #endregion
    }
}