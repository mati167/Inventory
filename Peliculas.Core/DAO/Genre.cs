using System;
using System.Collections.Generic;

namespace Peliculas.Core.DAO;

public partial class Genre
{
    public int Idgenre { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Film> Idfilms { get; set; } = new List<Film>();
}
