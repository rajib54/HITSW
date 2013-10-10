using System;
using System.Collections.Generic;

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
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit1 { get; set; }
        public virtual AddrBk_Person AddrBk_Person { get; set; }
        public virtual AddrBk_Person AddrBk_Person1 { get; set; }
        public virtual Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public virtual Lookup_GenderRelationship Lookup_GenderRelationship { get; set; }
        public virtual Lookup_AddrBk Lookup_AddrBk { get; set; }
    }
}
