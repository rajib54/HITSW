using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_RelationMap : EntityTypeConfiguration<AddrBk_Relation>
    {
        public AddrBk_RelationMap()
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
            this.ToTable("AddrBk_Relation");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.RelnToPrimaryIndiv_LCID).HasColumnName("RelnToPrimaryIndiv_LCID");
            this.Property(t => t.PrimaryIndivID).HasColumnName("PrimaryIndivID");
            this.Property(t => t.RelatedIndivID).HasColumnName("RelatedIndivID");
            this.Property(t => t.RelnToPrimaryOrg_LCID).HasColumnName("RelnToPrimaryOrg_LCID");
            this.Property(t => t.PrimaryOrgID).HasColumnName("PrimaryOrgID");
            this.Property(t => t.RelatedOrgID).HasColumnName("RelatedOrgID");
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
                .WithMany(t => t.AddrBk_Relation)
                .HasForeignKey(d => d.PrimaryOrgID);
            this.HasOptional(t => t.AddrBk_OrganizationUnit1)
                .WithMany(t => t.AddrBk_Relation1)
                .HasForeignKey(d => d.RelatedOrgID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_Relation)
                .HasForeignKey(d => d.PrimaryIndivID);
            this.HasOptional(t => t.AddrBk_Person1)
                .WithMany(t => t.AddrBk_Relation1)
                .HasForeignKey(d => d.RelatedIndivID);
            this.HasRequired(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_Relation)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasOptional(t => t.Lookup_GenderRelationship)
                .WithMany(t => t.AddrBk_Relation)
                .HasForeignKey(d => d.RelnToPrimaryIndiv_LCID);
            this.HasOptional(t => t.Lookup_AddrBk)
                .WithMany(t => t.AddrBk_Relation)
                .HasForeignKey(d => d.RelnToPrimaryOrg_LCID);

        }
    }
}
