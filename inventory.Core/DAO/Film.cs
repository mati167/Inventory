using System;
using System.Collections.Generic;

namespace Inventory.Core.DAO;

public partial class Film
{
    public int Idfilm { get; set; }

    public string FilmName { get; set; } = null!;

    public short? Year { get; set; }

    public TimeOnly? Duration { get; set; }

    public virtual ICollection<Country> Idcountries { get; set; } = new List<Country>();

    public virtual ICollection<Genre> Idgenres { get; set; } = new List<Genre>();

    public virtual ICollection<Person> Idpeople { get; set; } = new List<Person>();

    public virtual ICollection<Person> IdpeopleNavigation { get; set; } = new List<Person>();
}
