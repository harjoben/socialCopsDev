//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoreService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Like
    {
        public int userId { get; set; }
        public int complaintId { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual Complaint Complaint { get; set; }
        public virtual User User { get; set; }
    }
}
