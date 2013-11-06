using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HITSW.Models
{
    public partial class AddrBk_Identification
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage="Identification type is required")]
        public System.Guid IdentType_LCID { get; set; }

        public System.Guid ContactBasis_LCID { get; set; }
        public Nullable<System.Guid> OrgID { get; set; }
        public Nullable<System.Guid> IndivID { get; set; }

        [Required(ErrorMessage="Identification number is required")]
        public string Title { get; set; }

        public string LegalFirstN { get; set; }
        public string LegalLastN { get; set; }
        public string LegalMiddleN { get; set; }
        public Nullable<System.DateTime> IssuedDt { get; set; }
        public Nullable<System.DateTime> ExpDt { get; set; }
        public Nullable<System.Guid> IdentificationIssuerID { get; set; }

        [Required(ErrorMessage="Verification type is required")]
        public System.Guid VerificationType_LCID { get; set; }

        public Nullable<System.Guid> VerificDocID { get; set; }

        [Required(ErrorMessage = "Verification status is required")]
        public System.Guid IndentVerifStatus_LCID { get; set; }

        [Required]
        public System.DateTimeOffset EffDt { get; set; }

        public Nullable<System.DateTimeOffset> IneffDt { get; set; }
        public string Cmmt { get; set; }

        [Required]
        public bool ActiveRec { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        public System.DateTimeOffset CreatedDt { get; set; }

        public string LastUpdatedFrom { get; set; }
        public string LastUpdatedBy { get; set; }

        [Required]
        public System.DateTimeOffset LastUpdatedDt { get; set; }

        [Timestamp]
        public byte[] Concurrency { get; set; }

        public Lookup_ContactBasis Lookup_ContactBasis { get; set; }
        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit { get; set; }
        public Lookup_IdentificationType Lookup_IdentificationType { get; set; }
        public Lookup_Status Lookup_Status { get; set; }
        public AddrBk_Person AddrBk_Person { get; set; }
        public AddrBk_OrganizationUnit AddrBk_OrganizationUnit1 { get; set; }
        public Lookup_VerificationType Lookup_VerificationType { get; set; }
    }
}
