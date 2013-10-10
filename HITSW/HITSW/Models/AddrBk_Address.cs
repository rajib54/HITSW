using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_Address
    {
        public AddrBk_Address()
        {
            this.AddrBk_ContactAddr = new List<AddrBk_ContactAddr>();
            this.AddrBk_GeographicalGroupMember = new List<AddrBk_GeographicalGroupMember>();
        }

        public System.Guid Id { get; set; }
        public System.Guid AddrType_LCID { get; set; }
        public string Title { get; set; }
        public System.Guid StateOrProvLocalityID { get; set; }
        public System.Guid StateOrProv_LCID { get; set; }
        public System.Guid Cntry_LCID { get; set; }
        public string PostalCode { get; set; }
        public System.Data.Spatial.DbGeography SpatialLocation { get; set; }
        public System.Guid AddrVerifStatus_LCID { get; set; }
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
        public virtual ICollection<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public virtual ICollection<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
        public virtual Lookup_AddrType Lookup_AddrType { get; set; }
        public virtual Lookup_VerificationType Lookup_VerificationType { get; set; }
        public virtual Lookup_Country Lookup_Country { get; set; }
        public virtual Lookup_StateProvince Lookup_StateProvince { get; set; }
        public virtual AddrBk_StateOrProvinceLocality AddrBk_StateOrProvinceLocality { get; set; }
    }
}
