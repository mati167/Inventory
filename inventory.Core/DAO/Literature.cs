using System;
using System.Collections.Generic;

namespace Inventory.Core.DAO;

public partial class Literature
{
    public int Idliterature { get; set; }

    public int Idbook { get; set; }

    public int Author { get; set; }

    public int Isbn { get; set; }

    public int BookOrder { get; set; }

    public DateOnly? PublishYear { get; set; }

    public virtual Person AuthorNavigation { get; set; } = null!;

    public virtual Book IdbookNavigation { get; set; } = null!;
}
