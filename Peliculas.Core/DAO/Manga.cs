using System;
using System.Collections.Generic;

namespace Peliculas.Core.DAO;

public partial class Manga
{
    public int Idmanga { get; set; }

    public int Idbook { get; set; }

    public int Author { get; set; }

    public int? Artist { get; set; }

    public int Demography { get; set; }

    public int Isbn { get; set; }

    public int BookOrder { get; set; }

    public DateOnly? PublishYear { get; set; }

    public DateOnly? AquisitionDate { get; set; }

    public short? Price { get; set; }

    public int? Size { get; set; }

    public virtual Person? ArtistNavigation { get; set; }

    public virtual Person AuthorNavigation { get; set; } = null!;

    public virtual Demography DemographyNavigation { get; set; } = null!;

    public virtual Book IdbookNavigation { get; set; } = null!;

    public virtual Size? SizeNavigation { get; set; }
}
