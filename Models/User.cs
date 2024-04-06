using Microsoft.AspNetCore.Identity;

namespace TravelAgencyBackend.Models
{
    public class User : IdentityUser
    {
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Contact Information
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Address Information
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // Additional Information
        public string PassportNumber { get; set; } // For international flights
        public string PreferredLanguage { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public bool IsPreferredCustomer { get; set; }

        // Additional custom properties can be added as needed
    }
}
