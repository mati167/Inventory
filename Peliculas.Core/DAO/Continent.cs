using System;
using System.Collections.Generic;

namespace Peliculas.Core.DAO;

public partial class Continent
{
    public int Idcontinent { get; set; }

    public string ContinentName { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
