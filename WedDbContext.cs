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
}