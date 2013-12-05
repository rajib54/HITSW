using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_Relation
    {
        public System.Guid Id { get; set; }

        [Required]
        public System.Guid ContactBasis_LCID { get; set; }

        public Nullable<System.Guid> RelnToPrimaryIndiv_LCID { get; set; }
        public Nullable<System.Guid> PrimaryIndivID { get; set; }
        public Nullable<System.Guid> RelatedIndivID { get; set; }
        public Nullable<System.Guid> RelnToPrimaryExtOrg_LCID { get; set; }
        public Nullable<System.Guid> PrimaryExtOrgID { get; set; }
        public Nullable<System.Guid> RelatedExtOrgID { get; set; }
        public Nullable<System.Guid> RelnToExtToIntOrg_LCID { get; set; }
        public Nullable<System.Guid> PrimaryExtOrgID1 { get; set; }
        public Nullable<System.Guid> RelatedIntOrgID1 { get; set; }

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
        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit2 { get; set; }
        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit3 { get; set; }
        public AddrBk_Person AddrBk_Person { get; set; }
        public AddrBk_Person AddrBk_Person1 { get; set; }
        public Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public Lookup_AddrBk Lookup_AddrBk { get; set; }
        public Lookup_AddrBk Lookup_AddrBk1 { get; set; }
        public Lookup_GenderRelationship Lookup_GenderRelationship { get; set; }
    }
}
