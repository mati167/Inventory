using System;
using System.Collections.Generic;

namespace Inventory.Core.DAO;

public partial class Person
{
    public int Idpersona { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Comic> ComicArtistNavigations { get; set; } = new List<Comic>();

    public virtual ICollection<Comic> ComicAuthorNavigations { get; set; } = new List<Comic>();

    public virtual ICollection<Literature> Literatures { get; set; } = new List<Literature>();

    public virtual ICollection<Manga> MangaArtistNavigations { get; set; } = new List<Manga>();

    public virtual ICollection<Manga> MangaAuthorNavigations { get; set; } = new List<Manga>();

    public virtual ICollection<Country> Idcountries { get; set; } = new List<Country>();

    public virtual ICollection<Film> Idfilms { get; set; } = new List<Film>();

    public virtual ICollection<Film> IdfilmsNavigation { get; set; } = new List<Film>();
}
