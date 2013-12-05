using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_OrganizationUnitMap : EntityTypeConfiguration<AddrBk_OrganizationUnit>
    {
        public AddrBk_OrganizationUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.OUDesc)
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
            this.ToTable("AddrBk_OrganizationUnit");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsProspect).HasColumnName("IsProspect");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OUType_LCID).HasColumnName("OUType_LCID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.OUDesc).HasColumnName("OUDesc");
            this.Property(t => t.OperatingSince).HasColumnName("OperatingSince");
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
            this.HasOptional(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_OrganizationUnit)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasRequired(t => t.Lookup_ContactType)
                .WithMany(t => t.AddrBk_OrganizationUnit)
                .HasForeignKey(d => d.OUType_LCID);

        }
    }
}
