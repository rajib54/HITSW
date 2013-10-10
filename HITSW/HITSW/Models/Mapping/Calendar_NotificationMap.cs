using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Calendar_NotificationMap : EntityTypeConfiguration<Calendar_Notification>
    {
        public Calendar_NotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NotifMssg)
                .IsRequired()
                .HasMaxLength(900);

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
            this.ToTable("Calendar_Notification");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NotifMeth_LCID).HasColumnName("NotifMeth_LCID");
            this.Property(t => t.NotifMssg).HasColumnName("NotifMssg");
            this.Property(t => t.Evt_id).HasColumnName("Evt_id");
            this.Property(t => t.NotifStatus_LCID).HasColumnName("NotifStatus_LCID");
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
            this.HasRequired(t => t.Calendar_Events)
                .WithMany(t => t.Calendar_Notification)
                .HasForeignKey(d => d.Evt_id);
            this.HasRequired(t => t.Lookup_Calendar)
                .WithMany(t => t.Calendar_Notification)
                .HasForeignKey(d => d.NotifMeth_LCID);
            this.HasRequired(t => t.Lookup_Status)
                .WithMany(t => t.Calendar_Notification)
                .HasForeignKey(d => d.NotifStatus_LCID);

        }
    }
}
