using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Genre
{
    public int idgenre { get; set; }

    public string? description { get; set; }

    public virtual ICollection<Film> idfilms { get; set; } = new List<Film>();
}
