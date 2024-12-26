public class Card
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int AttackPoints { get; set; }
    public int DefensePoints { get; set; }
    public required string ImageUrl { get; set; }
}
