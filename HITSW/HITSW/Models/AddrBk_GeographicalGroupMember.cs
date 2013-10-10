using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_GeographicalGroupMember
    {
        public System.Guid Id { get; set; }
        public System.Guid GeographicalGroup_LCID { get; set; }
        public Nullable<System.Guid> Continent_LCID { get; set; }
        public Nullable<System.Guid> Country_LCID { get; set; }
        public Nullable<System.Guid> StateOrProv_LCID { get; set; }
        public Nullable<System.Guid> StateOrProvLocalityID { get; set; }
        public Nullable<System.Guid> AddrID { get; set; }
        public string PostalCode { get; set; }
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
        public virtual AddrBk_Address AddrBk_Address { get; set; }
        public virtual AddrBk_GeographicalGroup AddrBk_GeographicalGroup { get; set; }
        public virtual Lookup_Continent Lookup_Continent { get; set; }
        public virtual Lookup_Country Lookup_Country { get; set; }
        public virtual Lookup_StateProvince Lookup_StateProvince { get; set; }
        public virtual AddrBk_StateOrProvinceLocality AddrBk_StateOrProvinceLocality { get; set; }
    }
}
