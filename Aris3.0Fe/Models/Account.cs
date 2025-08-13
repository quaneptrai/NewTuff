using Aris3._0.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0Fe.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = "Guest";
        public bool status { get; set; } = true;
        public bool AccountStat { get; set; } = true;
        public int SubcriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public ICollection<Otp> Otps = new List<Otp>();
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
