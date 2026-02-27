using System;
using System.Collections.Generic;

namespace Inventory.Core.Entities.DAO;

public partial class Film
{
    public int idfilm { get; set; }

    public string filmname { get; set; } = null!;

    public short? year { get; set; }

    public TimeOnly? duration { get; set; }

    public string? imdbid { get; set; }

    public virtual ICollection<Country> idcountries { get; set; } = new List<Country>();

    public virtual ICollection<Genre> idgenres { get; set; } = new List<Genre>();

    public virtual ICollection<Person> iddirected { get; set; } = new List<Person>();

    public virtual ICollection<Person> idacted { get; set; } = new List<Person>();
}
