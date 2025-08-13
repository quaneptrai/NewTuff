﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0Fe.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Filename { get; set; }

        public string LinkEmbed { get; set; }

        public string LinkM3U8 { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }
    }
}
