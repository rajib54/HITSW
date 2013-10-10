using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_PhoneFax
    {
        public System.Guid Id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }
        public Nullable<System.Guid> FacilityID { get; set; }
        public Nullable<System.Guid> PhoneFaxType_LCID { get; set; }
        public string AreaCode { get; set; }
        public string PhoneFaxNum { get; set; }
        public string DialCntryCode { get; set; }
        public string DialCityCode { get; set; }
        public Nullable<byte> PreferPhoneSeq { get; set; }
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
        public virtual Lookup_PhoneFaxType Lookup_PhoneFaxType { get; set; }
    }
}
