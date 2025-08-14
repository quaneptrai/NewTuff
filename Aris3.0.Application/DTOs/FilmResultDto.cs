using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0.Application.DTOs
{
    public class FilmResultDto
    {
        public string FilmName { get; set; }
        public List<string> CategoryTemps { get; set; } = new List<string>();
    }
}
