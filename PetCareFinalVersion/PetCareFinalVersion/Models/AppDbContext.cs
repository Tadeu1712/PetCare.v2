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
                    Name = "Spad",
                    Email = "spadfnc@gmail.com",
                    Password = "secret123",
                    Admin = false
                },
                new User
                 {
                     Id = 3,
                     Name = "PATA",
                     Email = "pata@pata.pt",
                     Password = "secret123",
                     Admin = false
                 },
                new User
                  {
                      Id = 4,
                      Name = "Canil Municipal do Funchal",
                      Email = "CMF@cmf.com",
                      Password = "secret123",
                      Admin = false
                  },
                new User
                   {
                      Id = 5,
                      Name = "Associação Madeira Animal Welfare",
                      Email = "amaw@madeiraanimalwelfare.org",
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
                    Iban = "PT50000702430012359000733",
                    Adress = "R. do Matadouro 10, 9050-100 Funcha",
                    PhoneNumber = "291220852",
                    Description = "Intervenção Activa na Protecção, Bem-estar e Saúde Animal",
                    FoundationDate = DateTime.Now
                },
               new Association
                 {
                    User_id = 3,
                    Id = 2,
                    Iban = "233924194",
                    Adress = "Santa cruz",
                    PhoneNumber = "961133214",
                    Description = "A Associação PATA – Porque os Animais Também se Amam",
                    FoundationDate = DateTime.Now
                 },
               new Association
                {
                    User_id = 4,
                    Id = 3,
                    Iban = "28374659",
                    Adress = "Funchal",
                    PhoneNumber = "291773357",
                    Description = "Canil/Gatil Municipal do Funchal que tem como objectivo principal a recolha e alojamento de animais de companhia que se encontrem abandonados",
                    FoundationDate = DateTime.Now
                 },
               new Association
                {
                    User_id = 5,
                    Id = 4,
                    Iban = "28374659",
                    Adress = "Rua Cidade de Oakland 1 Funchal",
                    PhoneNumber = "966295555",
                    Description = "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados",
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
                  Status = "adoption",
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
                   Status = "adoption",
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
                   Status = "adoption",
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
                   Status = "adoption",
                   Description = "Pelo longo, com orellahs pretas",
                   Association_id = 1,
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
                   Status = "adoption",
                   Description = "Dá-se bem com crianças",
                   Association_id = 2,
                   Image = "",
               },
               new Animal
               {
                    Id = 6,
                    Name = "Grey",
                    Type = "Cão",
                    Breed = "Shar-pei",
                    Age = "4",
                    Weight = 27,
                    Size = "50cm",
                    Status = "adoption",
                    Description = "Gosta de comer comida humida",
                    Association_id = 2,
                    Image = "",
               },
               new Animal
                {
                      Id = 7,
                      Name = "Leão",
                      Type = "Cão",
                      Breed = "Pastor Alemão",
                      Age = "3",
                      Weight = 30,
                      Size = "65CM",
                      Status = "adoption",
                      Description = "Não gosta de gatos",
                      Association_id = 2,
                      Image = "",
                },
               new Animal
                {
                      Id = 8,
                      Name = "Faisca",
                      Type = "Gato",
                      Breed = "Rafeiro",
                      Age = "0.7",
                      Weight = 1,
                      Size = "15cm",
                      Status = "adoption",
                      Description = "Perfeito para apartamentos",
                      Association_id = 2,
                      Image = "",
                },
               new Animal
               {
                   Id = 9,
                   Name = "Toby",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = "2",
                   Weight = 2,
                   Size = "10cm",
                   Status = "adoption",
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "",
               },
               new Animal
               {
                   Id = 10,
                   Name = "Pipsy",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "3",
                   Weight = 10,
                   Size = "20cm",
                   Status = "adoption",
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "",
               },
               new Animal
               {
                   Id = 11,
                   Name = "Quirk",
                   Type = "Gato",
                   Breed = "Sphynx",
                   Age = "2",
                   Weight = 2,
                   Size = "10cm",
                   Status = "adoption",
                   Description = "Não têm pelo",
                   Association_id = 3,
                   Image = "",
               },
               new Animal
               {
                   Id = 12,
                   Name = "Odie",
                   Type = "Cão",
                   Breed = "Doberman",
                   Age = "2",
                   Weight = 40,
                   Size = "72cm",
                   Status = "adoption",
                   Description = "Precisa de muito espaço",
                   Association_id = 3,
                   Image = "",
               },
               new Animal
               {
                   Id = 13,
                   Name = "Barkley",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "4",
                   Weight = 20,
                   Size = "50cm",
                   Status = "adoption",
                   Description = "Gosta de crianças",
                   Association_id = 4,
                   Image = "",
               },
               new Animal
               {
                   Id = 14,
                   Name = "Maverick",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "3",
                   Weight = 10,
                   Size = "15cm",
                   Status = "adoption",
                   Description = "Não gosta de gato",
                   Association_id = 4,
                   Image = "",
               },
               new Animal
               {
                   Id = 15,
                   Name = "Kobe",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "6",
                   Weight = 30,
                   Size = "40cm",
                   Status = "adoption",
                   Description = "Precisa de muito espaço",
                   Association_id = 4,
                   Image = "",
               },
               new Animal
               {
                   Id = 16,
                   Name = "Dorito",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = "0.6",
                   Weight = 2,
                   Size = "20cm",
                   Status = "adoption",
                   Description = "Gosta de Crianças",
                   Association_id = 4,
                   Image = "",
               },
               new Animal
               {
                   Id = 17,
                   Name = "Rage",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = "2",
                   Weight = 5,
                   Size = "70cm",
                   Status = "adoption",
                   Description = "Precisa de muito espaço",
                   Association_id = 5,
                   Image = "",
               },
               new Animal
               {
                   Id = 18,
                   Name = "Tally",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = "1",
                   Weight = 2,
                   Size = "20cm",
                   Status = "adoption",
                   Description = "Não gosta de Crianças",
                   Association_id = 5,
                   Image = "",
               },
               new Animal
               {
                   Id = 19,
                   Name = "Connor",
                   Type = "Cão",
                   Breed = "Labrador",
                   Age = "4",
                   Weight = 30,
                   Size = "50cm",
                   Status = "adoption",
                   Description = "Adora água",
                   Association_id = 5,
                   Image = "",
               },
               new Animal
               {
                   Id = 20,
                   Name = "Gaia",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "0.3",
                   Weight = 1,
                   Size = "10cm",
                   Status = "adoption",
                   Description = "bebe",
                   Association_id = 5,
                   Image = "",
               },
               new Animal
               {
                   Id = 21,
                   Name = "Palmer",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = "0.2",
                   Weight = 1,
                   Size = "5cm",
                   Status = "adoption",
                   Description = "Be",
                   Association_id = 5,
                   Image = "bebe",
               },
               new Animal
               {
                   Id = 22,
                   Name = "Warp",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = "1",
                   Weight = 2,
                   Size = "15cm",
                   Status = "adoption",
                   Description = "Gosta de Cães",
                   Association_id = 5,
                   Image = "",
               },
               new Animal
                {
                    Id = 23,
                    Name = "Rave",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = "0.4",
                    Weight = 1,
                    Size = "8cm",
                    Status = "adoption",
                    Description = "Bom para ter num apartamento",
                    Association_id = 1,
                    Image = "",
                },
               new Animal
               {
                    Id = 24,
                    Name = "Linus",
                    Type = "Cão",
                    Breed = "Rafeiro",
                    Age = "2",
                    Weight = 5,
                    Size = "30cm",
                    Status = "adoption",
                    Description = "Muito energetico",
                    Association_id = 1,
                    Image = "",
               },
               new Animal
               {
                   Id = 25,
                   Name = "Newton",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = "0.9",
                   Weight = 2,
                   Size = "15cm",
                   Status = "adoption",
                   Description = "Gosta de Cães",
                   Association_id = 1,
                   Image = "",
               },
               new Animal
               {
                   Id = 26,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "1",
                   Weight = 10,
                   Size = "10cm",
                   Status = "lost",
                   Description = "Desaparecido",
                   Association_id = 1,
                   Image = "",
               },
               new Animal
               {
                   Id = 27,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "2",
                   Weight = 10,
                   Size = "26cm",
                   Status = "lost",
                   Description = "Desaparecido",
                   Association_id = 1,
                   Image = "",
               },
               new Animal
               {
                    Id = 28,
                    Name = "Shakira",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = "4",
                    Weight = 4,
                    Size = "20cm",
                    Status = "lost",
                    Description = "Desaparecido",
                    Association_id = 1,
                    Image = "",
               },
               new Animal
               {
                   Id = 29,
                   Name = "Bagel",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = "2",
                   Weight = 5,
                   Size = "25cm",
                   Status = "lost",
                   Description = "Desaparecido",
                   Association_id = 2,
                   Image = "",
               },
               new Animal
               {
                    Id = 30,
                    Name = "Bingo",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = "1",
                    Weight = 3,
                    Size = "20cm",
                    Status = "lost",
                    Description = "Desaparecido",
                    Association_id = 2,
                    Image = "",
               },
                new Animal
                {
                    Id = 31,
                    Name = "Basil",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = "2",
                    Weight = 3,
                    Size = "18cm",
                    Status = "lost",
                    Description = "Desaparecido",
                    Association_id = 2,
                    Image = "",
                },
                new Animal
                {
                    Id = 32,
                    Name = "Raisin",
                    Type = "Cão",
                    Breed = "Rafeiro",
                    Age = "3",
                    Weight = 9,
                    Size = "20cm",
                    Status = "lost",
                    Description = "Desaparecido",
                    Association_id = 3,
                    Image = "",
                }
           );

            //Posts
            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Association_id = 1,
                    Title = "SPAD - Ajude a nossa associação a angariar fundos para poder aumentar o espaço",
                    Description = "Ajude a nossa associação a angariar fundos para poder aumentar o espaço",
                    Image = ""
                },
                new Post
                {
                    Id = 2,
                    Association_id = 1,
                    Title = "SPAD - Ajude a nossa associação doando comida de animal",
                    Description = "Ajude a nossa associação doando comida de animal",
                    Image = ""
                },
                new Post
                {
                    Id = 3,
                    Association_id = 1,
                    Title = "SPAD - Estamos a ficar sem espaços disponíveis",
                    Description = "Estamos a ficar sem espaços disponíveis",
                    Image = ""
                },
                new Post
                {
                    Id = 4,
                    Association_id = 2,
                    Title = "PATA - Numero de animais aumenta no ultimo ano",
                    Description = "Numero de animais aumenta no ultimo ano",
                    Image = ""
                },
                new Post
                {
                     Id = 5,
                     Association_id = 2,
                     Title = "PATA - Precisamos de doações de ração",
                     Description = "Precisamos de doações de ração",
                     Image = ""
                },
                new Post
                {
                    Id = 6,
                    Association_id = 3,
                    Title = "Ano record a nivel de adoção",
                    Description = "Ano record a nivel de adoção",
                    Image = ""
                },
                new Post
                {
                    Id = 7,
                    Association_id = 3,
                    Title = "Um obrigado a todos os que ajudaram",
                    Description = "Um obrigado a todos os que ajudaram",
                    Image = ""
                },
                new Post
                {
                     Id = 8,
                     Association_id = 4,
                     Title = "",
                     Description = "Precismos de doações de ração",
                     Image = ""
                },
                new Post
                {
                     Id = 9,
                     Association_id = 4,
                     Title = "Estamos a ficar sem espaços disponíveis",
                     Description = "Estamos a ficar sem espaços disponíveis",
                     Image = ""
                },
                new Post
                {
                     Id = 10,
                     Association_id = 5,
                     Title = "Estamos a ficar sem espaços disponíveis",
                     Description = "Estamos a ficar sem espaços disponíveis",
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
