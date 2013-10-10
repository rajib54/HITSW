using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_Timezone
    {
        public Lookup_Timezone()
        {
            this.AddrBk_ContactTimezone = new List<AddrBk_ContactTimezone>();
            this.Calendar_ResourceCalendar = new List<Calendar_ResourceCalendar>();
            this.Lookup_StateOrProvTimezone = new List<Lookup_StateOrProvTimezone>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
        public virtual ICollection<AddrBk_ContactTimezone> AddrBk_ContactTimezone { get; set; }
        public virtual ICollection<Calendar_ResourceCalendar> Calendar_ResourceCalendar { get; set; }
        public virtual ICollection<Lookup_StateOrProvTimezone> Lookup_StateOrProvTimezone { get; set; }
    }
}
