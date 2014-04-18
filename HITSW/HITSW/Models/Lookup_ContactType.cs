using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class Lookup_ContactType
    {
        public Lookup_ContactType()
        {
            this.AddrBk_OrganizationalUnitMember = new List<AddrBk_OrganizationalUnitMember>();
            this.AddrBk_OrganizationUnit = new List<AddrBk_OrganizationUnit>();
            this.AddrBk_Prospect = new List<AddrBk_Prospect>();
        }

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ContactBasis_LCID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string TblColSel { get; set; }
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

        public ICollection<AddrBk_OrganizationalUnitMember> AddrBk_OrganizationalUnitMember { get; set; }
        public ICollection<AddrBk_OrganizationUnit> AddrBk_OrganizationUnit { get; set; }
        public ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public Lookup_ContactBasis Lookup_ContactBasis { get; set; }
    }
}
