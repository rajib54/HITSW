using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Calendar_ResourceCalendar
    {
        public Calendar_ResourceCalendar()
        {
            this.Calendar_Events = new List<Calendar_Events>();
        }

        public System.Guid Id { get; set; }
        public System.Guid TimeZone_LCID { get; set; }
        public System.Guid RsrcCalFor_LCID { get; set; }
        public System.Guid OU_ID { get; set; }
        public string Title { get; set; }
        public string RsrcCalDesc { get; set; }
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
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public virtual ICollection<Calendar_Events> Calendar_Events { get; set; }
        public virtual Lookup_Calendar Lookup_Calendar { get; set; }
        public virtual Lookup_Timezone Lookup_Timezone { get; set; }
    }
}
