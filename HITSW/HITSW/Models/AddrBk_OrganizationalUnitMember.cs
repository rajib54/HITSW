using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_OrganizationalUnitMember
    {
        public System.Guid Id { get; set; }
        public System.Guid OU_ID { get; set; }
        public System.Guid MemType_LCID { get; set; }
        public System.Guid MemberID { get; set; }
        public string MemTitle { get; set; }
        public Nullable<System.DateTime> MemberSince { get; set; }
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
        public virtual AddrBk_Person AddrBk_Person { get; set; }
        public virtual Lookup_ContactType Lookup_ContactType { get; set; }
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
    }
}
