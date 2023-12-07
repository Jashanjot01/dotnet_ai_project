using dotnet_ai_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ai_project.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProfileModel> Profiles { get; set; } = default!;
    public DbSet<ChatModel> Chats { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProfileModel>().ToTable("Profiles");
        builder.Entity<ChatModel>().ToTable("Chat");

    }
}
