//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebEmployees
{
    using System;
    using System.Collections.Generic;
    
    public partial class canceledorders
    {
        public int idCanceledOrders { get; set; }
        public System.Guid Order { get; set; }
        public System.Guid Master { get; set; }
        public System.DateTime takenDate { get; set; }
        public System.DateTime canceledDate { get; set; }
        public string masterComment { get; set; }
    }
}