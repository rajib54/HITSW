using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_OrganizationalUnitMemberMap : EntityTypeConfiguration<AddrBk_OrganizationalUnitMember>
    {
        public AddrBk_OrganizationalUnitMemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MemTitle)
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
            this.ToTable("AddrBk_OrganizationalUnitMember");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OU_ID).HasColumnName("OU_ID");
            this.Property(t => t.MemType_LCID).HasColumnName("MemType_LCID");
            this.Property(t => t.MemberID).HasColumnName("MemberID");
            this.Property(t => t.MemTitle).HasColumnName("MemTitle");
            this.Property(t => t.MemberSince).HasColumnName("MemberSince");
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
            this.HasRequired(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_OrganizationalUnitMember)
                .HasForeignKey(d => d.MemberID);
            this.HasRequired(t => t.Lookup_ContactType)
                .WithMany(t => t.AddrBk_OrganizationalUnitMember)
                .HasForeignKey(d => d.MemType_LCID);
            this.HasRequired(t => t.AddrBk_OrganizationUnit)
                .WithMany(t => t.AddrBk_OrganizationalUnitMember)
                .HasForeignKey(d => d.OU_ID);

        }
    }
}
