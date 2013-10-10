using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_StateOrProvinceLocalityMap : EntityTypeConfiguration<AddrBk_StateOrProvinceLocality>
    {
        public AddrBk_StateOrProvinceLocalityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
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
            this.ToTable("AddrBk_StateOrProvinceLocality");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GeoBasis_LCID).HasColumnName("GeoBasis_LCID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StateOrProv_LCID).HasColumnName("StateOrProv_LCID");
            this.Property(t => t.Cntry_LCID).HasColumnName("Cntry_LCID");
            this.Property(t => t.Sort).HasColumnName("Sort");
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
            this.HasRequired(t => t.Lookup_Country)
                .WithMany(t => t.AddrBk_StateOrProvinceLocality)
                .HasForeignKey(d => d.Cntry_LCID);
            this.HasRequired(t => t.Lookup_GeographicalBasis)
                .WithMany(t => t.AddrBk_StateOrProvinceLocality)
                .HasForeignKey(d => d.GeoBasis_LCID);
            this.HasRequired(t => t.Lookup_StateProvince)
                .WithMany(t => t.AddrBk_StateOrProvinceLocality)
                .HasForeignKey(d => d.StateOrProv_LCID);

        }
    }
}
