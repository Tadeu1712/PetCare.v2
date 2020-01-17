using System;
using Microsoft.EntityFrameworkCore;

namespace PetCareFinalVersion.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<LostAnimalPost> LostAnimalPosts { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Animal> Animals { get; set; }
        
    }
}
