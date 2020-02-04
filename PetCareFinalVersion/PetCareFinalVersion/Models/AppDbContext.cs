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
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("secret123"),
                    Admin = true                 
                },
                new User
                {
                    Id = 2,
                    Name = "Spad",
                    Email = "spadfnc@gmail.com",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("secret123"),
                    Admin = false
                },
                new User
                 {
                     Id = 3,
                     Name = "PATA",
                     Email = "pata@pata.pt",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("secret123"),
                    Admin = false
                 },
                new User
                  {
                      Id = 4,
                      Name = "Canil Municipal do Funchal",
                      Email = "CMF@cmf.com",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("secret123"),
                    Admin = false
                  },
                new User
                   {
                      Id = 5,
                      Name = "Associação Madeira Animal Welfare",
                      Email = "amaw@madeiraanimalwelfare.org",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("secret123"),
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
                    Adress = "R. do Matadouro 10, 9050-100 Funchal",
                    PhoneNumber = "291220852",
                    Description = "Intervenção Activa na Protecção, Bem-estar e Saúde Animal",
                    FoundationDate = new DateTime(1897, 6, 30)
               },
               new Association
                 {
                    User_id = 3,
                    Id = 2,
                    Iban = "233924194",
                    Adress = "Santa cruz",
                    PhoneNumber = "961133214",
                    Description = "A Associação PATA – Porque os Animais Também se Amam",
                    FoundationDate = new DateTime(2006, 5, 8)
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
                    Iban = "PT50 0007 0000 0008 4526 8682 3",
                    Adress = "Rua Cidade de Oakland 1 Funchal",
                    PhoneNumber = "966295555",
                    Description = "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados",
                    FoundationDate = new DateTime(2012, 1, 2)
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
                  Age = new DateTime(2018,10,2).ToString(),
                  Weight = 2,
                  Size = "30cm",
                  Status = "Para adoção",
                  Description = "Mancha no centro da testa",
                  Association_id = 1,
                  Image = "/api/animal/img/Napoleão_animal_1.jpg",   

               },
               new Animal
               {
                   Id = 2,
                   Name = "Bolinhas",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 1, 20).ToString(),
                   Weight = 150,
                   Size = "180 metros",
                   Status = "Para adoção",
                   Description = "Cão de pequeno porte",
                   Association_id = 1,
                   Image = "/api/animal/img/Bolinhas_animal_2.jpg",

               },
               new Animal
               {
                   Id = 3,
                   Name = "Bob",
                   Type = "Cão",
                   Breed = "Boxer",
                   Age = new DateTime(2016,5, 9).ToString(),
                   Weight = 25,
                   Size = "1.20m",
                   Status = "Para adoção",
                   Description = "Muita energia",
                   Association_id = 1,
                   Image = "/api/animal/img/Bob_animal_3.jpg",

               },
               new Animal
               {
                   Id = 4,
                   Name = "Belinha",
                   Type = "Gato",
                   Breed = "Ragdoll",
                   Age = new DateTime(2017,8, 17).ToString(),
                   Weight = 2,
                   Size = "25 cm",
                   Status = "Para adoção",
                   Description = "Pelo longo, com orellahs pretas",
                   Association_id = 1,
                   Image = "/api/animal/img/Belinha_animal_4.jpg",

               },
               new Animal
               {
                   Id = 5,
                   Name = "Duke",
                   Type = "Cão",
                   Breed = "Pastor Alemão",
                   Age = new DateTime(2017,4,10).ToString(),
                   Weight = 18,
                   Size = "50cm",
                   Status = "Para adoção",
                   Description = "Dá-se bem com crianças",
                   Association_id = 2,
                   Image = "/api/animal/img/Duke_animal_5.jpg",
               },
               new Animal
               {
                    Id = 6,
                    Name = "Grey",
                    Type = "Cão",
                    Breed = "Shar-pei",
                    Age = new DateTime(2016,12,3).ToString(),
                    Weight = 27,
                    Size = "50cm",
                    Status = "Para adoção",
                    Description = "Gosta de comer comida humida",
                    Association_id = 2,
                    Image = "/api/animal/img/Grey_animal_6.jpg",
               },
               new Animal
                {
                      Id = 7,
                      Name = "Leão",
                      Type = "Cão",
                      Breed = "Pastor Alemão",
                      Age = new DateTime(2019,11,10).ToString(),
                      Weight = 2,
                      Size = "15cm",
                      Status = "Para adoção",
                      Description = "Não gosta de gatos",
                      Association_id = 2,
                      Image = "/api/animal/img/Leão_animal_7.jpg",
                },
               new Animal
                {
                      Id = 8,
                      Name = "Faisca",
                      Type = "Gato",
                      Breed = "Rafeiro",
                      Age = new DateTime(2019,11,27).ToString(),
                      Weight = 1,
                      Size = "15cm",
                      Status = "Para adoção",
                      Description = "Perfeito para apartamentos",
                      Association_id = 2,
                      Image = "/api/animal/img/Faisca_animal_8.jpg",
                },
               new Animal
               {
                   Id = 9,
                   Name = "Toby",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 5, 21).ToString(),
                   Weight = 2,
                   Size = "10cm",
                   Status = "Para adoção",
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "/api/animal/img/Toby_animal_9.jpg",
               },
               new Animal
               {
                   Id = 10,
                   Name = "Pipsy",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 3, 10).ToString(),
                   Weight = 10,
                   Size = "20cm",
                   Status = "Para adoção",
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "/api/animal/img/Pipsy_animal_10.jpg",
               },
               new Animal
               {
                   Id = 11,
                   Name = "Quirk",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 5, 11).ToString(),
                   Weight = 2,
                   Size = "10cm",
                   Status = "Para adoção",
                   Description = "Não têm pelo",
                   Association_id = 3,
                   Image = "/api/animal/img/Quirk_animal_11.jpg",
               },
               new Animal
               {
                   Id = 12,
                   Name = "Odie",
                   Type = "Cão",
                   Breed = "Doberman",
                   Age = new DateTime(2018, 1, 20).ToString(),
                   Weight = 40,
                   Size = "72cm",
                   Status = "Para adoção",
                   Description = "Precisa de muito espaço",
                   Association_id = 3,
                   Image = "/api/animal/img/Odie_animal_12.jpg",
               },
               new Animal
               {
                   Id = 13,
                   Name = "Barkley",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2016, 2, 10).ToString(),
                   Weight = 20,
                   Size = "50cm",
                   Status = "Para adoção",
                   Description = "Gosta de crianças",
                   Association_id = 4,
                   Image = "/api/animal/img/Barkley_animal_13.jpg",
               },
               new Animal
               {
                   Id = 14,
                   Name = "Maverick",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 1, 15).ToString(),
                   Weight = 10,
                   Size = "15cm",
                   Status = "Para adoção",
                   Description = "Não gosta de gato",
                   Association_id = 4,
                   Image = "/api/animal/img/Maverick_animal_14.jpg",
               },
               new Animal
               {
                   Id = 15,
                   Name = "Kobe",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2015, 2, 7).ToString(),
                   Weight = 30,
                   Size = "40cm",
                   Status = "Para adoção",
                   Description = "Precisa de muito espaço",
                   Association_id = 4,
                   Image = "/api/animal/img/Kobe_animal_15.jpg",
               },
               new Animal
               {
                   Id = 16,
                   Name = "Dorito",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 10, 2).ToString(),
                   Weight = 2,
                   Size = "20cm",
                   Status = "Para adoção",
                   Description = "Gosta de Crianças",
                   Association_id = 4,
                   Image = "/api/animal/img/Dorito_animal_16.jpg",
               },
               new Animal
               {
                   Id = 17,
                   Name = "Rage",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = new DateTime(2017, 12, 29).ToString(),
                   Weight = 5,
                   Size = "70cm",
                   Status = "Para adoção",
                   Description = "Precisa de muito espaço",
                   Association_id = 1,
                   Image = "/api/animal/img/Rage_animal_17.jpg",
               },
               new Animal
               {
                   Id = 18,
                   Name = "Tally",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 1, 2).ToString(),
                   Weight = 2,
                   Size = "20cm",
                   Status = "Para adoção",
                   Description = "Não gosta de Crianças",
                   Association_id = 2,
                   Image = "/api/animal/img/Tally_animal_18.jpg",
               },
               new Animal
               {
                   Id = 19,
                   Name = "Connor",
                   Type = "Cão",
                   Breed = "Labrador",
                   Age = new DateTime(2016, 7, 24).ToString(),
                   Weight = 30,
                   Size = "50cm",
                   Status = "Para adoção",
                   Description = "Adora água",
                   Association_id = 3,
                   Image = "/api/animal/img/Connor_animal_19.jpg",
               },
               new Animal
               {
                   Id = 20,
                   Name = "Gaia",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019,9, 20).ToString(),
                   Weight = 1,
                   Size = "10cm",
                   Status = "Para adoção",
                   Description = "bebe",
                   Association_id = 4,
                   Image = "/api/animal/img/Gaia_animal_20.jpg",
               },
               new Animal
               {
                   Id = 21,
                   Name = "Palmer",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = new DateTime(2019, 11, 8).ToString(),
                   Weight = 1,
                   Size = "5cm",
                   Status = "Para adoção",
                   Description = "Be",
                   Association_id = 1,
                   Image = "/api/animal/img/Palmer_animal_21.jpg",
               },
               new Animal
               {
                   Id = 22,
                   Name = "Warp",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018,12, 29).ToString(),
                   Weight = 2,
                   Size = "15cm",
                   Status = "Para adoção",
                   Description = "Gosta de Cães",
                   Association_id = 2,
                   Image = "/api/animal/img/Warp_animal_22.jpg",
               },
               new Animal
                {
                    Id = 23,
                    Name = "Rave",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = new DateTime(2019, 9, 13).ToString(),
                   Weight = 1,
                    Size = "8cm",
                    Status = "Para adoção",
                    Description = "Bom para ter num apartamento",
                    Association_id = 1,
                    Image = "/api/animal/img/Rave_animal_23.jpg",
                },
               new Animal
               {
                    Id = 24,
                    Name = "Linus",
                    Type = "Cão",
                    Breed = "Rafeiro",
                    Age = new DateTime(2017,11 , 28).ToString(),
                   Weight = 5,
                    Size = "30cm",
                    Status = "Para adoção",
                    Description = "Muito energetico",
                    Association_id = 1,
                    Image = "/api/animal/img/Linus_animal_24.jpg",
               },
               new Animal
               {
                   Id = 25,
                   Name = "Newton",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 8, 10).ToString(),
                   Weight = 2,
                   Size = "15cm",
                   Status = "Para adoção",
                   Description = "Gosta de Cães",
                   Association_id = 1,
                   Image = "/api/animal/img/Newton_animal_25.jpg",
               },
               new Animal
               {
                   Id = 26,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 7, 12).ToString(),
                   Weight = 10,
                   Size = "10cm",
                   Status = "Para adoção",
                   Description = "Desaparecido",
                   Association_id = 1,
                   Image = "/api/animal/img/Lenny_animal_26.jpg",
               },
               new Animal
               {
                   Id = 27,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 6, 28).ToString(),
                   Weight = 10,
                   Size = "26cm",
                   Status = "Para adoção",
                   Description = "Desaparecido",
                   Association_id = 1,
                   Image = "/api/animal/img/Lenny_animal_27.jpg",
               },
               new Animal
               {
                    Id = 28,
                    Name = "Shakira",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = new DateTime(2016, 2, 19).ToString(),
                   Weight = 4,
                    Size = "20cm",
                    Status = "Para adoção",
                    Description = "Desaparecido",
                    Association_id = 1,
                    Image = "/api/animal/img/Shakira_animal_28.jpg",
               },
               new Animal
               {
                   Id = 29,
                   Name = "Bagel",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 2, 9).ToString(),
                   Weight = 5,
                   Size = "25cm",
                   Status = "Para adoção",
                   Description = "Desaparecido",
                   Association_id = 2,
                   Image = "/api/animal/img/Bagel_animal_29.jpg",
               },
               new Animal
               {
                    Id = 30,
                    Name = "Bingo",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = new DateTime(2018, 1, 17).ToString(),
                   Weight = 3,
                    Size = "20cm",
                    Status = "Para adoção",
                    Description = "Desaparecido",
                    Association_id = 2,
                    Image = "/api/animal/img/Bingo_animal_30.jpg",
               },
               new Animal
                {
                    Id = 31,
                    Name = "Basil",
                    Type = "Gato",
                    Breed = "Rafeiro",
                    Age = new DateTime(2017,11,28).ToString(),
                   Weight = 3,
                    Size = "18cm",
                    Status = "Para adoção",
                    Description = "Desaparecido",
                    Association_id = 2,
                    Image = "/api/animal/img/Basil_animal_31.jpg",
                },
               new Animal
                {
                    Id = 32,
                    Name = "Raisin",
                    Type = "Cão",
                    Breed = "Rafeiro",
                    Age = new DateTime(2016, 5, 17).ToString(),
                   Weight = 9,
                    Size = "20cm",
                    Status = "Para adoção",
                    Description = "Desaparecido",
                    Association_id = 3,
                    Image = "/api/animal/img/Raisin_animal_32.jpg",
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
                     Association_id = 4,
                     Title = "Estamos a ficar sem espaços disponíveis",
                     Description = "Estamos a ficar sem espaços disponíveis",
                     Image = ""
                }
           );
            //Eventos
            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Association_id = 1,
                    Title = "Campanha de Vacinação",
                    Description = "Promoção no preço das vacinas",
                    Location = "No nosso recinto",
                    DateInit =  new DateTime(2020, 3, 20) ,
                    DateEnd = new DateTime(2020, 3, 20),
                    Type = "",
                    Price = 25,
                    Image = "/api/animal/img/event/event_1.jpg"
                },
                new Event
                {
                    Id = 2,
                    Association_id = 1,
                    Title = "Concentração Canina",
                    Description = "Concentração Canina",
                    Location = "Funchal",
                    DateInit = new DateTime(2020, 5, 20),
                    DateEnd = new DateTime(2020, 5, 20),
                    Type = "",
                    Price = 30,
                    Image = "/api/animal/img/event/event_2.jpg"
                },
                new Event
                {
                    Id = 3,
                    Association_id = 1,
                    Title = "Campanha de Vacinação",
                    Description = "Promoção no preço das vacinas",
                    Location = "Funchal",
                    DateInit = new DateTime(2020, 10, 20),
                    DateEnd = new DateTime(2020, 10, 20),
                    Type = "",
                    Price = 30,
                    Image = "/api/animal/img/event/event_3.jpg"
                },
                new Event
                {
                    Id = 4,
                    Association_id = 2,
                    Title = "Campanha de Vacinação",
                    Description = "Promoção no preço das vacinas",
                    Location = "Santa cruz",
                    DateInit = new DateTime(2020, 10, 20),
                    DateEnd = new DateTime(2020, 10, 20),
                    Type = "",
                    Price = 25,
                    Image = "/api/animal/img/event/event_4.jpg"
                },
                new Event
                 {
                     Id = 5,
                     Association_id = 2,
                     Title = "Campanha de adoção",
                     Description = "Promoção no preço das adoções",
                     Location = "Machico",
                     DateInit = new DateTime(2020, 10, 20),
                     DateEnd = new DateTime(2020, 10, 20),
                     Type = "",
                     Price = 25,
                     Image = "/api/animal/img/event/event_8.jpg"
                },
                new Event
                 {
                     Id = 6,
                     Association_id = 3,
                     Title = "Concurso Canino",
                     Description = "Mostre o potencial do seu melhor amigo",
                     Location = "Funchal",
                     DateInit = new DateTime(2020, 10, 20),
                     DateEnd = new DateTime(2020, 10, 20),
                     Type = "",
                     Price = 10,
                     Image = "/api/animal/img/event/event_6.jpg"
                },
                new Event
                 {
                     Id = 7,
                     Association_id = 4,
                     Title = "Campanha de Vacinação",
                     Description = "Promoção no preço das vacinas",
                     Location = "Santana",
                     DateInit = new DateTime(2020, 10, 20),
                     DateEnd = new DateTime(2020, 10, 20),
                     Type = "",
                     Price = 25,
                     Image = "/api/animal/img/event/event_5.jpg"
                },
                new Event
                 {
                     Id = 8,
                     Association_id = 4,
                     Title = "Campanha de adoção",
                     Description = "Promoção no preço das adoções",
                     Location = "Ribeira Brava",
                     DateInit = new DateTime(2020, 10, 20),
                     DateEnd = new DateTime(2020, 10, 20),
                     Type = "",
                     Price = 25,
                     Image = "/api/animal/img/event/event_7.jpg"
                },
                new Event
                 {
                     Id = 9,
                     Association_id = 4,
                     Title = "Campanha de Vacinação",
                     Description = "Promoção no preço das vacinas",
                     Location = "Machico",
                     DateInit = new DateTime(2020, 10, 20),
                     DateEnd = new DateTime(2020, 10, 20),
                     Type = "",
                     Price = 25,
                     Image = "/api/animal/img/event/event_9.jpg"
                },
                new Event
                  {
                      Id = 10,
                      Association_id = 4,
                      Title = "Campanha de adoção",
                      Description = "Promoção no preço das adoções",
                      Location = "Funchal",
                      DateInit = new DateTime(2020, 10, 20),
                      DateEnd = new DateTime(2020, 10, 20),
                      Type = "",
                      Price = 25,
                      Image = "/api/animal/img/event/event_10.jpg"
                }
                );

            builder.Entity<LostAnimalPost>().HasData(
                new LostAnimalPost
                {
                    Id = 1,
                    Title = "Minha cadelinha desapareceu",
                    Description = "Por favor ajudem me a encontrar a minha cadela numero:925789365",
                    Image = "",
                    Contact= "291987123"
                },
                new LostAnimalPost
                {
                    Id = 2,
                    Title = "Meu gato encontra-se desaparecido",
                    Description = "Ajude-me a encontar o meu gato numero:925789365",
                    Image = "",
                    Contact = "291987123"
                },
                new LostAnimalPost
                {
                     Id = 3,
                     Title = "Se alguem ver este cão que me contacte",
                     Description = "numero:925789365",
                     Image = "",
                    Contact = "291987123"
                }
                );
        }

    }
}
