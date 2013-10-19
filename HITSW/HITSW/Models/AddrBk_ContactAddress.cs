using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HITSW.Models
{
    public class AddrBk_ContactAddress
    {
        public AddrBk_ContactAddr AddrBk_ContactAddr { get; set; }
        public AddrBk_Address AddrBk_Address { get; set; }
        public IEnumerable<Lookup_AddrType> Lookup_AddrTypes { get; set; }
        public IEnumerable<Lookup_Country> Lookup_Countries { get; set; }
        public IEnumerable<Lookup_Status> Lookup_VerificationStatuses { get; set; }
    }
}