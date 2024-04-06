namespace TravelAgencyBackend.Presentation.Models
{
    public class RegisterModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public string? PostalCode { get; set; }
        public string? PassportNumber { get; set; }
        public string? PreferredLanguage { get; set; }
    }
}
