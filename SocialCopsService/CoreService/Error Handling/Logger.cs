using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreService.Error_Handling
{
    public class Logger
    {
        private static Log log = new Log();
        SocialCopsEntities context;
        public Log LogMethod(string username,string method,string message,params object[] parameters)
        {
            context = new SocialCopsEntities();
            log.username = username;
            log.logDetails = method;
            log.logMessage = message;
            log.date = DateTime.Now;
            context.Logs.Add(log);
            context.SaveChanges();
            return log;
        }

    }
}