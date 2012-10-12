using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreService.Error_Handling
{
    public class Logger
    {
        private static Log log = new Log();

        public void LogMethod(string username,string method,string message,params object[] parameters)
        {
            log.UserName = username;
            log.MethodName = method;
            log.Message = message;
            log.Parameters = parameters;
        }

    }
}