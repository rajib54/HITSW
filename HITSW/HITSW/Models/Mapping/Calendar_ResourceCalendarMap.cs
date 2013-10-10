using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Calendar_ResourceCalendarMap : EntityTypeConfiguration<Calendar_ResourceCalendar>
    {
        public Calendar_ResourceCalendarMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.RsrcCalDesc)
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
            this.ToTable("Calendar_ResourceCalendar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TimeZone_LCID).HasColumnName("TimeZone_LCID");
            this.Property(t => t.RsrcCalFor_LCID).HasColumnName("RsrcCalFor_LCID");
            this.Property(t => t.OU_ID).HasColumnName("OU_ID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.RsrcCalDesc).HasColumnName("RsrcCalDesc");
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
            this.HasRequired(t => t.AddrBk_OrganizationUnit)
                .WithMany(t => t.Calendar_ResourceCalendar)
                .HasForeignKey(d => d.OU_ID);
            this.HasRequired(t => t.Lookup_Calendar)
                .WithMany(t => t.Calendar_ResourceCalendar)
                .HasForeignKey(d => d.RsrcCalFor_LCID);
            this.HasRequired(t => t.Lookup_Timezone)
                .WithMany(t => t.Calendar_ResourceCalendar)
                .HasForeignKey(d => d.TimeZone_LCID);

        }
    }
}
