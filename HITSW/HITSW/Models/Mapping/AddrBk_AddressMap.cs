using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_AddressMap : EntityTypeConfiguration<AddrBk_Address>
    {
        public AddrBk_AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.CityOrTown)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PostalCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

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
            this.ToTable("AddrBk_Address");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AddrType_LCID).HasColumnName("AddrType_LCID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.CityOrTown).HasColumnName("CityOrTown");
            this.Property(t => t.StateOrProv_LCID).HasColumnName("StateOrProv_LCID");
            this.Property(t => t.Cntry_LCID).HasColumnName("Cntry_LCID");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.SpatialLocation).HasColumnName("SpatialLocation");
            this.Property(t => t.AddrVerifStatus_LCID).HasColumnName("AddrVerifStatus_LCID");
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
            this.HasRequired(t => t.Lookup_AddrType)
                .WithMany(t => t.AddrBk_Address)
                .HasForeignKey(d => d.AddrType_LCID);
            this.HasRequired(t => t.Lookup_Status)
                .WithMany(t => t.AddrBk_Address)
                .HasForeignKey(d => d.AddrVerifStatus_LCID);
            this.HasRequired(t => t.Lookup_Country)
                .WithMany(t => t.AddrBk_Address)
                .HasForeignKey(d => d.Cntry_LCID);
            this.HasOptional(t => t.Lookup_StateProvince)
                .WithMany(t => t.AddrBk_Address)
                .HasForeignKey(d => d.StateOrProv_LCID);

        }
    }
}
