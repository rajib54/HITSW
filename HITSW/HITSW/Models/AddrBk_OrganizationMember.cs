using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HITSW.Models
{
    public class AddrBk_OrganizationMember
    {
        public AddrBk_OrganizationalUnitMember OrganizationUnitMember { get; set; }
        public AddrBk_Person Person { get; set; }
        public IEnumerable<Lookup_ContactType> LookUp_MemberTypes { get; set; }
        public IEnumerable<Lookup_GenderRelationship> Lookup_Salutations { get; set; }
        public IEnumerable<Lookup_AddrBk> Lookup_Suffixes { get; set; }
        public IEnumerable<Lookup_Gender> Lookup_Genders { get; set; }
        public IEnumerable<Lookup_MaritalStatus> Lookup_MaritialStatuses { get; set; }
        public IEnumerable<Lookup_Race> Lookup_Races { get; set; }
        public IEnumerable<Lookup_Ethnicity> Lookup_Ethnicities { get; set; }
        public IEnumerable<Lookup_AddrBk> Lookup_ReligiousBackgrounds { get; set; }
        public IEnumerable<Lookup_EducationalLevel> Lookup_EducationLevels { get; set; }
        public IEnumerable<Lookup_Occupation> Lookup_Occupations { get; set; }
    }
}