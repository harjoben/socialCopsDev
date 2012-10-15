using CoreService.Error_Handling;
using CoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace CoreService.Controllers
{
    public class UserController
    {
        Bug error = new Bug();
        Logger logger = new Logger();
        socialcopsentity context;

        #region SaveUser
        public bool SaveUser(userItem user)
        {
            try
            {
                logger.LogMethod("jo", "SaveUser", "Enter", null);
                context = new socialcopsentity();
                User temp = new User();

                List<User> users = (from u
                                    in context.Users
                                    where u.email == user.email
                                    select u).ToList();

                if (users.Count > 0)
                {
                    logger.LogMethod("jo", "SaveUser", "Invalid Sign-up. Email address already exists.");
                    error.Result = false;
                    error.ErrorMessage = "Email address already exists. Cannot register again.";
                    throw new FaultException<Bug>(error);
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
                temp.date = System.DateTime.Now;
                context.Users.Add(temp);
                context.SaveChanges();
                logger.LogMethod("jo", "SaveUser", "Exit", null);
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
                context = new socialcopsentity();
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
                    temp.phoneURI = user.phoneUri;
                    temp.date = System.DateTime.Now;
                    temp.phone = user.phone.ToString();
                    temp.points = 0;
                    temp.profilePic = user.profilePic;
                    temp.userAddress = user.userAddress;
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
    }
}