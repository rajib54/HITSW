using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_Relation
    {
        public System.Guid Id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> RelnToPrimaryIndiv_LCID { get; set; }
        public Nullable<System.Guid> PrimaryIndivID { get; set; }
        public Nullable<System.Guid> RelatedIndivID { get; set; }
        public Nullable<System.Guid> RelnToPrimaryOrg_LCID { get; set; }
        public Nullable<System.Guid> PrimaryOrgID { get; set; }
        public Nullable<System.Guid> RelatedOrgID { get; set; }

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

        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit1 { get; set; }
        public AddrBk_Person AddrBk_Person { get; set; }
        public AddrBk_Person AddrBk_Person1 { get; set; }
        public Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public Lookup_GenderRelationship Lookup_GenderRelationship { get; set; }
        public Lookup_AddrBk Lookup_AddrBk { get; set; }
    }
}
