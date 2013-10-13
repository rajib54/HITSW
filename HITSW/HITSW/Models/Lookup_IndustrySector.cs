using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class Lookup_IndustrySector
    {
        public Lookup_IndustrySector()
        {
            this.AddrBk_IndustryAffliation = new List<AddrBk_IndustryAffliation>();
            this.Lookup_Occupation = new List<Lookup_Occupation>();
        }

        public System.Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
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

        public ICollection<AddrBk_IndustryAffliation> AddrBk_IndustryAffliation { get; set; }
        public ICollection<Lookup_Occupation> Lookup_Occupation { get; set; }
    }
}
