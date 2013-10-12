using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_GenderRelationship
    {
        public Lookup_GenderRelationship()
        {
            this.AddrBk_Person = new List<AddrBk_Person>();
            this.AddrBk_Relation = new List<AddrBk_Relation>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> Gender_LCID { get; set; }
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

        public ICollection<AddrBk_Person> AddrBk_Person { get; set; }
        public ICollection<AddrBk_Relation> AddrBk_Relation { get; set; }
        public Lookup_Gender Lookup_Gender { get; set; }
    }
}
