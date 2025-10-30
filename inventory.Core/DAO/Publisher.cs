using System;
using System.Collections.Generic;

namespace Inventory.Core.DAO;

public partial class Publisher
{
    public int Idpublisher { get; set; }

    public string? PublisherName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
