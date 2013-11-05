using HITSW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HITSW.Class
{
    public class Utils
    {
        public static String concurrencyMsg = "Data has been modified by another user. Please check the current value showed below. If you want to keep your value just click save. If you want to form with please click Back";
        public static int pageSize = 10;
        public static String errorMsg = "Unable to save changes. Please fill all the necessary fields and try again.";
        public static String norecordfoundMsg = "No Record Found";

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

        public static Guid GetLookUpBasisId(bool isOrganization = true)
        {
            HITSWContext db = new HITSWContext();
            if (isOrganization)
            {
                var basis = db.Lookup_ContactBasis.Where(e => e.Title == "Organization").First();
                db.Dispose();
                return basis.Id;
            }
            else
            {
                var basis = db.Lookup_ContactBasis.Where(e => e.Title == "Individual").First();
                db.Dispose();
                return basis.Id;
            }
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

        /*public static String GetMemberStatusFromId(Guid id)
        {
            HITSWContext db = new HITSWContext();
            var model = db.Lookup_MemberStatus.Find(id);
            db.Dispose();
            if (model == null) return "";
            return model.Title;
        }*/

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
            return model.Title.Trim();
        }
    }
}