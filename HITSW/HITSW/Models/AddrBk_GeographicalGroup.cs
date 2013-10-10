using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class AddrBk_GeographicalGroup
    {
        public AddrBk_GeographicalGroup()
        {
            this.AddrBk_GeographicalCollection = new List<AddrBk_GeographicalCollection>();
            this.AddrBk_GeographicalGroupMember = new List<AddrBk_GeographicalGroupMember>();
        }

        public System.Guid Id { get; set; }
        public System.Guid GeoBasis_LCID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
        public virtual ICollection<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public virtual Lookup_GeographicalBasis Lookup_GeographicalBasis { get; set; }
        public virtual ICollection<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
    }
}
