using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetCareFinalVersion.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LostAnimalPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostAnimalPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Iban = table.Column<string>(maxLength: 25, nullable: false),
                    Adress = table.Column<string>(maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 9, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    FoundationDate = table.Column<DateTime>(nullable: false),
                    User_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associations_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Breed = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<int>(maxLength: 50, nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Size = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Association_id = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Associations_Association_id",
                        column: x => x.Association_id,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Association_id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Location = table.Column<string>(maxLength: 250, nullable: false),
                    DateInit = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Associations_Association_id",
                        column: x => x.Association_id,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Association_id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Associations_Association_id",
                        column: x => x.Association_id,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LostAnimalPosts",
                columns: new[] { "Id", "Contact", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 1, "291987123", "Por favor ajudem me a encontrar a minha cadela numero:925789365", "", "Minha cadelinha desapareceu" },
                    { 2, "291987123", "Ajude-me a encontar o meu gato numero:925789365", "", "Meu gato encontra-se desaparecido" },
                    { 3, "291987123", "numero:925789365", "", "Se alguem ver este cão que me contacte" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, true, "Admin@admin.com", "Admin", "secret123" },
                    { 2, false, "spadfnc@gmail.com", "Spad", "secret123" },
                    { 3, false, "pata@pata.pt", "PATA", "secret123" },
                    { 4, false, "CMF@cmf.com", "Canil Municipal do Funchal", "secret123" },
                    { 5, false, "amaw@madeiraanimalwelfare.org", "Associação Madeira Animal Welfare", "secret123" }
                });

            migrationBuilder.InsertData(
                table: "Associations",
                columns: new[] { "Id", "Adress", "Description", "FoundationDate", "Iban", "PhoneNumber", "User_id" },
                values: new object[,]
                {
                    { 1, "R. do Matadouro 10, 9050-100 Funcha", "Intervenção Activa na Protecção, Bem-estar e Saúde Animal", new DateTime(2020, 1, 29, 20, 49, 4, 510, DateTimeKind.Local).AddTicks(2260), "PT50000702430012359000733", "291220852", 2 },
                    { 2, "Santa cruz", "A Associação PATA – Porque os Animais Também se Amam", new DateTime(2020, 1, 29, 20, 49, 4, 528, DateTimeKind.Local).AddTicks(2600), "233924194", "961133214", 3 },
                    { 3, "Funchal", "Canil/Gatil Municipal do Funchal que tem como objectivo principal a recolha e alojamento de animais de companhia que se encontrem abandonados", new DateTime(2020, 1, 29, 20, 49, 4, 528, DateTimeKind.Local).AddTicks(2630), "28374659", "291773357", 4 },
                    { 4, "Rua Cidade de Oakland 1 Funchal", "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados", new DateTime(2020, 1, 29, 20, 49, 4, 528, DateTimeKind.Local).AddTicks(2640), "28374659", "966295555", 5 }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "Association_id", "Breed", "Description", "Image", "Name", "Size", "Status", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 1, "Rafeiro", "Mancha no centro da testa", "/api/animal/img/Napoleão_animal_1.jpg", "Napoleão", "30cm", "Para adoção", "Gato", 2f },
                    { 18, 1, 2, "Rafeiro", "Não gosta de Crianças", "/api/animal/img/Tally_animal_18.jpg", "Tally", "20cm", "Para adoção", "Gato", 2f },
                    { 22, 1, 2, "Rafeiro", "Gosta de Cães", "/api/animal/img/Warp_animal_22.jpg", "Warp", "15cm", "Para adoção", "Gato", 2f },
                    { 29, 2, 2, "Rafeiro", "Desaparecido", "/api/animal/img/Bagel_animal_29.jpg", "Bagel", "25cm", "Para adoção", "Cão", 5f },
                    { 30, 1, 2, "Rafeiro", "Desaparecido", "/api/animal/img/Bingo_animal_30.jpg", "Bingo", "20cm", "Para adoção", "Gato", 3f },
                    { 9, 2, 3, "Rafeiro", "Perfeito para apartamentos", "/api/animal/img/Toby_animal_9.jpg", "Toby", "10cm", "Para adoção", "Gato", 2f },
                    { 10, 3, 3, "Rafeiro", "Perfeito para apartamentos", "/api/animal/img/Pipsy_animal_10.jpg", "Pipsy", "20cm", "Para adoção", "Cão", 10f },
                    { 11, 2, 3, "Sphynx", "Não têm pelo", "/api/animal/img/Quirk_animal_11.jpg", "Quirk", "10cm", "Para adoção", "Gato", 2f },
                    { 12, 2, 3, "Doberman", "Precisa de muito espaço", "/api/animal/img/Odie_animal_12.jpg", "Odie", "72cm", "Para adoção", "Cão", 40f },
                    { 19, 4, 3, "Labrador", "Adora água", "/api/animal/img/Connor_animal_19.jpg", "Connor", "50cm", "Para adoção", "Cão", 30f },
                    { 32, 3, 3, "Rafeiro", "Desaparecido", "/api/animal/img/Raisin_animal_32.jpg", "Raisin", "20cm", "Para adoção", "Cão", 9f },
                    { 13, 4, 4, "Rafeiro", "Gosta de crianças", "/api/animal/img/Barkley_animal_13.jpg", "Barkley", "50cm", "Para adoção", "Cão", 20f },
                    { 14, 3, 4, "Rafeiro", "Não gosta de gato", "/api/animal/img/Maverick_animal_14.jpg", "Maverick", "15cm", "Para adoção", "Cão", 10f },
                    { 15, 6, 4, "Rafeiro", "Precisa de muito espaço", "/api/animal/img/Kobe_animal_15.jpg", "Kobe", "40cm", "Para adoção", "Cão", 30f },
                    { 16, 0, 4, "Rafeiro", "Gosta de Crianças", "/api/animal/img/Dorito_animal_16.jpg", "Dorito", "20cm", "Para adoção", "Gato", 2f },
                    { 20, 0, 4, "Rafeiro", "bebe", "/api/animal/img/Gaia_animal_20.jpg", "Gaia", "10cm", "Para adoção", "Cão", 1f },
                    { 8, 0, 2, "Rafeiro", "Perfeito para apartamentos", "/api/animal/img/Faisca_animal_8.jpg", "Faisca", "15cm", "Para adoção", "Gato", 1f },
                    { 7, 0, 2, "Pastor Alemão", "Não gosta de gatos", "/api/animal/img/Leão_animal_7.jpg", "Leão", "15cm", "Para adoção", "Cão", 2f },
                    { 31, 2, 2, "Rafeiro", "Desaparecido", "/api/animal/img/Basil_animal_31.jpg", "Basil", "18cm", "Para adoção", "Gato", 3f },
                    { 5, 3, 2, "Pastor Alemão", "Dá-se bem com crianças", "/api/animal/img/Duke_animal_5.jpg", "Duke", "50cm", "Para adoção", "Cão", 18f },
                    { 2, 2, 1, "Rafeiro", "Cão de pequeno porte", "/api/animal/img/Bolinhas_animal_2.jpg", "Bolinhas", "180 metros", "Para adoção", "Cão", 150f },
                    { 3, 4, 1, "Boxer", "Muita energia", "/api/animal/img/Bob_animal_3.jpg", "Bob", "1.20m", "Para adoção", "Cão", 25f },
                    { 4, 3, 1, "Ragdoll", "Pelo longo, com orellahs pretas", "/api/animal/img/Belinha_animal_4.jpg", "Belinha", "25 cm", "Para adoção", "Gato", 2f },
                    { 17, 2, 1, "Maine Coon", "Precisa de muito espaço", "/api/animal/img/Rage_animal_17.jpg", "Rage", "70cm", "Para adoção", "Gato", 5f },
                    { 21, 0, 1, "Maine Coon", "Be", "/api/animal/img/Palmer_animal_21.jpg", "Palmer", "5cm", "Para adoção", "Gato", 1f },
                    { 6, 4, 2, "Shar-pei", "Gosta de comer comida humida", "/api/animal/img/Grey_animal_6.jpg", "Grey", "50cm", "Para adoção", "Cão", 27f },
                    { 24, 2, 1, "Rafeiro", "Muito energetico", "/api/animal/img/Linus_animal_24.jpg", "Linus", "30cm", "Para adoção", "Cão", 5f },
                    { 25, 0, 1, "Rafeiro", "Gosta de Cães", "/api/animal/img/Newton_animal_25.jpg", "Newton", "15cm", "Para adoção", "Gato", 2f },
                    { 26, 1, 1, "Rafeiro", "Desaparecido", "/api/animal/img/Lenny_animal_26.jpg", "Lenny", "10cm", "Para adoção", "Cão", 10f },
                    { 27, 2, 1, "Rafeiro", "Desaparecido", "/api/animal/img/Lenny_animal_27.jpg", "Lenny", "26cm", "Para adoção", "Cão", 10f },
                    { 23, 0, 1, "Rafeiro", "Bom para ter num apartamento", "/api/animal/img/Rave_animal_23.jpg", "Rave", "8cm", "Para adoção", "Gato", 1f },
                    { 28, 4, 1, "Rafeiro", "Desaparecido", "/api/animal/img/Shakira_animal_28.jpg", "Shakira", "20cm", "Para adoção", "Gato", 4f }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Association_id", "DateEnd", "DateInit", "Description", "Image", "Location", "Price", "Title", "Type" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6280), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6260), "Concentração Canina", "", "Funchal", 30m, "Concentração Canina", "" },
                    { 4, 2, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6320), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6320), "Promoção no preço das vacinas", "", "Santa cruz", 25m, "Campanha de Vacinação", "" },
                    { 3, 2, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6310), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6310), "Promoção no preço das adoções", "", "Funchal", 30m, "Campanha de adoção", "" },
                    { 10, 4, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6340), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6340), "Promoção no preço das adoções", "", "Funchal", 25m, "Campanha de adoção", "" },
                    { 5, 3, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6320), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6320), "Promoção no preço das adoções", "", "Machico", 25m, "Campanha de adoção", "" },
                    { 6, 3, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6330), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6330), "Mostre o potencial do seu melhor amigo", "", "Funchal", 10m, "Concurso Canino", "" },
                    { 9, 4, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6340), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6340), "Promoção no preço das vacinas", "", "Machico", 25m, "Campanha de Vacinação", "" },
                    { 8, 4, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6340), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6330), "Promoção no preço das adoções", "", "Ribeira Brava", 25m, "Campanha de adoção", "" },
                    { 1, 1, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(4590), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(4180), "Promoção no preço das vacinas", "", "No nosso recinto", 10m, "Campanha de Vacinação", "" },
                    { 7, 4, new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6330), new DateTime(2020, 1, 29, 20, 49, 4, 529, DateTimeKind.Local).AddTicks(6330), "Promoção no preço das vacinas", "", "Santana", 25m, "Campanha de Vacinação", "" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Association_id", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 8, 4, "Precismos de doações de ração", "", "" },
                    { 3, 1, "Estamos a ficar sem espaços disponíveis", "", "SPAD - Estamos a ficar sem espaços disponíveis" },
                    { 5, 2, "Precisamos de doações de ração", "", "PATA - Precisamos de doações de ração" },
                    { 1, 1, "Ajude a nossa associação a angariar fundos para poder aumentar o espaço", "", "SPAD - Ajude a nossa associação a angariar fundos para poder aumentar o espaço" },
                    { 7, 3, "Um obrigado a todos os que ajudaram", "", "Um obrigado a todos os que ajudaram" },
                    { 6, 3, "Ano record a nivel de adoção", "", "Ano record a nivel de adoção" },
                    { 9, 4, "Estamos a ficar sem espaços disponíveis", "", "Estamos a ficar sem espaços disponíveis" },
                    { 4, 2, "Numero de animais aumenta no ultimo ano", "", "PATA - Numero de animais aumenta no ultimo ano" },
                    { 2, 1, "Ajude a nossa associação doando comida de animal", "", "SPAD - Ajude a nossa associação doando comida de animal" },
                    { 10, 4, "Estamos a ficar sem espaços disponíveis", "", "Estamos a ficar sem espaços disponíveis" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Association_id",
                table: "Animals",
                column: "Association_id");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_User_id",
                table: "Associations",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Association_id",
                table: "Events",
                column: "Association_id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Association_id",
                table: "Posts",
                column: "Association_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "LostAnimalPosts");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
