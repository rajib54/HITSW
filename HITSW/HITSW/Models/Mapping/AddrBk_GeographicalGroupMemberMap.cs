using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class AddrBk_GeographicalGroupMemberMap : EntityTypeConfiguration<AddrBk_GeographicalGroupMember>
    {
        public AddrBk_GeographicalGroupMemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PostalCode)
                .IsFixedLength()
                .HasMaxLength(10);

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
            this.ToTable("AddrBk_GeographicalGroupMember");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GeographicalGroup_LCID).HasColumnName("GeographicalGroup_LCID");
            this.Property(t => t.Continent_LCID).HasColumnName("Continent_LCID");
            this.Property(t => t.Country_LCID).HasColumnName("Country_LCID");
            this.Property(t => t.StateOrProv_LCID).HasColumnName("StateOrProv_LCID");
            this.Property(t => t.StateOrProvLocalityID).HasColumnName("StateOrProvLocalityID");
            this.Property(t => t.AddrID).HasColumnName("AddrID");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
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
            this.HasOptional(t => t.AddrBk_Address)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.AddrID);
            this.HasRequired(t => t.AddrBk_GeographicalGroup)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.GeographicalGroup_LCID);
            this.HasOptional(t => t.Lookup_Continent)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.Continent_LCID);
            this.HasOptional(t => t.Lookup_Country)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.Country_LCID);
            this.HasOptional(t => t.Lookup_StateProvince)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.StateOrProv_LCID);
            this.HasOptional(t => t.AddrBk_StateOrProvinceLocality)
                .WithMany(t => t.AddrBk_GeographicalGroupMember)
                .HasForeignKey(d => d.StateOrProvLocalityID);

        }
    }
}
