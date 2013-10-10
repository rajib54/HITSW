using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_VirtualAddressMap : EntityTypeConfiguration<AddrBk_VirtualAddress>
    {
        public AddrBk_VirtualAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VirtualAddrDomain)
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
            this.ToTable("AddrBk_VirtualAddress");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.VirtualAddrType_LCID).HasColumnName("VirtualAddrType_LCID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.VirtualAddrDomain).HasColumnName("VirtualAddrDomain");
            this.Property(t => t.PreferSeq).HasColumnName("PreferSeq");
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
                .WithMany(t => t.AddrBk_VirtualAddress)
                .HasForeignKey(d => d.OrgID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_VirtualAddress)
                .HasForeignKey(d => d.IndivID);
            this.HasRequired(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_VirtualAddress)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasRequired(t => t.Lookup_AddrType)
                .WithMany(t => t.AddrBk_VirtualAddress)
                .HasForeignKey(d => d.VirtualAddrType_LCID);

        }
    }
}
