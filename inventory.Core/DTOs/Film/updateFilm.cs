using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.DTOs.Film
{
    public class updateFilm
    {
        public int Idfilm { get; set; }
        public string FilmName { get; set; } = null!;
        public short? Year { get; set; }
        public TimeOnly? Duration { get; set; }

        public List<int> CountryIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
        public List<int> DirectedIds { get; set; } = new();
        public List<int> ActedIds { get; set; } = new();
    }
}
