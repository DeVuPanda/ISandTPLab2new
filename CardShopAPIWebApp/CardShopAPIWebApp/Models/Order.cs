using System;
using System.Collections.Generic;

namespace CardShopAPIWebApp.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string PersonName { get; set; } = null!;

    public string PersonPhone { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public int DeckId { get; set; }

    public virtual CardDeck Deck { get; set; } = null!;
}
