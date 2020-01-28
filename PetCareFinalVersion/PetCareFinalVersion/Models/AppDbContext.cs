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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            //User
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "Admin@admin.com",
                    Password = "secret123",
                    Admin = true                 
                },
                new User
                {
                    Id = 2,
                    Name = "Artur",
                    Email = "Artur@artur.com",
                    Password = "secret123",
                    Admin = false
                },
                new User
                 {
                     Id = 3,
                     Name = "Ricardo",
                     Email = "Ricardo@ricardo.com",
                     Password = "secret123",
                     Admin = false
                 },
                new User
                  {
                      Id = 4,
                      Name = "Ruben",
                      Email = "Ruben@ruben.com",
                      Password = "secret123",
                      Admin = false
                  },
                new User
                   {
                      Id = 5,
                      Name = "Tadeu",
                      Email = "Tadeu@tadeu.com",
                      Password = "secret123",
                      Admin = false
                   }
            );

            //Associação
            builder.Entity<Association>().HasData(
               new Association
                {
                    User_id = 2, 
                    Id = 1,
                    Iban = "233924194",
                    Adress = "Rua dos milagres",
                    PhoneNumber = "291876234",
                    Description = "Spad",
                    FoundationDate = DateTime.Now
                },
               new Association
                 {
                    User_id = 3,
                    Id = 2,
                    Iban = "233924194",
                    Adress = "Rua dos milagres",
                    PhoneNumber = "291876234",
                    Description = "Spad",
                    FoundationDate = DateTime.Now
                 },
               new Association
                {
                    User_id = 4,
                    Id = 3,
                    Iban = "28374659",
                    Adress = "Rua dos Vinagres",
                    PhoneNumber = "291745637",
                    Description = "Ajuda do Animal",
                    FoundationDate = DateTime.Now
                 },
               new Association
                {
                    User_id = 5,
                    Id = 4,
                    Iban = "28374659",
                    Adress = "Rua dos Vinagres",
                    PhoneNumber = "291745637",
                    Description = "Ajuda do Animal",
                    FoundationDate = DateTime.Now
                }
            );

            //Animais
            builder.Entity<Animal>().HasData(
               new Animal
               {
                  Id = 1,
                  Name = "Napoleão",
                  Type = "Gato",
                  Breed = "Rafeiro",
                  Age = "1",
                  Weight = 2,
                  Size = "30cm",
                  Status =  "Nem sei",
                  Description = "Mancha no centro da testa",
                  Association_id = 1,
                  Image = "",   

               },
               new Animal
               {
                   Id = 2,
                   Name = "Bolinhas",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "2",
                   Weight = 150,
                   Size = "180 metros",
                   Status = "Nem sei",
                   Description = "Cão de pequeno porte",
                   Association_id = 1,
                   Image = "",

               },
               new Animal
               {
                   Id = 3,
                   Name = "Bob",
                   Type = "Cão",
                   Breed = "Boxer",
                   Age = "4",
                   Weight = 25,
                   Size = "1.20m",
                   Status = "nem sei",
                   Description = "Muita energia",
                   Association_id = 1,
                   Image = "",

               },
               new Animal
               {
                   Id = 4,
                   Name = "Belinha",
                   Type = "Gato",
                   Breed = "Ragdoll",
                   Age = "3",
                   Weight = 2,
                   Size = "25 cm",
                   Status = "Nem sei",
                   Description = "Pelo longo, com orellahs pretas",
                   Association_id = 2,
                   Image = "",

               },
               new Animal
               {
                   Id = 5,
                   Name = "Duke",
                   Type = "Cão",
                   Breed = "Pastor Alemão",
                   Age = "3",
                   Weight = 18,
                   Size = "50cm",
                   Status = "Nem sei",
                   Description = "Dá-se bem com crianças",
                   Association_id = 2,
                   Image = "",
               }
           );

            //Posts
            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Association_id = 1,
                    Title = "Bob procura nova casa ",
                    Description = "Dá-se bem com crianças, cão muito energetico",
                    Image = ""
                },
                new Post
                {
                    Id = 2,
                    Association_id = 1,
                    Title = "Ajude-nos a encontrar novo dono para Napoleão.",
                    Description = "Napoleão é um gato muito amigavel",
                    Image = ""
                },
                new Post
                {
                    Id = 3,
                    Association_id = 2,
                    Title = "Ajude a nossa associação a angariar fundos para poder aumentar o espaço",
                    Description = "Ajude a nossa associação a angariar fundos para poder aumentar o espaço",
                    Image = ""
                },
                new Post
                {
                    Id = 4,
                    Association_id = 2,
                    Title = "Ajude a nossa associação doando comida de animal",
                    Description = "Ajude a nossa associação doando comida de animal",
                    Image = ""
                }
           );

            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Association_id = 1,
                    Title = "",
                    Description = "",
                    Location = "",
                    DateInit =  DateTime.Now ,
                    DateEnd = DateTime.Now,
                    Type = "",
                    Price = 2,
                    Image = "wasd"
                }
                );
        }

    }
}
