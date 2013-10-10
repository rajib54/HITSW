using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HITSW.Models.Mapping
{
    public class Lookup_CountryMap : EntityTypeConfiguration<Lookup_Country>
    {
        public Lookup_CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.CntyDescr)
                .HasMaxLength(900);

            this.Property(t => t.Alfa2Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Alfa3Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NumCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

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
            this.ToTable("Lookup_Country");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.CntyDescr).HasColumnName("CntyDescr");
            this.Property(t => t.Alfa2Code).HasColumnName("Alfa2Code");
            this.Property(t => t.Alfa3Code).HasColumnName("Alfa3Code");
            this.Property(t => t.NumCode).HasColumnName("NumCode");
            this.Property(t => t.Continent_LCID).HasColumnName("Continent_LCID");
            this.Property(t => t.Currency_LCID).HasColumnName("Currency_LCID");
            this.Property(t => t.Sort).HasColumnName("Sort");
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
            this.HasRequired(t => t.Lookup_Continent)
                .WithMany(t => t.Lookup_Country)
                .HasForeignKey(d => d.Continent_LCID);
            this.HasOptional(t => t.Lookup_CurrencyCode)
                .WithMany(t => t.Lookup_Country)
                .HasForeignKey(d => d.Currency_LCID);

        }
    }
}
