using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_ProspectMap : EntityTypeConfiguration<AddrBk_Prospect>
    {
        public AddrBk_ProspectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.LeadSrcName)
                .HasMaxLength(64);

            this.Property(t => t.CurrentVendors)
                .HasMaxLength(900);

            this.Property(t => t.SpecialReqmt)
                .HasMaxLength(900);

            this.Property(t => t.OutcomeToDate)
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
            this.ToTable("AddrBk_Prospect");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.ProspectType_LCID).HasColumnName("ProspectType_LCID");
            this.Property(t => t.ContactDt).HasColumnName("ContactDt");
            this.Property(t => t.LeadSrcType_LCID).HasColumnName("LeadSrcType_LCID");
            this.Property(t => t.LeadSrcName).HasColumnName("LeadSrcName");
            this.Property(t => t.CurrentVendors).HasColumnName("CurrentVendors");
            this.Property(t => t.AnnualGrossRevOrInc).HasColumnName("AnnualGrossRevOrInc");
            this.Property(t => t.NumOfEmpOrMem).HasColumnName("NumOfEmpOrMem");
            this.Property(t => t.SpecialReqmt).HasColumnName("SpecialReqmt");
            this.Property(t => t.OutcomeToDate).HasColumnName("OutcomeToDate");
            this.Property(t => t.ProspectStatus_LCID).HasColumnName("ProspectStatus_LCID");
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
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.OrgID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.IndivID);
            this.HasRequired(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasRequired(t => t.Lookup_LeadSourceType)
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.LeadSrcType_LCID);
            this.HasOptional(t => t.Lookup_Status)
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.ProspectStatus_LCID);
            this.HasRequired(t => t.Lookup_ContactType)
                .WithMany(t => t.AddrBk_Prospect)
                .HasForeignKey(d => d.ProspectType_LCID);

        }
    }
}
