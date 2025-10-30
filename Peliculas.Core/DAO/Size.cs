using System;
using System.Collections.Generic;

namespace Peliculas.Core.DAO;

public partial class Size
{
    public int Idsize { get; set; }

    public string? SizeName { get; set; }

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();
}
