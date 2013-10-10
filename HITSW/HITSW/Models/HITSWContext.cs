using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HITSW.Models.Mapping;

namespace HITSW.Models
{
    public partial class HITSWContext : DbContext
    {
        static HITSWContext()
        {
            Database.SetInitializer<HITSWContext>(null);
        }

        public HITSWContext()
            : base("Name=HITSWContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AddrBk_Address> AddrBk_Address { get; set; }
        public DbSet<AddrBk_ContactAddr> AddrBk_ContactAddr { get; set; }
        public DbSet<AddrBk_ContactTimezone> AddrBk_ContactTimezone { get; set; }
        public DbSet<AddrBk_GeographicalCollection> AddrBk_GeographicalCollection { get; set; }
        public DbSet<AddrBk_GeographicalGroup> AddrBk_GeographicalGroup { get; set; }
        public DbSet<AddrBk_GeographicalGroupMember> AddrBk_GeographicalGroupMember { get; set; }
        public DbSet<Addrbk_Hobby> Addrbk_Hobby { get; set; }
        public DbSet<AddrBk_Identification> AddrBk_Identification { get; set; }
        public DbSet<AddrBk_IndustryAffliation> AddrBk_IndustryAffliation { get; set; }
        public DbSet<AddrBk_InterestedProductSrvcs> AddrBk_InterestedProductSrvcs { get; set; }
        public DbSet<AddrBk_Language> AddrBk_Language { get; set; }
        public DbSet<AddrBk_OrganizationalUnitMember> AddrBk_OrganizationalUnitMember { get; set; }
        public DbSet<AddrBk_OrganizationUnit> AddrBk_OrganizationUnit { get; set; }
        public DbSet<AddrBk_Person> AddrBk_Person { get; set; }
        public DbSet<AddrBk_PhoneFax> AddrBk_PhoneFax { get; set; }
        public DbSet<AddrBk_PhysicalActivity> AddrBk_PhysicalActivity { get; set; }
        public DbSet<AddrBk_Prospect> AddrBk_Prospect { get; set; }
        public DbSet<AddrBk_Relation> AddrBk_Relation { get; set; }
        public DbSet<AddrBk_StateOrProvinceLocality> AddrBk_StateOrProvinceLocality { get; set; }
        public DbSet<AddrBk_Veteran> AddrBk_Veteran { get; set; }
        public DbSet<AddrBk_VirtualAddress> AddrBk_VirtualAddress { get; set; }
        public DbSet<Calendar_Events> Calendar_Events { get; set; }
        public DbSet<Calendar_EventSubscriber> Calendar_EventSubscriber { get; set; }
        public DbSet<Calendar_Notification> Calendar_Notification { get; set; }
        public DbSet<Calendar_ResourceCalendar> Calendar_ResourceCalendar { get; set; }
        public DbSet<Lookup_AddrBk> Lookup_AddrBk { get; set; }
        public DbSet<Lookup_AddrType> Lookup_AddrType { get; set; }
        public DbSet<Lookup_Calendar> Lookup_Calendar { get; set; }
        public DbSet<Lookup_ContactBasis> Lookup_ContactBasis { get; set; }
        public DbSet<Lookup_ContactType> Lookup_ContactType { get; set; }
        public DbSet<Lookup_Continent> Lookup_Continent { get; set; }
        public DbSet<Lookup_Country> Lookup_Country { get; set; }
        public DbSet<Lookup_CurrencyCode> Lookup_CurrencyCode { get; set; }
        public DbSet<Lookup_EducationalLevel> Lookup_EducationalLevel { get; set; }
        public DbSet<Lookup_Ethnicity> Lookup_Ethnicity { get; set; }
        public DbSet<Lookup_Gender> Lookup_Gender { get; set; }
        public DbSet<Lookup_GenderRelationship> Lookup_GenderRelationship { get; set; }
        public DbSet<Lookup_GeographicalBasis> Lookup_GeographicalBasis { get; set; }
        public DbSet<Lookup_IdentificationType> Lookup_IdentificationType { get; set; }
        public DbSet<Lookup_IndustrySector> Lookup_IndustrySector { get; set; }
        public DbSet<Lookup_Language> Lookup_Language { get; set; }
        public DbSet<Lookup_LeadSourceType> Lookup_LeadSourceType { get; set; }
        public DbSet<Lookup_MaritalStatus> Lookup_MaritalStatus { get; set; }
        public DbSet<Lookup_Occupation> Lookup_Occupation { get; set; }
        public DbSet<Lookup_PhoneFaxType> Lookup_PhoneFaxType { get; set; }
        public DbSet<Lookup_Race> Lookup_Race { get; set; }
        public DbSet<Lookup_StateOrProvTimezone> Lookup_StateOrProvTimezone { get; set; }
        public DbSet<Lookup_StateProvince> Lookup_StateProvince { get; set; }
        public DbSet<Lookup_Status> Lookup_Status { get; set; }
        public DbSet<Lookup_Timezone> Lookup_Timezone { get; set; }
        public DbSet<Lookup_VerificationType> Lookup_VerificationType { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddrBk_AddressMap());
            modelBuilder.Configurations.Add(new AddrBk_ContactAddrMap());
            modelBuilder.Configurations.Add(new AddrBk_ContactTimezoneMap());
            modelBuilder.Configurations.Add(new AddrBk_GeographicalCollectionMap());
            modelBuilder.Configurations.Add(new AddrBk_GeographicalGroupMap());
            modelBuilder.Configurations.Add(new AddrBk_GeographicalGroupMemberMap());
            modelBuilder.Configurations.Add(new Addrbk_HobbyMap());
            modelBuilder.Configurations.Add(new AddrBk_IdentificationMap());
            modelBuilder.Configurations.Add(new AddrBk_IndustryAffliationMap());
            modelBuilder.Configurations.Add(new AddrBk_InterestedProductSrvcsMap());
            modelBuilder.Configurations.Add(new AddrBk_LanguageMap());
            modelBuilder.Configurations.Add(new AddrBk_OrganizationalUnitMemberMap());
            modelBuilder.Configurations.Add(new AddrBk_OrganizationUnitMap());
            modelBuilder.Configurations.Add(new AddrBk_PersonMap());
            modelBuilder.Configurations.Add(new AddrBk_PhoneFaxMap());
            modelBuilder.Configurations.Add(new AddrBk_PhysicalActivityMap());
            modelBuilder.Configurations.Add(new AddrBk_ProspectMap());
            modelBuilder.Configurations.Add(new AddrBk_RelationMap());
            modelBuilder.Configurations.Add(new AddrBk_StateOrProvinceLocalityMap());
            modelBuilder.Configurations.Add(new AddrBk_VeteranMap());
            modelBuilder.Configurations.Add(new AddrBk_VirtualAddressMap());
            modelBuilder.Configurations.Add(new Calendar_EventsMap());
            modelBuilder.Configurations.Add(new Calendar_EventSubscriberMap());
            modelBuilder.Configurations.Add(new Calendar_NotificationMap());
            modelBuilder.Configurations.Add(new Calendar_ResourceCalendarMap());
            modelBuilder.Configurations.Add(new Lookup_AddrBkMap());
            modelBuilder.Configurations.Add(new Lookup_AddrTypeMap());
            modelBuilder.Configurations.Add(new Lookup_CalendarMap());
            modelBuilder.Configurations.Add(new Lookup_ContactBasisMap());
            modelBuilder.Configurations.Add(new Lookup_ContactTypeMap());
            modelBuilder.Configurations.Add(new Lookup_ContinentMap());
            modelBuilder.Configurations.Add(new Lookup_CountryMap());
            modelBuilder.Configurations.Add(new Lookup_CurrencyCodeMap());
            modelBuilder.Configurations.Add(new Lookup_EducationalLevelMap());
            modelBuilder.Configurations.Add(new Lookup_EthnicityMap());
            modelBuilder.Configurations.Add(new Lookup_GenderMap());
            modelBuilder.Configurations.Add(new Lookup_GenderRelationshipMap());
            modelBuilder.Configurations.Add(new Lookup_GeographicalBasisMap());
            modelBuilder.Configurations.Add(new Lookup_IdentificationTypeMap());
            modelBuilder.Configurations.Add(new Lookup_IndustrySectorMap());
            modelBuilder.Configurations.Add(new Lookup_LanguageMap());
            modelBuilder.Configurations.Add(new Lookup_LeadSourceTypeMap());
            modelBuilder.Configurations.Add(new Lookup_MaritalStatusMap());
            modelBuilder.Configurations.Add(new Lookup_OccupationMap());
            modelBuilder.Configurations.Add(new Lookup_PhoneFaxTypeMap());
            modelBuilder.Configurations.Add(new Lookup_RaceMap());
            modelBuilder.Configurations.Add(new Lookup_StateOrProvTimezoneMap());
            modelBuilder.Configurations.Add(new Lookup_StateProvinceMap());
            modelBuilder.Configurations.Add(new Lookup_StatusMap());
            modelBuilder.Configurations.Add(new Lookup_TimezoneMap());
            modelBuilder.Configurations.Add(new Lookup_VerificationTypeMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
