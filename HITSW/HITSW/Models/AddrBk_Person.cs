using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_Person
    {
        public AddrBk_Person()
        {
            this.AddrBk_ContactAddr = new List<AddrBk_ContactAddr>();
            this.AddrBk_ContactTimezone = new List<AddrBk_ContactTimezone>();
            this.AddrBk_GeographicalCollection = new List<AddrBk_GeographicalCollection>();
            this.Addrbk_Hobby = new List<Addrbk_Hobby>();
            this.AddrBk_Identification = new List<AddrBk_Identification>();
            this.AddrBk_IndustryAffliation = new List<AddrBk_IndustryAffliation>();
            this.AddrBk_InterestedProductSrvcs = new List<AddrBk_InterestedProductSrvcs>();
            this.AddrBk_Language = new List<AddrBk_Language>();
            this.AddrBk_OrganizationalUnitMember = new List<AddrBk_OrganizationalUnitMember>();
            this.AddrBk_PhoneFax = new List<AddrBk_PhoneFax>();
            this.AddrBk_PhysicalActivity = new List<AddrBk_PhysicalActivity>();
            this.AddrBk_Prospect = new List<AddrBk_Prospect>();
            this.AddrBk_Veteran = new List<AddrBk_Veteran>();
            this.AddrBk_VirtualAddress = new List<AddrBk_VirtualAddress>();
            this.Calendar_EventSubscriber = new List<Calendar_EventSubscriber>();
            this.AddrBk_Relation = new List<AddrBk_Relation>();
            this.AddrBk_Relation1 = new List<AddrBk_Relation>();
        }

        public System.Guid Id { get; set; }
        public bool IsProspect { get; set; }
        public System.Guid Salutation_LCID { get; set; }

        [Required(ErrorMessage="First name is required")]
        public string FName { get; set; }

        [Required(ErrorMessage="Last name is required")]
        public string LName { get; set; }

        public string MName { get; set; }
        public Nullable<System.Guid> Suffix_LCID { get; set; }
        public Nullable<System.Guid> Gender_LCID { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<System.DateTime> DOD { get; set; }
        public Nullable<System.Guid> MaritalStatus_LCID { get; set; }
        public Nullable<System.Guid> Race_LCID { get; set; }
        public Nullable<System.Guid> Ethnicity_LCID { get; set; }
        public Nullable<System.Guid> ReligiousBkgd_LCID { get; set; }
        public Nullable<System.Guid> HighestEducLvl_LCID { get; set; }
        public Nullable<System.Guid> Occupation_LCID { get; set; }
        public Nullable<System.Guid> PhotoFile { get; set; }

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

        public ICollection<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public ICollection<AddrBk_ContactTimezone> AddrBk_ContactTimezone { get; set; }
        public ICollection<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public ICollection<Addrbk_Hobby> Addrbk_Hobby { get; set; }
        public ICollection<AddrBk_Identification> AddrBk_Identification { get; set; }
        public ICollection<AddrBk_IndustryAffliation> AddrBk_IndustryAffliation { get; set; }
        public ICollection<AddrBk_InterestedProductSrvcs> AddrBk_InterestedProductSrvcs { get; set; }
        public ICollection<AddrBk_Language> AddrBk_Language { get; set; }
        public ICollection<AddrBk_OrganizationalUnitMember> AddrBk_OrganizationalUnitMember { get; set; }
        public Lookup_Ethnicity Lookup_Ethnicity { get; set; }
        public Lookup_Gender Lookup_Gender { get; set; }
        public Lookup_EducationalLevel Lookup_EducationalLevel { get; set; }
        public ICollection<AddrBk_PhoneFax> AddrBk_PhoneFax { get; set; }
        public ICollection<AddrBk_PhysicalActivity> AddrBk_PhysicalActivity { get; set; }
        public ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public ICollection<AddrBk_Veteran> AddrBk_Veteran { get; set; }
        public ICollection<AddrBk_VirtualAddress> AddrBk_VirtualAddress { get; set; }
        public ICollection<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public Lookup_MaritalStatus Lookup_MaritalStatus { get; set; }
        public Lookup_Occupation Lookup_Occupation { get; set; }
        public ICollection<AddrBk_Relation> AddrBk_Relation { get; set; }
        public Lookup_Race Lookup_Race { get; set; }
        public ICollection<AddrBk_Relation> AddrBk_Relation1 { get; set; }
        public Lookup_AddrBk Lookup_AddrBk { get; set; }
        public Lookup_GenderRelationship Lookup_GenderRelationship { get; set; }
        public Lookup_AddrBk Lookup_AddrBk1 { get; set; }
    }
}
