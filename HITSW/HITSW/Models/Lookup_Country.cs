using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_Country
    {
        public Lookup_Country()
        {
            this.AddrBk_Address = new List<AddrBk_Address>();
            this.AddrBk_GeographicalGroupMember = new List<AddrBk_GeographicalGroupMember>();
            this.AddrBk_StateOrProvinceLocality = new List<AddrBk_StateOrProvinceLocality>();
            this.Lookup_StateProvince = new List<Lookup_StateProvince>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string CntyDescr { get; set; }
        public string Alfa2Code { get; set; }
        public string Alfa3Code { get; set; }
        public string NumCode { get; set; }
        public System.Guid Continent_LCID { get; set; }
        public Nullable<System.Guid> Currency_LCID { get; set; }
        public Nullable<int> Sort { get; set; }
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
        public virtual ICollection<AddrBk_Address> AddrBk_Address { get; set; }
        public virtual ICollection<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
        public virtual ICollection<AddrBk_StateOrProvinceLocality> AddrBk_StateOrProvinceLocality { get; set; }
        public virtual Lookup_Continent Lookup_Continent { get; set; }
        public virtual ICollection<Lookup_StateProvince> Lookup_StateProvince { get; set; }
        public virtual Lookup_CurrencyCode Lookup_CurrencyCode { get; set; }
    }
}
