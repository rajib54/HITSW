using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_GeographicalBasis
    {
        public Lookup_GeographicalBasis()
        {
            this.AddrBk_GeographicalGroup = new List<AddrBk_GeographicalGroup>();
            this.AddrBk_StateOrProvinceLocality = new List<AddrBk_StateOrProvinceLocality>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
        public virtual ICollection<AddrBk_GeographicalGroup> AddrBk_GeographicalGroup { get; set; }
        public virtual ICollection<AddrBk_StateOrProvinceLocality> AddrBk_StateOrProvinceLocality { get; set; }
    }
}
