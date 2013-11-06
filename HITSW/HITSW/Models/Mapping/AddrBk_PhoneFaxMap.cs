using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_PhoneFaxMap : EntityTypeConfiguration<AddrBk_PhoneFax>
    {
        public AddrBk_PhoneFaxMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AreaCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PhoneFaxNum)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(7);

            this.Property(t => t.DialCntryCode)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DialCityCode)
                .IsFixedLength()
                .HasMaxLength(4);

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
            this.ToTable("AddrBk_PhoneFax");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContactBasis_LCID).HasColumnName("ContactBasis_LCID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.IndivID).HasColumnName("IndivID");
            this.Property(t => t.PhoneFaxType_LCID).HasColumnName("PhoneFaxType_LCID");
            this.Property(t => t.AreaCode).HasColumnName("AreaCode");
            this.Property(t => t.PhoneFaxNum).HasColumnName("PhoneFaxNum");
            this.Property(t => t.DialCntryCode).HasColumnName("DialCntryCode");
            this.Property(t => t.DialCityCode).HasColumnName("DialCityCode");
            this.Property(t => t.PreferPhoneSeq).HasColumnName("PreferPhoneSeq");
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
                .WithMany(t => t.AddrBk_PhoneFax)
                .HasForeignKey(d => d.OrgID);
            this.HasOptional(t => t.AddrBk_Person)
                .WithMany(t => t.AddrBk_PhoneFax)
                .HasForeignKey(d => d.IndivID);
            this.HasRequired(t => t.Lookup_ContactBasis)
                .WithMany(t => t.AddrBk_PhoneFax)
                .HasForeignKey(d => d.ContactBasis_LCID);
            this.HasOptional(t => t.Lookup_PhoneFaxType)
                .WithMany(t => t.AddrBk_PhoneFax)
                .HasForeignKey(d => d.PhoneFaxType_LCID);

        }
    }
}
