namespace DatabaseEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class clothes
    {
        public long id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        //[Required]
        [StringLength(256)]
        public string picture_path { get; set; }

        public long type_id { get; set; }

        [StringLength(2000)]
        public string description { get; set; }

        public bool? in_use { get; set; }

        public DateTime? in_use_from { get; set; }

        public long? times_on { get; set; }

        public DateTime? last_time_on { get; set; }

        public virtual types types { get; set; }
    }
}
