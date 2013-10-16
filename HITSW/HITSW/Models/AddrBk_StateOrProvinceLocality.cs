using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_StateOrProvinceLocality
    {
        public AddrBk_StateOrProvinceLocality()
        {
            this.AddrBk_Address = new List<AddrBk_Address>();
            this.AddrBk_GeographicalGroupMember = new List<AddrBk_GeographicalGroupMember>();
        }

        public System.Guid Id { get; set; }

        [Required]
        public System.Guid GeoBasis_LCID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public System.Guid StateOrProv_LCID { get; set; }

        [Required]
        public System.Guid Cntry_LCID { get; set; }

        public Nullable<int> Sort { get; set; }

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

        public ICollection<AddrBk_Address> AddrBk_Address { get; set; }
        public ICollection<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
        public Lookup_Country Lookup_Country { get; set; }
        public Lookup_GeographicalBasis Lookup_GeographicalBasis { get; set; }
        public Lookup_StateProvince Lookup_StateProvince { get; set; }
    }
}
