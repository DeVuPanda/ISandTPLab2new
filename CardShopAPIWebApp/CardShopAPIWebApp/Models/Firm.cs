using System;
using System.Collections.Generic;

namespace CardShopAPIWebApp.Model;

public partial class Firm
{
    public int FirmId { get; set; }

    public string FirmName { get; set; } = null!;

    public virtual ICollection<CardDeck> CardDecks { get; set; } = new List<CardDeck>();
}
