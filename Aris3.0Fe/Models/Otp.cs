using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0Fe.Models
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public DateTime ExpireAt { get; set; } = DateTime.Now - TimeSpan.FromMinutes(30);
        [Required]
        public string Purpose { get; set; } = string.Empty;
        [Required]
        public bool Valid = true;
        public Guid AccounId { get; set; }
        public Account Account { get; set; }
    }
}
