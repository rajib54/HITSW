using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_VirtualAddress
    {
        public System.Guid Id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public System.Guid VirtualAddrType_LCID { get; set; }

        [Required]
        public string Name { get; set; }

        public string VirtualAddrDomain { get; set; }
        public Nullable<byte> PreferSeq { get; set; }

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
        public Lookup_AddrType Lookup_AddrType { get; set; }
    }
}
