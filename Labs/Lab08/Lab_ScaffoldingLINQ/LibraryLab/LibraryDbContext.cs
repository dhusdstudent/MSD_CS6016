using Lab_ScaffoldingLINQ.Entities;

namespace Lab_ScaffoldingLINQ;
using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {}
    
    public DbSet<Title> titles { get; set; }
    public DbSet<Inventory> inventories { get; set; }
    public DbSet<Patron> patrons { get; set; }
    public DbSet<Checkedout> checkedOuts { get; set; }
    public DbSet<Phone> phones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Title>().HasKey(t => t.isbn);
        modelBuilder.Entity<Inventory>().HasKey(i => i.serial);
        modelBuilder.Entity<Checkedout>().HasKey(c => c.serial);
        modelBuilder.Entity<Patron>().HasKey(p => p.cardNum);
        modelBuilder.Entity<Phone>().HasIndex(n => new { n.cardNum, n.phoneNum});

        modelBuilder.Entity<Inventory>();
    }
    
    
    
}