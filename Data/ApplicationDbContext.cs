using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = -1,
            Username = "Osama",
            Password = "123456789", // In production, hash this password!
            Role = "Admin"
        }
    );
    modelBuilder.Entity<Card>().HasData(
        new Card
        {
            Id = 1,
            Name = "Dark Magician",
            Description = "The ultimate wizard in terms of attack and defense.",
            AttackPoints = 2500,
            DefensePoints = 2100,
            ImageUrl = "/images/darkMagician.png"
        },
        new Card
        {
            Id = 2,
            Name = "Blue-Eyes White Dragon",
            Description = "A powerful engine of destruction, virtually invincible.",
            AttackPoints = 3000,
            DefensePoints = 2500,
            ImageUrl = "/images/blueEyes.png"
        },
        new Card
        {
            Id = 3,
            Name = "Red-Eyes Black Dragon",
            Description = "A ferocious dragon with a deadly attack.",
            AttackPoints = 2400,
            DefensePoints = 2000,
            ImageUrl = "/images/redEyes.png"
        }
    );
}


}
