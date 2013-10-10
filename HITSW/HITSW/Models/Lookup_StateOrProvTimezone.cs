using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Lookup_StateOrProvTimezone
    {
        public System.Guid Id { get; set; }
        public System.Guid StateOrProvID { get; set; }
        public System.Guid Timezone_LCID { get; set; }
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
        public virtual Lookup_StateProvince Lookup_StateProvince { get; set; }
        public virtual Lookup_Timezone Lookup_Timezone { get; set; }
    }
}
