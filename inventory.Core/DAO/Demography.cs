using System;
using System.Collections.Generic;

namespace Peliculas.Core.DAO;

public partial class Demography
{
    public int Iddemography { get; set; }

    public string? DemographyDescription { get; set; }

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();
}
