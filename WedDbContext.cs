// WedDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using wedding_api.Models;
public class WedDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<WeddingProfile> Weddings { get; set; }
    public DbSet<GeneralMediaUploading> GenMedia { get; set; }
    public DbSet<StoryReaction> Reactions { get; set; }
    public DbSet<AdminStory> AdminStories { get; set; }

    public WedDbContext(DbContextOptions<WedDbContext> options)
        : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Admin has 1 WeddingProfile (no cascade)
        modelBuilder.Entity<Admin>()
            .HasOne(a => a.WeddingProfile)
            .WithOne(w => w.Admin)
            .HasForeignKey<WeddingProfile>(w => w.AdminId)
            .OnDelete(DeleteBehavior.Restrict); // No cascade

        // AdminStory -> Admin (Restrict cascade here too)
        modelBuilder.Entity<AdminStory>()
            .HasOne(s => s.Admin)
            .WithMany()
            .HasForeignKey(s => s.AdminId)
            .OnDelete(DeleteBehavior.Restrict);  // <--- make sure this is Restrict

        // AdminStory -> WeddingProfile (Cascade delete allowed)
        modelBuilder.Entity<AdminStory>()
            .HasOne(s => s.Wedding)
            .WithMany(w => w.Stories)
            .HasForeignKey(s => s.WeddingId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade OK here
    }



}