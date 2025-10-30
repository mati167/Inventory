using System;
using System.Collections.Generic;

namespace Inventory.Core.DAO;

public partial class Book
{
    public int Idbook { get; set; }

    public string? BookName { get; set; }

    public int? Publisher { get; set; }

    public string? OriginalName { get; set; }

    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();

    public virtual ICollection<Literature> Literatures { get; set; } = new List<Literature>();

    public virtual ICollection<Manga> Mangas { get; set; } = new List<Manga>();

    public virtual Publisher? PublisherNavigation { get; set; }
}
