using Kolok2.Models;

namespace Kolok2.Context;
using Microsoft.EntityFrameworkCore;

public class ContextDB : DbContext
{
    public ContextDB()
    {
        
    }

    public ContextDB(DbContextOptions<ContextDB> options) : base(options)
    {
        
    }

    public DbSet<Item> Items { get; set; }
    
    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<BackpackSlot> BackpackSlots { get; set; }
    
}