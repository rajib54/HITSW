using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_IdentificationMap : EntityTypeConfiguration<AddrBk_Identification>
    {
        public AddrBk_IdentificationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.LegalFirstN)
                .HasMaxLength(30);

            this.Property(t => t.LegalLastN)
                .HasMaxLength(30);

            this.Property(t => t.LegalMiddleN)
                .HasMaxLength(30);

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
            this.ToTable("AddrBk_Identification");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdentType_LCID).HasColumnName("IdentType_LCID");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.LegalFirstN).HasColumnName("LegalFirstN");
            this.Property(t => t.LegalLastN).HasColumnName("LegalLastN");
            this.Property(t => t.LegalMiddleN).HasColumnName("LegalMiddleN");
            this.Property(t => t.IssuedDt).HasColumnName("IssuedDt");
            this.Property(t => t.ExpDt).HasColumnName("ExpDt");
            this.Property(t => t.IdentificationIssuerID).HasColumnName("IdentificationIssuerID");
            this.Property(t => t.VerificationType_LCID).HasColumnName("VerificationType_LCID");
            this.Property(t => t.VerificDocID).HasColumnName("VerificDocID");
            this.Property(t => t.IndentVerifStatus_LCID).HasColumnName("IndentVerifStatus_LCID");
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
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasOptional(t => t.AddrBk_OrganizationUnit)
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.IdentificationIssuerID);
            this.HasOptional(t => t.Lookup_IdentificationType)
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.IdentType_LCID);
            this.HasRequired(t => t.Lookup_Status)
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.IndentVerifStatus_LCID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.IndivID);
            this.HasOptional(t => t.AddrBk_OrganizationUnit1)
                .WithMany(t => t.AddrBk_Identification1)
                .HasForeignKey(d => d.OrgID);
            this.HasRequired(t => t.Lookup_VerificationType)
                .WithMany(t => t.AddrBk_Identification)
                .HasForeignKey(d => d.VerificationType_LCID);

        }
    }
}
