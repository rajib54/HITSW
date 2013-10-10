using System;
using System.Collections.Generic;

namespace HITSW.Models
{
    public partial class Calendar_EventSubscriber
    {
        public System.Guid Id { get; set; }
        public int Evt_id { get; set; }
        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }
        public Nullable<System.Guid> FacilityID { get; set; }
        public Nullable<System.Guid> FacilityUnitID { get; set; }
        public Nullable<System.Guid> InventID { get; set; }
        public Nullable<System.Guid> TrackedProdID { get; set; }
        public Nullable<System.Guid> MaintID { get; set; }
        public System.Guid EvtFor_LCID { get; set; }
        public Nullable<bool> OptionalAttendee { get; set; }
        public Nullable<bool> Organizer { get; set; }
        public System.Guid SubscriberStatus_LCID { get; set; }
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
        public virtual AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public virtual AddrBk_Person AddrBk_Person { get; set; }
        public virtual Calendar_Events Calendar_Events { get; set; }
        public virtual Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public virtual Lookup_Calendar Lookup_Calendar { get; set; }
        public virtual Lookup_Status Lookup_Status { get; set; }
    }
}
