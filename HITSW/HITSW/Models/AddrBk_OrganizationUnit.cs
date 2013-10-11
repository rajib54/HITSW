using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_OrganizationUnit
    {
        public AddrBk_OrganizationUnit()
        {
            this.AddrBk_ContactAddr = new List<AddrBk_ContactAddr>();
            this.AddrBk_ContactTimezone = new List<AddrBk_ContactTimezone>();
            this.AddrBk_GeographicalCollection = new List<AddrBk_GeographicalCollection>();
            this.AddrBk_Identification = new List<AddrBk_Identification>();
            this.AddrBk_Identification1 = new List<AddrBk_Identification>();
            this.AddrBk_IndustryAffliation = new List<AddrBk_IndustryAffliation>();
            this.AddrBk_InterestedProductSrvcs = new List<AddrBk_InterestedProductSrvcs>();
            this.AddrBk_Language = new List<AddrBk_Language>();
            this.AddrBk_OrganizationalUnitMember = new List<AddrBk_OrganizationalUnitMember>();
            this.AddrBk_PhoneFax = new List<AddrBk_PhoneFax>();
            this.AddrBk_Prospect = new List<AddrBk_Prospect>();
            this.AddrBk_VirtualAddress = new List<AddrBk_VirtualAddress>();
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
            this.Calendar_ResourceCalendar = new List<Calendar_ResourceCalendar>();
            this.AddrBk_Relation = new List<AddrBk_Relation>();
            this.AddrBk_Relation1 = new List<AddrBk_Relation>();
        }

        public System.Guid Id { get; set; }

        [Required]
        public bool IsProspect { get; set; }

        [Required(ErrorMessage="Organiation Type is required")]
        public System.Guid OUType_LCID { get; set; }

        [Required(ErrorMessage="Organization name is required")]
        public string Name { get; set; }

        public string OUDesc { get; set; }
        public Nullable<System.DateTime> OperatingSince { get; set; }

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

        [Required]
        public byte[] Concurrency { get; set; }

        public ICollection<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public ICollection<AddrBk_ContactTimezone> AddrBk_ContactTimezone { get; set; }
        public ICollection<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public ICollection<AddrBk_Identification> AddrBk_Identification { get; set; }
        public ICollection<AddrBk_Identification> AddrBk_Identification1 { get; set; }
        public ICollection<AddrBk_IndustryAffliation> AddrBk_IndustryAffliation { get; set; }
        public ICollection<AddrBk_InterestedProductSrvcs> AddrBk_InterestedProductSrvcs { get; set; }
        public ICollection<AddrBk_Language> AddrBk_Language { get; set; }
        public ICollection<AddrBk_OrganizationalUnitMember> AddrBk_OrganizationalUnitMember { get; set; }
        public ICollection<AddrBk_PhoneFax> AddrBk_PhoneFax { get; set; }
        public ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public ICollection<AddrBk_VirtualAddress> AddrBk_VirtualAddress { get; set; }
        public ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public ICollection<Calendar_ResourceCalendar> Calendar_ResourceCalendar { get; set; }
        public Lookup_ContactType Lookup_ContactType { get; set; }
        public ICollection<AddrBk_Relation> AddrBk_Relation { get; set; }
        public ICollection<AddrBk_Relation> AddrBk_Relation1 { get; set; }
    }
}
