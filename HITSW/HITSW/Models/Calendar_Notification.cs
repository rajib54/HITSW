using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Calendar_Notification
    {
        public System.Guid Id { get; set; }
        public System.Guid NotifMeth_LCID { get; set; }
        public string NotifMssg { get; set; }
        public int Evt_id { get; set; }
        public System.Guid NotifStatus_LCID { get; set; }
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
        public virtual Calendar_Events Calendar_Events { get; set; }
        public virtual Lookup_Calendar Lookup_Calendar { get; set; }
        public virtual Lookup_Status Lookup_Status { get; set; }
    }
}
