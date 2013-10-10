using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_Identification
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> IdentType_LCID { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }
        public string Title { get; set; }
        public string LegalFirstN { get; set; }
        public string LegalLastN { get; set; }
        public string LegalMiddleN { get; set; }
        public Nullable<System.DateTime> IssuedDt { get; set; }
        public Nullable<System.DateTime> ExpDt { get; set; }
        public Nullable<System.Guid> IdentificationIssuerID { get; set; }
        public System.Guid VerificationType_LCID { get; set; }
        public Nullable<System.Guid> VerificDocID { get; set; }
        public System.Guid IndentVerifStatus_LCID { get; set; }
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
        public virtual Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public virtual Lookup_IdentificationType Lookup_IdentificationType { get; set; }
        public virtual Lookup_Status Lookup_Status { get; set; }
        public virtual AddrBk_Person AddrBk_Person { get; set; }
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit1 { get; set; }
        public virtual Lookup_VerificationType Lookup_VerificationType { get; set; }
    }
}
