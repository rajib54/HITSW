using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Address type is required")]
        public System.Guid AddrType_LCID { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(64, ErrorMessage = "Address should be maximum 64 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(64, ErrorMessage = "City should be maximum 100 characters")]
        public string CityOrTown { get; set; }

        public Nullable<System.Guid> StateOrProv_LCID { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public System.Guid Cntry_LCID { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(10, ErrorMessage = "Postal code should be maximum 10 characters")]
        public string PostalCode { get; set; }

        public System.Data.Spatial.DbGeography SpatialLocation { get; set; }

        [Required(ErrorMessage = "Verificaition status is required")]
        public System.Guid AddrVerifStatus_LCID { get; set; }
        
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

        public ICollection<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public ICollection<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
        public Lookup_AddrType Lookup_AddrType { get; set; }
        public Lookup_Status Lookup_Status { get; set; }
        public Lookup_Country Lookup_Country { get; set; }
        public Lookup_StateProvince Lookup_StateProvince { get; set; }
    }
}
