using Inventory.Core.DTOs.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.DTOs.Film
{
    public class FilmDto
    {
        public int Idfilm { get; set; }
        public string FilmName { get; set; } = null!;
        public short? Year { get; set; }
        public TimeOnly? Duration { get; set; }
        public List<idDescriptionDTO> Directed { get; set; } = new();
        public List<idDescriptionDTO> Countries { get; set; } = new();
        public List<idDescriptionDTO> Genres { get; set; } = new();
    }
}
