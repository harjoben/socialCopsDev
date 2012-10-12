using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoreService.Error_Handling
{
    public class Bug
    {
        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string ErrorDetails { get; set; }
    }
}