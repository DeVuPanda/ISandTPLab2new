using System;
using System.Collections.Generic;

namespace CardShopAPIWebApp.Model;

public partial class Review
{
    public int ReviewId { get; set; }

    public string Review1 { get; set; } = null!;

    public int DeckId { get; set; }

    public virtual CardDeck Deck { get; set; } = null!;
}
