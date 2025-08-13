using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0Fe.Models
{
    public class Server
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("server_name")]
        public string ServerName { get; set; }
        public string FilmId { get; set; }
        public Film Film { get; set; }
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
