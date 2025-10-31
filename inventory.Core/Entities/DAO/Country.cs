using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Country
{
    public int Idcountry { get; set; }

    public string CountryName { get; set; } = null!;

    public int? Idcontinent { get; set; }

    public virtual Continent? IdcontinentNavigation { get; set; }

    public virtual ICollection<Film> Idfilms { get; set; } = new List<Film>();

    public virtual ICollection<Person> Idpeople { get; set; } = new List<Person>();
}
