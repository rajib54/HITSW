using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Calendar_EventSubscriberMap : EntityTypeConfiguration<Calendar_EventSubscriber>
    {
        public Calendar_EventSubscriberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Cmmt)
                .HasMaxLength(900);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            this.Property(t => t.LastUpdatedFrom)
                .HasMaxLength(50);

            this.Property(t => t.LastUpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.Concurrency)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Calendar_EventSubscriber");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Evt_id).HasColumnName("Evt_id");
            this.Property(t => t.SubscripBasis_LCID).HasColumnName("SubscripBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.FacilityID).HasColumnName("FacilityID");
            this.Property(t => t.FacilityUnitID).HasColumnName("FacilityUnitID");
            this.Property(t => t.InventID).HasColumnName("InventID");
            this.Property(t => t.TrackedProdID).HasColumnName("TrackedProdID");
            this.Property(t => t.MaintID).HasColumnName("MaintID");
            this.Property(t => t.EvtFor_LCID).HasColumnName("EvtFor_LCID");
            this.Property(t => t.OptionalAttendee).HasColumnName("OptionalAttendee");
            this.Property(t => t.Organizer).HasColumnName("Organizer");
            this.Property(t => t.SubscriberStatus_LCID).HasColumnName("SubscriberStatus_LCID");
            this.Property(t => t.EffDt).HasColumnName("EffDt");
            this.Property(t => t.IneffDt).HasColumnName("IneffDt");
            this.Property(t => t.Cmmt).HasColumnName("Cmmt");
            this.Property(t => t.ActiveRec).HasColumnName("ActiveRec");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDt).HasColumnName("CreatedDt");
            this.Property(t => t.LastUpdatedFrom).HasColumnName("LastUpdatedFrom");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDt).HasColumnName("LastUpdatedDt");
            this.Property(t => t.Concurrency).HasColumnName("Concurrency");

            // Relationships
            this.HasOptional(t => t.AddrBk_OrganizationUnit)
                .WithMany(t => t.Calendar_EventSubscriber)
                .HasForeignKey(d => d.OrgID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.Calendar_EventSubscriber)
                .HasForeignKey(d => d.IndivID);
            this.HasRequired(t => t.Calendar_Events)
                .WithMany(t => t.Calendar_EventSubscriber)
                .HasForeignKey(d => d.Evt_id);
            this.HasRequired(t => t.Lookup_Calendar)
                .WithMany(t => t.Calendar_EventSubscriber)
                .HasForeignKey(d => d.EvtFor_LCID);
            this.HasRequired(t => t.Lookup_Status)
                .WithMany(t => t.Calendar_EventSubscriber)
                .HasForeignKey(d => d.SubscriberStatus_LCID);
            this.HasRequired(t => t.Lookup_Calendar1)
                .WithMany(t => t.Calendar_EventSubscriber1)
                .HasForeignKey(d => d.SubscripBasis_LCID);

        }
    }
}
