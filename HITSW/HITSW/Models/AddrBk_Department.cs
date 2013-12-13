using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HITSW.Models
{
    public class AddrBk_Department
    {
        public AddrBk_Relation Addrbk_Relation { get; set; }
        public AddrBk_OrganizationUnit Addrbk_OrganizationUnit { get; set; }
        public IEnumerable<Lookup_AddrBk> Lookup_AddrBks { get; set; }
        public IEnumerable<Lookup_ContactType> Lookup_ContactTypes { get; set; }
    }
}