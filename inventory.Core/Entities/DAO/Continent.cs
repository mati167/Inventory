using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Continent
{
    public int idcontinent { get; set; }

    public string continentname { get; set; } = null!;

    public virtual ICollection<Country> countries { get; set; } = new List<Country>();
}
