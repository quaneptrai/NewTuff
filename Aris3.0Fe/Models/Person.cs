using System.ComponentModel.DataAnnotations;

namespace Aris3._0Fe.Models
{
    public class Person
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = "Guest";
        [Required]
        public string PhoneNumber { get; set; }
        = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; }
        = string.Empty;
        [Required]
        public string Country { get; set; } = "Vn";
        [Required]
        public string Region { get; set; } = "Sea";
        public bool AccountStat { get; set; } = true;
        public Account Account { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
