using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Contex;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }
    public DatabaseContext(DbContextOptions options) : base(options) 
    { 
    } 
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
        {
            new Backpack()
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 2
            }
        });
        modelBuilder.Entity<Character>().HasData(new List<Character>
        {
            new Character()
            {
                Id = 1,
                FirstName = "Artur",
                LastName = "Nikytenko",
                CurrentWeight = 10,
                MaxWeight = 150
            }
        });
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
        {
            new CharacterTitle()
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Parse("2024-05-28")
            }
        });
        modelBuilder.Entity<Item>().HasData(new List<Item>
        {
            new Item()
            {
                Id = 1,
                Weight =5,
                Name = "Apple"
            }
        });
        modelBuilder.Entity<Title>().HasData(new List<Title>
        {
            new Title()
            {
                Id = 1,
                Name = "Student"
            }
        });
    }
}