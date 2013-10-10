using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_ContactBasis
    {
        public Lookup_ContactBasis()
        {
            this.AddrBk_ContactAddr = new List<AddrBk_ContactAddr>();
            this.AddrBk_ContactTimezone = new List<AddrBk_ContactTimezone>();
            this.AddrBk_GeographicalCollection = new List<AddrBk_GeographicalCollection>();
            this.AddrBk_Identification = new List<AddrBk_Identification>();
            this.AddrBk_IndustryAffliation = new List<AddrBk_IndustryAffliation>();
            this.AddrBk_InterestedProductSrvcs = new List<AddrBk_InterestedProductSrvcs>();
            this.AddrBk_Language = new List<AddrBk_Language>();
            this.AddrBk_PhoneFax = new List<AddrBk_PhoneFax>();
            this.AddrBk_Prospect = new List<AddrBk_Prospect>();
            this.AddrBk_Relation = new List<AddrBk_Relation>();
            this.AddrBk_VirtualAddress = new List<AddrBk_VirtualAddress>();
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
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
        public string LastUpdatedrom { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedDt { get; set; }
        public byte[] Concurrency { get; set; }
        public virtual ICollection<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public virtual ICollection<AddrBk_ContactTimezone> AddrBk_ContactTimezone { get; set; }
        public virtual ICollection<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public virtual ICollection<AddrBk_Identification> AddrBk_Identification { get; set; }
        public virtual ICollection<AddrBk_IndustryAffliation> AddrBk_IndustryAffliation { get; set; }
        public virtual ICollection<AddrBk_InterestedProductSrvcs> AddrBk_InterestedProductSrvcs { get; set; }
        public virtual ICollection<AddrBk_Language> AddrBk_Language { get; set; }
        public virtual ICollection<AddrBk_PhoneFax> AddrBk_PhoneFax { get; set; }
        public virtual ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public virtual ICollection<AddrBk_Relation> AddrBk_Relation { get; set; }
        public virtual ICollection<AddrBk_VirtualAddress> AddrBk_VirtualAddress { get; set; }
        public virtual ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
    }
}
