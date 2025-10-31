using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Size
{
    public int Idsize { get; set; }

    public string? SizeName { get; set; }

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();
}
