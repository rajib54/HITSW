using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_Prospect
    {
        public System.Guid Id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }
        public System.Guid ProspectType_LCID { get; set; }
        public System.DateTime ContactDt { get; set; }
        public System.Guid LeadSrcType_LCID { get; set; }
        public string LeadSrcName { get; set; }
        public string CurrentVendors { get; set; }
        public Nullable<decimal> AnnualGrossRevOrInc { get; set; }
        public Nullable<int> NumOfEmpOrMem { get; set; }
        public string SpecialReqmt { get; set; }
        public string OutcomeToDate { get; set; }
        public Nullable<System.Guid> ProspectStatus_LCID { get; set; }
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
        public virtual AddrBk_Person AddrBk_Person { get; set; }
        public virtual Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public virtual Lookup_LeadSourceType Lookup_LeadSourceType { get; set; }
        public virtual Lookup_Status Lookup_Status { get; set; }
        public virtual Lookup_ContactType Lookup_ContactType { get; set; }
    }
}
