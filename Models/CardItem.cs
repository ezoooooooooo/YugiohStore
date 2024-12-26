public class CartItem
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public Card Card { get; set; } = null!; // Navigation property
    public int UserId { get; set; }
    public int Quantity { get; set; }
}
