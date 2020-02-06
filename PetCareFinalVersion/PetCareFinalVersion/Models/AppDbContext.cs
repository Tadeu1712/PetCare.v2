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
                    Address = "R. do Matadouro 10, 9050-100 Funchal",
                    PhoneNumber = "291220852",
                    Description = "Intervenção Activa na Protecção, Bem-estar e Saúde Animal",
                    FoundationDate = new DateTime(1897, 6, 30).ToString(),
                    Image = "spad.png",
               },
               new Association
                 {
                    User_id = 3,
                    Id = 2,
                    Iban = "PT50000702430012359000733",
                    Address = "Santa cruz",
                    PhoneNumber = "961133214",
                    Description = "A Associação PATA – Porque os Animais Também se Amam",
                    FoundationDate = new DateTime(2006, 5, 8).ToString(),
                    Image = "PATA.JPG",
               },
               new Association
                {
                    User_id = 4,
                    Id = 3,
                    Iban = "PT50000702430012359000733",
                    Address = "Funchal",
                    PhoneNumber = "291773357",
                    Description = "Canil/Gatil Municipal do Funchal que tem como objectivo principal a recolha e alojamento de animais de companhia que se encontrem abandonados",
                    FoundationDate = DateTime.Now.ToString(),
                    Image = "Canil_Municipal_Funchal.jpg",
               },
               new Association
                {
                    User_id = 5,
                    Id = 4,
                    Iban = "PT50 0007 0000 0008 4526 8682 3",
                    Address = "Rua Cidade de Oakland 1 Funchal",
                    PhoneNumber = "966295555",
                    Description = "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados",
                    FoundationDate = new DateTime(2012, 1, 2).ToString(),
                    Image = "AMAW.jpg",
               }
            );

            //Animais
            _ = builder.Entity<Animal>().HasData(
               new Animal
               {
                   Id = 1,
                   Name = "Napoleão",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 10, 2),
                   Weight = 2,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 40,
                   Energy = 30,
                   TroubleMaker = 20,
                   Description = "Mancha no centro da testa",
                   Association_id = 1,
                   Image = "Napoleão_animal_1.jpg",

               },
               new Animal
               {
                   Id = 2,
                   Name = "Bolinhas",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 1, 20),
                   Weight = 150,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 60,
                   Chill=55,
                   Energy = 40,
                   TroubleMaker = 50,
                   Description = "Bom cão guarda",
                   Association_id = 1,
                   Image = "Bolinhas_animal_2.jpg",

               },
               new Animal
               {
                   Id = 3,
                   Name = "Bob",
                   Type = "Cão",
                   Breed = "Boxer",
                   Age = new DateTime(2016, 5, 9),
                   Weight = 25,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 58,
                   Chill = 40,
                   Energy = 70,
                   TroubleMaker = 60,
                   Description = "Cão com muita força e energia",
                   Association_id = 1,
                   Image = "Bob_animal_3.jpg",

               },
               new Animal
               {
                   Id = 4,
                   Name = "Belinha",
                   Type = "Gato",
                   Breed = "Ragdoll",
                   Age = new DateTime(2017, 8, 17),
                   Weight = 2,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Gosta de morder",
                   Association_id = 1,
                   Image = "Belinha_animal_4.jpg",

               },
               new Animal
               {
                   Id = 5,
                   Name = "Duke",
                   Type = "Cão",
                   Breed = "Pastor Alemão",
                   Age = new DateTime(2017, 4, 10),
                   Weight = 18,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Dá-se bem com crianças",
                   Association_id = 2,
                   Image = "Duke_animal_5.jpg",
               },
               new Animal
               {
                   Id = 6,
                   Name = "Grey",
                   Type = "Cão",
                   Breed = "Shar-pei",
                   Age = new DateTime(2016, 12, 3),
                   Weight = 27,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Gosta de comer comida húmida",
                   Association_id = 2,
                   Image = "Grey_animal_6.jpg",
               },
               new Animal
               {
                   Id = 7,
                   Name = "Leão",
                   Type = "Cão",
                   Breed = "Pastor Alemão",
                   Age = new DateTime(2019, 11, 10),
                   Weight = 2,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 70,
                   Chill = 80,
                   Energy = 100,
                   TroubleMaker = 80,
                   Description = "Não gosta de gatos",
                   Association_id = 2,
                   Image = "Leão_animal_7.jpg",
               },
               new Animal
               {
                   Id = 8,
                   Name = "Faisca",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 11, 27),
                   Weight = 1,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 50,
                   Energy = 100,
                   TroubleMaker = 40,
                   Description = "Perfeito para apartamentos",
                   Association_id = 2,
                   Image = "Faisca_animal_8.jpg",
               },
               new Animal
               {
                   Id = 9,
                   Name = "Toby",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 5, 21),
                   Weight = 2,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 70,
                   Chill = 40,
                   Energy = 20,
                   TroubleMaker = 60,
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "Toby_animal_9.jpg",
               },
               new Animal
               {
                   Id = 10,
                   Name = "Pipsy",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 3, 10),
                   Weight = 10,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Perfeito para apartamentos",
                   Association_id = 3,
                   Image = "Pipsy_animal_10.jpg",
               },
               new Animal
               {
                   Id = 11,
                   Name = "Quirk",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 5, 11),
                   Weight = 2,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 90,
                   Chill = 30,
                   Energy = 50,
                   TroubleMaker = 60,
                   Description = "Não têm pelo",
                   Association_id = 3,
                   Image = "Quirk_animal_11.jpg",
               },
               new Animal
               {
                   Id = 12,
                   Name = "Odie",
                   Type = "Cão",
                   Breed = "Doberman",
                   Age = new DateTime(2018, 1, 20),
                   Weight = 40,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Precisa de muito espaço",
                   Association_id = 3,
                   Image = "Odie_animal_12.jpg",
               },
               new Animal
               {
                   Id = 13,
                   Name = "Barkley",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2016, 2, 10),
                   Weight = 20,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Gosta de crianças",
                   Association_id = 4,
                   Image = "Barkley_animal_13.jpg",
               },
               new Animal
               {
                   Id = 14,
                   Name = "Maverick",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 1, 15),
                   Weight = 10,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Não gosta de gato",
                   Association_id = 4,
                   Image = "Maverick_animal_14.jpg",
               },
               new Animal
               {
                   Id = 15,
                   Name = "Kobe",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2015, 2, 7),
                   Weight = 30,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Precisa de muito espaço",
                   Association_id = 4,
                   Image = "Kobe_animal_15.jpg",
               },
               new Animal
               {
                   Id = 16,
                   Name = "Dorito",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 10, 2),
                   Weight = 2,
                   Funny = 100,
                   Chill = 70,
                   Energy = 70,
                   TroubleMaker = 80,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Description = "Gosta de Crianças",
                   Association_id = 4,
                   Image = "Dorito_animal_16.jpg",
               },
               new Animal
               {
                   Id = 17,
                   Name = "Rage",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = new DateTime(2017, 12, 29),
                   Weight = 5,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Precisa de muito espaço",
                   Association_id = 1,
                   Image = "Rage_animal_17.jpg",
               },
               new Animal
               {
                   Id = 18,
                   Name = "Tally",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 1, 2),
                   Weight = 2,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 20,
                   Energy = 40,
                   TroubleMaker = 60,
                   Description = "Não gosta de Crianças",
                   Association_id = 2,
                   Image = "Tally_animal_18.jpg",
               },
               new Animal
               {
                   Id = 19,
                   Name = "Connor",
                   Type = "Cão",
                   Breed = "Labrador",
                   Age = new DateTime(2016, 7, 24),
                   Weight = 30,
                   Size = "Grande",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 100,
                   Energy = 50,
                   TroubleMaker = 30,
                   Description = "Adora água",
                   Association_id = 3,
                   Image = "Connor_animal_19.jpg",
               },
               new Animal
               {
                   Id = 20,
                   Name = "Gaia",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 9, 20),
                   Weight = 1,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 100,
                   Energy = 50,
                   TroubleMaker = 30,
                   Description = "É muito fofinho.",
                   Association_id = 4,
                   Image = "Gaia_animal_20.jpg",
               },
               new Animal
               {
                   Id = 21,
                   Name = "Palmer",
                   Type = "Gato",
                   Breed = "Maine Coon",
                   Age = new DateTime(2019, 11, 8),
                   Weight = 1,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 70,
                   Chill = 80,
                   Energy = 90,
                   TroubleMaker = 70,
                   Description = "Muito bom com crianças.",
                   Association_id = 1,
                   Image = "Palmer_animal_21.jpg",
               },
               new Animal
               {
                   Id = 22,
                   Name = "Warp",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 12, 29),
                   Weight = 2,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 20,
                   Chill = 100,
                   Energy = 30,
                   TroubleMaker = 60,
                   Description = "Gosta de Cães",
                   Association_id = 2,
                   Image = "Warp_animal_22.jpg",
               },
               new Animal
               {
                   Id = 23,
                   Name = "Rave",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 9, 13),
                   Weight = 1,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 90,
                   Chill = 100,
                   Energy = 40,
                   TroubleMaker = 50,
                   Description = "Bom para ter num apartamento",
                   Association_id = 1,
                   Image = "Rave_animal_23.jpg",
               },
               new Animal
               {
                   Id = 24,
                   Name = "Linus",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 11, 28),
                   Weight = 5,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Muito energetico",
                   Association_id = 1,
                   Image = "Linus_animal_24.jpg",
               },
               new Animal
               {
                   Id = 25,
                   Name = "Newton",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2019, 8, 10),
                   Weight = 2,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 20,
                   Energy = 50,
                   TroubleMaker = 90,
                   Description = "Gosta de Cães",
                   Association_id = 1,
                   Image = "Newton_animal_25.jpg",
               },
               new Animal
               {
                   Id = 26,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 7, 12),
                   Weight = 10,
                   Size = "Pequeno",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 80,
                   Energy = 80,
                   TroubleMaker = 60,
                   Description = "Gosta de bolas.",
                   Association_id = 1,
                   Image = "Lenny_animal_26.jpg",
               },
               new Animal
               {
                   Id = 27,
                   Name = "Lenny",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 6, 28),
                   Weight = 10,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 100,
                   Chill = 30,
                   Energy = 100,
                   TroubleMaker = 80,
                   Description = "Gosta de brincar com crianças.",
                   Association_id = 1,
                   Image = "Lenny_animal_27.jpg",
               },
               new Animal
               {
                   Id = 28,
                   Name = "Shakira",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2016, 2, 19),
                   Weight = 4,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Nunca está parado",
                   Association_id = 1,
                   Image = "Shakira_animal_28.jpg",
               },
               new Animal
               {
                   Id = 29,
                   Name = "Bagel",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 2, 9),
                   Weight = 5,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 80,
                   Chill = 50,
                   Energy = 80,
                   TroubleMaker = 60,
                   Description = "Não gosta de gatos",
                   Association_id = 2,
                   Image = "Bagel_animal_29.jpg",
               },
               new Animal
               {
                   Id = 30,
                   Name = "Bingo",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2018, 1, 17),
                   Weight = 3,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Bom para apartamentos",
                   Association_id = 2,
                   Image = "Bingo_animal_30.jpg",
               },
               new Animal
               {
                   Id = 31,
                   Name = "Basil",
                   Type = "Gato",
                   Breed = "Rafeiro",
                   Age = new DateTime(2017, 11, 28),
                   Weight = 3,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Adoro apanhar sol",
                   Association_id = 2,
                   Image = "Basil_animal_31.jpg",
               },
               new Animal
               {
                   Id = 32,
                   Name = "Raisin",
                   Type = "Cão",
                   Breed = "Rafeiro",
                   Age = new DateTime(2016, 5, 17),
                   Weight = 9,
                   Size = "Médio",
                   Status = "Adoção",
                   Funny = 50,
                   Chill = 60,
                   Energy = 40,
                   TroubleMaker = 20,
                   Description = "Sempre a correr em circulos.",
                   Association_id = 3,
                   Image = "Raisin_animal_32.jpg",
               }
           );

            //Posts
            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Association_id = 1,
                    Title = "Ajude a nossa associação a angariar fundos para poder aumentar o espaço",
                    Description = "Queremos pedir ajuda dos nossos sócios e amigos para angariar fundos para aumentar a nossa capacidade abrigar cães",
                    Image = ""
                },
                new Post
                {
                    Id = 2,
                    Association_id = 1,
                    Title = "Ajude a nossa associação doando comida de animal",
                    Description = "Queremos pedir ajuda dos nossos sócios e amigos para angariar comida para os nossos animais.",
                    Image = ""
                },
                new Post
                {
                    Id = 3,
                    Association_id = 1,
                    Title = "Estamos a ficar sem espaços disponíveis",
                    Description = "Viemos por este meio anunciar que estamos completamente sobrelotados.",
                    Image = ""
                },
                new Post
                {
                    Id = 4,
                    Association_id = 2,
                    Title = "Numero de animais aumenta no ultimo ano",
                    Description = "Governo Português vem a publico dizer que a taxa de animais abandonados aumentou por 20%.",
                    Image = ""
                },
                new Post
                {
                     Id = 5,
                     Association_id = 2,
                     Title = "Precisamos de doações de ração",
                     Description = "Queremos pedir ajuda dos nossos sócios e amigos para angariar ração para os nossos animais.",
                     Image = ""
                },
                new Post
                {
                    Id = 6,
                    Association_id = 3,
                    Title = "Ano record a nivel de adoção",
                    Description = "Este ano batemos o record em numero de animais adotados",
                    Image = ""
                },
                new Post
                {
                    Id = 7,
                    Association_id = 3,
                    Title = "Um obrigado a todos os que ajudaram",
                    Description = "Venho por este meio agradeçer a todos pela vossa ajuda.",
                    Image = ""
                },
                new Post
                {
                     Id = 8,
                     Association_id = 4,
                     Title = "Ajude a nossa associação doando comida de animal",
                     Description = "Queremos pedir ajuda dos nossos sócios e amigos para angariar ração para os nossos animais.",
                     Image = ""
                },
                new Post
                {
                     Id = 9,
                     Association_id = 4,
                     Title = "Estamos a ficar sem espaços disponíveis",
                     Description = "Gostariamos de informar que estamos a ficar sem espaço disponível para abrigar mais animais",
                     Image = ""
                },
                new Post
                {
                     Id = 10,
                     Association_id = 4,
                     Title = "Precisamos de doações de ração",
                     Description = "Queremos pedir ajuda dos nossos sócios e amigos para angariar ração para os nossos animais.",
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
                    Image = "event/event_1.jpg"
                },
                new Event
                {
                    Id = 2,
                    Association_id = 1,
                    Title = "Concentração Canina",
                    Description = "Venha participar nesta Concentração Canina",
                    Location = "Funchal",
                    DateInit = new DateTime(2020, 5, 20),
                    DateEnd = new DateTime(2020, 5, 20),
                    Type = "",
                    Price = 30,
                    Image = "event_2.jpg"
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
                    Image = "event_3.jpg"
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
                    Image = "event_4.jpg"
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
                     Image = "event_8.jpg"
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
                     Image = "event_6.jpg"
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
                     Image = "event_5.jpg"
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
                     Image = "event_7.jpg"
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
                    Image = "event_9.jpg"
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
                    Image = "event_10.jpg"
                }
                );

            builder.Entity<LostAnimalPost>().HasData(
                new LostAnimalPost
                {
                    Id = 1,
                    Title = "Minha cadelinha desapareceu",
                    Description = "Por favor ajudem me a encontrar a minha cadela",
                    Image = "lost_1",
                    Contact= "291987123",
                    Location ="Avenida do mar",
                    Date = new DateTime(2020 -01-29)
                },
                new LostAnimalPost
                {
                    Id = 2,
                    Title = "Meu gato encontra-se desaparecido",
                    Description = "Ajude-me a encontar o meu gato por favor",
                    Image = "lost_2",
                    Contact = "291987123",
                    Location = "Rua da carreira",
                    Date = new DateTime(2020-01-15)
                },
                new LostAnimalPost
                {
                    Id = 3,
                    Title = "Se alguem ver este cão contacte-me por favor",
                    Description = "É um cão de porte muito grande, mas é amoroso",
                    Image = "lost_3",
                    Contact = "291987123",
                    Location = "Rua das Pretas",
                    Date = new DateTime(2020 -01-01)
                }
                );;
        }

    }
}
