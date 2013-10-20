using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class Addrbk_Hobby
    {
        public System.Guid Id { get; set; }
        public System.Guid IndivID { get; set; }

        [Required(ErrorMessage="Hobby is required")]
        public System.Guid Hobby_LCID { get; set; }

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

        public Lookup_AddrBk Lookup_AddrBk { get; set; }
        public AddrBk_Person AddrBk_Person { get; set; }
    }
}
