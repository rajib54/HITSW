using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Lookup_StateOrProvTimezoneMap : EntityTypeConfiguration<Lookup_StateOrProvTimezone>
    {
        public Lookup_StateOrProvTimezoneMap()
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
            this.ToTable("Lookup_StateOrProvTimezone");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StateOrProvID).HasColumnName("StateOrProvID");
            this.Property(t => t.Timezone_LCID).HasColumnName("Timezone_LCID");
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
            this.HasRequired(t => t.Lookup_StateProvince)
                .WithMany(t => t.Lookup_StateOrProvTimezone)
                .HasForeignKey(d => d.StateOrProvID);
            this.HasRequired(t => t.Lookup_Timezone)
                .WithMany(t => t.Lookup_StateOrProvTimezone)
                .HasForeignKey(d => d.Timezone_LCID);

        }
    }
}
