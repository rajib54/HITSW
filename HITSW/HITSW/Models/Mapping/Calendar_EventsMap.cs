using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Calendar_EventsMap : EntityTypeConfiguration<Calendar_Events>
    {
        public Calendar_EventsMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.text)
                .HasMaxLength(900);

            this.Property(t => t.rec_type)
                .HasMaxLength(50);

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
            this.ToTable("Calendar_Events");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.text).HasColumnName("text");
            this.Property(t => t.start_date).HasColumnName("start_date");
            this.Property(t => t.end_date).HasColumnName("end_date");
            this.Property(t => t.rec_type).HasColumnName("rec_type");
            this.Property(t => t.event_length).HasColumnName("event_length");
            this.Property(t => t.event_pid).HasColumnName("event_pid");
            this.Property(t => t.RsrcCalID).HasColumnName("RsrcCalID");
            this.Property(t => t.EvtFor_LCID).HasColumnName("EvtFor_LCID");
            this.Property(t => t.CalEvtStatus_LCID).HasColumnName("CalEvtStatus_LCID");
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
            this.HasRequired(t => t.Lookup_Status)
                .WithMany(t => t.Calendar_Events)
                .HasForeignKey(d => d.CalEvtStatus_LCID);
            this.HasRequired(t => t.Lookup_Calendar)
                .WithMany(t => t.Calendar_Events)
                .HasForeignKey(d => d.EvtFor_LCID);
            this.HasRequired(t => t.Calendar_ResourceCalendar)
                .WithMany(t => t.Calendar_Events)
                .HasForeignKey(d => d.RsrcCalID);

        }
    }
}
