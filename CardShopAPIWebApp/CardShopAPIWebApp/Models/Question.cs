using System;
using System.Collections.Generic;

namespace CardShopAPIWebApp.Model;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Question1 { get; set; } = null!;

    public string PersonName { get; set; } = null!;

    public int DeckId { get; set; }

    public virtual CardDeck Deck { get; set; } = null!;
}
