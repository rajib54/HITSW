using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_InterestedProductSrvcsMap : EntityTypeConfiguration<AddrBk_InterestedProductSrvcs>
    {
        public AddrBk_InterestedProductSrvcsMap()
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
            this.ToTable("AddrBk_InterestedProductSrvcs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.InterestedProdSrvc_LCID).HasColumnName("InterestedProdSrvc_LCID");
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
            this.HasRequired(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_InterestedProductSrvcs)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_InterestedProductSrvcs)
                .HasForeignKey(d => d.IndivID);
            this.HasRequired(t => t.Lookup_AddrBk)
                .WithMany(t => t.AddrBk_InterestedProductSrvcs)
                .HasForeignKey(d => d.InterestedProdSrvc_LCID);
            this.HasOptional(t => t.AddrBk_OrganizationUnit)
                .WithMany(t => t.AddrBk_InterestedProductSrvcs)
                .HasForeignKey(d => d.OrgID);

        }
    }
}
