using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_Calendar
    {
        public Lookup_Calendar()
        {
            this.Calendar_Events = new List<Calendar_Events>();
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
            this.Calendar_Notification = new List<Calendar_Notification>();
            this.Calendar_ResourceCalendar = new List<Calendar_ResourceCalendar>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TblColSel { get; set; }
        public Nullable<int> Sort { get; set; }
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
        public virtual ICollection<Calendar_Events> Calendar_Events { get; set; }
        public virtual ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public virtual ICollection<Calendar_Notification> Calendar_Notification { get; set; }
        public virtual ICollection<Calendar_ResourceCalendar> Calendar_ResourceCalendar { get; set; }
    }
}
