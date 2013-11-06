using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_PhoneFax
    {
        public System.Guid Id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }
        public Nullable<System.Guid> PhoneFaxType_LCID { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Area code should be maximum of 3 digits")]
        public string AreaCode { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "Phone fax number should be maximum of 7 digits")]
        public string PhoneFaxNum { get; set; }

        [StringLength(3, ErrorMessage = "Country code should be maximum of 3 digits")]
        public string DialCntryCode { get; set; }

        [StringLength(4, ErrorMessage = "City code should be maximum of 4 digits")]
        public string DialCityCode { get; set; }

        public Nullable<byte> PreferPhoneSeq { get; set; }

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

        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public AddrBk_Person AddrBk_Person { get; set; }
        public Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public Lookup_PhoneFaxType Lookup_PhoneFaxType { get; set; }
    }
}
