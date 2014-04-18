using HITSW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HITSW.Class
{
    public class Utils
    {
        //General message
        public static String concurrencyMsg = "Data has been modified by another user. Please check the current value showed below. If you want to keep your value just click save. If you want to form with please click Back";
        public static int pageSize = 10;
        public static String errorMsg = "Unable to save changes. Please fill all the necessary fields and try again.";
        public static String norecordfoundMsg = "No Record Found";

        //Message while populating a module
        public static String AddressBook = "Address Book";
        public static String AddrBkOrganizationUnit = "Organization";
        public static String AddrBkOrganizationPhoneFax = "Phone Fax";
        public static String AddrBkOrganizationVirtualAddress = "Virtual Address";
        public static String AddrBkOrganizationLanguage = "Language";
        public static String AddrBkOrganizationOperatingLoacation = "Opearting Location";
        public static String AddrBkOrganizationIndustryAffliation = "Industry Affliation";
        public static String AddrBkOrganizationMemeber = "Member";
        public static String AddrBkOrganizationInterestedProductServices = "Interested Product and Services";
        public static String AddrBkPerson = "Person";
        public static String AddrBkHobby = "Hobby";
        public static String AddrBkLocationGroup = "Location Group";
        public static String AddrBkVeteran = "Veteran";
        public static String AddrBkPhysicalActivity = "Physical Activity";
        public static String AddrBkIdentification = "Identification";
        public static String AddrBkRelation = "Relation";
        public static String AddrBkContactAddress = "Contact Address";
        public static String AddrbkGeographicalGroup = "Geographical group";
        public static String AddrbkGeographicalGroupMember = "Geographical group Member";
        public static String AddrbkDepartment = "Department";

        public static String DataTableOrganizationType = "Organization Type";

        //Filter names
        public static String AB_AddressVerificationStatus = "AddrBk_Address.AddrVerifStatus_LCID";
        public static String AB_GeoGraphicalMemberCountry = "Country";
        public static String AB_GeoGraphicalMemberState = "State/Province";
        public static String AB_GeoGraphicalMemberContinent = "Continent";
        public static String AB_GeoGraphicalMemberCity = "City/Town";
        public static String AB_GeoGraphicalMemberCounty = "County";
        public static String AB_GeoGraphicalMemberMunicipality = "Municipality";
        public static String AB_GeoGraphicalMemberStreet = "Street Address";
        public static String AB_GeoGraphicalMemberPostalCode = "Postal Code";
        public static String AB_Hobby = "Addrbk_Hobby.Hobby_LCID";
        public static String AB_IdentificationVerificationStatus = "AddrBk_Identification.IndentVerifStatus_LCID";
        public static String AB_InterestedProductsServices = "AddrBk_InterestedProductSrvcs.InterestedProdSrvc_LCID";
        public static String AB_OrganizationUnitType = "AddrBk_OrganizationUnit.OUType_LCID";
        public static String AB_OrganizationUnitMemberType = "AddrBk_OrganizationalUnitMember.MemType_LCID";
        public static String AB_OrganizationUnitMemberSuffix = "AddrBk_Person.Suffix_LCID";
        public static String AB_OrganizationUnitMemberReligiousBackground = "AddrBk_Person.ReligiousBkgd_LCID";
        public static String AB_OrganizationUnitMemberSalutation = "AddrBk_Person.Salutation_LCID";
        public static String AB_PhysicalActivity = "AddrBk_PhysicalActivity.PhyActiv_LCID";
        public static String AB_Veteran = "AddrBk_Veteran.Veteran_LCID";
        public static String AB_RelationIndiv = "AddrBk_Relation.RelnToPrimaryIndiv_LCID";
        public static String AB_RelationOrg = "AddrBk_Relation.RelnToPrimaryExtOrg_LCID";
        public static String AB_RelationContactType = "AddrBk_OrganizationUnit.OUType_LCID";
        public static String AB_RelationDept = "AddrBk_Relation.RelnToExtToIntOrg_LCID";

        public static Guid GetLookUpBasisId(bool isOrganization = true)
        {
            HITSWContext db = new HITSWContext();
            Lookup_ContactBasis basis = new Lookup_ContactBasis();

            if (isOrganization)
                basis = db.Lookup_ContactBasis.Where(e => e.Title == "External Organization").First();
            else
                basis = db.Lookup_ContactBasis.Where(e => e.Title == "Individual").First();
                
            db.Dispose();
            return basis.Id;
        }

        public static Guid GetOrganizationLookUpBasisId(bool isExternalOrganization = true)
        {
            HITSWContext db = new HITSWContext();
            Lookup_ContactBasis basis = new Lookup_ContactBasis();

            if (isExternalOrganization)
                basis = db.Lookup_ContactBasis.Where(e => e.Title == "External Organization").First();
            else
                basis = db.Lookup_ContactBasis.Where(e => e.Title == "Internal Organization").First();

            db.Dispose();
            return basis.Id;
        }

        public static Guid GetOrganizationContactBasisId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            AddrBk_OrganizationUnit addrbk_organization_unit = db.AddrBk_OrganizationUnit.Find(id);
            db.Dispose();
            return addrbk_organization_unit.ContactBasis_LCID;
        }

        public static String GetMemberTypeFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_ContactType.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetSalutationFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_GenderRelationship.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetSuffixFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetSuffixFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetGenderFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_Gender.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetGenderFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetMaritialStatusFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_MaritalStatus.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetMaritialStatusFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetRaceFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_Race.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetRaceFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetEthnicityFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_Ethnicity.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetEthnicityFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetReligiousBackgroundFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetReligiousBackgroundFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetHighestEducationFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_EducationalLevel.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetHighestEducationFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetOccupationFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_Occupation.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        internal static string GetOccupationFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetIndustrySectorFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var industry = db.Lookup_IndustrySector.Find(id);
            db.Dispose();
            if (industry == null) return "";
            return industry.Title;
        }

        public static String GetLanguageFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var language = db.Lookup_Language.Find(id);
            db.Dispose();
            if (language == null) return "";
            return language.Title;
        }

        public static String GetPhoneFaxTypeFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var phonefax = db.Lookup_PhoneFaxType.Find(id);
            db.Dispose();
            if (phonefax == null) return "";
            return phonefax.Title;
        }

        internal static string GetPhoneFaxTypeFromId(Guid? nullable)
        {
            throw new NotImplementedException();
        }

        public static String GetVirtualAddressTypeFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var virtualaddress = db.Lookup_AddrType.Find(id);
            db.Dispose();
            if (virtualaddress == null) return "";
            return virtualaddress.Title;
        }

        public static String GetOrganizationTypeTitleFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var organizationType = db.Lookup_ContactType.Find(id);
            db.Dispose();
            if (organizationType == null) return "";
            return organizationType.Title;
        }

        public static String GetInterestedProductServicesTitleFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetHobbyFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetVeteranFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetPhysicalActivityFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_AddrBk.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }

        public static String GetOrganiationFromId(Guid id)
        {
            if (id == null) return "";
            HITSWContext db = new HITSWContext();
            var model = db.AddrBk_OrganizationUnit.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Name;
        }

        public static String GetGeoBasisTitle(Guid id)
        {
            if (id == null) return "";
            HITSWContext db = new HITSWContext();
            AddrBk_GeographicalGroup addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Find(id);
            Lookup_GeographicalBasis model = db.Lookup_GeographicalBasis.Find(addrbk_geographicalgroup.GeoBasis_LCID);
            db.Dispose();
            return model.Title.Trim();
        }

        public static Guid GetGeoBasisId(Guid id)
        {
            if (id == null) return Guid.Empty;
            HITSWContext db = new HITSWContext();
            AddrBk_GeographicalGroup addrbk_geographicalgroup = db.AddrBk_GeographicalGroup.Find(id);
            db.Dispose();
            return addrbk_geographicalgroup.GeoBasis_LCID;
        }

        public static Guid GetAddressTypeId(String title)
        {
            if (title == null || title == "") return Guid.Empty;
            HITSWContext db = new HITSWContext();
            Lookup_AddrType address_type = db.Lookup_AddrType.Where(e => e.ActiveRec == true && e.Title.Equals(title)).First();
            db.Dispose();
            return address_type.Id;
        }
    }
}