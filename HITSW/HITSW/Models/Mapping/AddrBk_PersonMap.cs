using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_PersonMap : EntityTypeConfiguration<AddrBk_Person>
    {
        public AddrBk_PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.MName)
                .HasMaxLength(30);

            this.Property(t => t.JobTitle)
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
            this.ToTable("AddrBk_Person");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsProspect).HasColumnName("IsProspect");
            this.Property(t => t.Salutation_LCID).HasColumnName("Salutation_LCID");
            this.Property(t => t.FName).HasColumnName("FName");
            this.Property(t => t.LName).HasColumnName("LName");
            this.Property(t => t.MName).HasColumnName("MName");
            this.Property(t => t.Suffix_LCID).HasColumnName("Suffix_LCID");
            this.Property(t => t.Gender_LCID).HasColumnName("Gender_LCID");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.DOD).HasColumnName("DOD");
            this.Property(t => t.MaritalStatus_LCID).HasColumnName("MaritalStatus_LCID");
            this.Property(t => t.Race_LCID).HasColumnName("Race_LCID");
            this.Property(t => t.Ethnicity_LCID).HasColumnName("Ethnicity_LCID");
            this.Property(t => t.ReligiousBkgd_LCID).HasColumnName("ReligiousBkgd_LCID");
            this.Property(t => t.HighestEducLvl_LCID).HasColumnName("HighestEducLvl_LCID");
            this.Property(t => t.Occupation_LCID).HasColumnName("Occupation_LCID");
            this.Property(t => t.PhotoFile).HasColumnName("PhotoFile");
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
            this.HasOptional(t => t.Lookup_Ethnicity)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.Ethnicity_LCID);
            this.HasOptional(t => t.Lookup_Gender)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.Gender_LCID);
            this.HasOptional(t => t.Lookup_EducationalLevel)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.HighestEducLvl_LCID);
            this.HasOptional(t => t.Lookup_MaritalStatus)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.MaritalStatus_LCID);
            this.HasOptional(t => t.Lookup_Occupation)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.Occupation_LCID);
            this.HasOptional(t => t.Lookup_Race)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.Race_LCID);
            this.HasOptional(t => t.Lookup_AddrBk)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.ReligiousBkgd_LCID);
            this.HasRequired(t => t.Lookup_GenderRelationship)
                .WithMany(t => t.AddrBk_Person)
                .HasForeignKey(d => d.Salutation_LCID);
            this.HasOptional(t => t.Lookup_AddrBk1)
                .WithMany(t => t.AddrBk_Person1)
                .HasForeignKey(d => d.Suffix_LCID);

        }
    }
}
