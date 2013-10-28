using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_GeographicalGroupMember
    {
        public System.Guid Id { get; set; }

        [Required]
        public System.Guid GeographicalGroup_LCID { get; set; }

        public Nullable<System.Guid> Continent_LCID { get; set; }
        public Nullable<System.Guid> Country_LCID { get; set; }
        public Nullable<System.Guid> StateOrProv_LCID { get; set; }
        public Nullable<System.Guid> StateOrProvLocalityID { get; set; }
        public Nullable<System.Guid> AddrID { get; set; }
        public string PostalCode { get; set; }

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

        public AddrBk_Address AddrBk_Address { get; set; }
        public AddrBk_GeographicalGroup AddrBk_GeographicalGroup { get; set; }
        public Lookup_Continent Lookup_Continent { get; set; }
        public Lookup_Country Lookup_Country { get; set; }
        public Lookup_StateProvince Lookup_StateProvince { get; set; }
        public AddrBk_StateOrProvinceLocality AddrBk_StateOrProvinceLocality { get; set; }
    }
}
