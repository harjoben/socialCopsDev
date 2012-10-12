using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoreService.Error_Handling
{
    [DataContract]
    public class Log
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public object[] Parameters { get; set; }
    }
}