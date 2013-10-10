using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_AddrBk
    {
        public Lookup_AddrBk()
        {
            this.AddrBk_GeographicalCollection = new List<AddrBk_GeographicalCollection>();
            this.Addrbk_Hobby = new List<Addrbk_Hobby>();
            this.AddrBk_InterestedProductSrvcs = new List<AddrBk_InterestedProductSrvcs>();
            this.AddrBk_Person = new List<AddrBk_Person>();
            this.AddrBk_Person1 = new List<AddrBk_Person>();
            this.AddrBk_PhysicalActivity = new List<AddrBk_PhysicalActivity>();
            this.AddrBk_PhysicalActivity1 = new List<AddrBk_PhysicalActivity>();
            this.AddrBk_Relation = new List<AddrBk_Relation>();
            this.AddrBk_Veteran = new List<AddrBk_Veteran>();
        }

        public System.Guid Id { get; set; }
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
        public virtual ICollection<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public virtual ICollection<Addrbk_Hobby> Addrbk_Hobby { get; set; }
        public virtual ICollection<AddrBk_InterestedProductSrvcs> AddrBk_InterestedProductSrvcs { get; set; }
        public virtual ICollection<AddrBk_Person> AddrBk_Person { get; set; }
        public virtual ICollection<AddrBk_Person> AddrBk_Person1 { get; set; }
        public virtual ICollection<AddrBk_PhysicalActivity> AddrBk_PhysicalActivity { get; set; }
        public virtual ICollection<AddrBk_PhysicalActivity> AddrBk_PhysicalActivity1 { get; set; }
        public virtual ICollection<AddrBk_Relation> AddrBk_Relation { get; set; }
        public virtual ICollection<AddrBk_Veteran> AddrBk_Veteran { get; set; }
    }
}
