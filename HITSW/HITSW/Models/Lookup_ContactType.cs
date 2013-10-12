using System;
using System.Collections.Generic;

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
        public string Title { get; set; }
        public string Description { get; set; }
        public string TblColSel { get; set; }
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
        public virtual ICollection<AddrBk_OrganizationalUnitMember> AddrBk_OrganizationalUnitMember { get; set; }
        public virtual ICollection<AddrBk_OrganizationUnit> AddrBk_OrganizationUnit { get; set; }
        public virtual ICollection<AddrBk_Prospect> AddrBk_Prospect { get; set; }
    }
}
