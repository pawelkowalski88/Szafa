//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLiteDatabaseConnection
{
    using System;
    using System.Collections.Generic;
    
    public partial class clothes
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picture_path { get; set; }
        public Nullable<long> type_id { get; set; }
        public string description { get; set; }
        public Nullable<bool> in_use { get; set; }
        public Nullable<System.DateTime> in_use_from { get; set; }
        public Nullable<long> times_on { get; set; }
        public Nullable<System.DateTime> last_time_on { get; set; }
    
        public virtual types types { get; set; }
    }
}
