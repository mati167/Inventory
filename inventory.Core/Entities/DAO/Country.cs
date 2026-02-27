using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Country
{
    public int idcountry { get; set; }

    public string countryname { get; set; } = null!;

    public int? idcontinent { get; set; }

    public virtual Continent? idcontinentNavigation { get; set; }

    public virtual ICollection<Film> idfilms { get; set; } = new List<Film>();

    public virtual ICollection<Person> idpeople { get; set; } = new List<Person>();
}
