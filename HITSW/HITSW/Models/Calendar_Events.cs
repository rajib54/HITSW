using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Calendar_Events
    {
        public Calendar_Events()
        {
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
            this.Calendar_Notification = new List<Calendar_Notification>();
        }

        public int id { get; set; }
        public string text { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public string rec_type { get; set; }
        public Nullable<int> event_length { get; set; }
        public Nullable<int> event_pid { get; set; }
        public System.Guid RsrcCalID { get; set; }
        public System.Guid EvtFor_LCID { get; set; }
        public System.Guid CalEvtStatus_LCID { get; set; }
        public System.DateTimeOffset EffDt { get; set; }
        public Nullable<System.DateTimeOffset> IneffDt { get; set; }
        public string Cmmt { get; set; }
        public bool ActiveRec { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTimeOffset CreatedDt { get; set; }
        public string LastUpdatedFrom { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedDt { get; set; }
        public byte[] Concurrency { get; set; }
        public virtual Lookup_Status Lookup_Status { get; set; }
        public virtual ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public virtual ICollection<Calendar_Notification> Calendar_Notification { get; set; }
        public virtual Lookup_Calendar Lookup_Calendar { get; set; }
        public virtual Calendar_ResourceCalendar Calendar_ResourceCalendar { get; set; }
    }
}
