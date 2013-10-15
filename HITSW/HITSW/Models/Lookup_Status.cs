using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class Lookup_Status
    {
        public Lookup_Status()
        {
            this.AddrBk_Identification = new List<AddrBk_Identification>();
            this.AddrBk_Prospect = new List<AddrBk_Prospect>();
            this.Calendar_Events = new List<Calendar_Events>();
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
            this.Calendar_Notification = new List<Calendar_Notification>();
        }

        public System.Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string TblColSel { get; set; }
        public Nullable<int> Sort { get; set; }

        [Required]
        public System.DateTimeOffset EffDt { get; set; }

        public Nullable<System.DateTimeOffset> IneffDt { get; set; }
        public string Cmmt { get; set; }

        [Required]
        public bool ActiveRec { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        public System.DateTimeOffset CreatedDt { get; set; }

        public string LastUpdatedFrom { get; set; }
        public string LastUpdatedBy { get; set; }

        [Required]
        public System.DateTimeOffset LastUpdatedDt { get; set; }

        [Timestamp]
        public byte[] Concurrency { get; set; }

        public ICollection<AddrBk_Identification> AddrBk_Identification { get; set; }
        public ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public ICollection<Calendar_Events> Calendar_Events { get; set; }
        public ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public ICollection<Calendar_Notification> Calendar_Notification { get; set; }
    }
}
