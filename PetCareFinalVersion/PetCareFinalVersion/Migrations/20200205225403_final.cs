using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetCareFinalVersion.Migrations
{
    public partial class final : Migration
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
                    Location = table.Column<string>(maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
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
                    Iban = table.Column<string>(maxLength: 32, nullable: false),
                    Adress = table.Column<string>(maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 9, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    FoundationDate = table.Column<string>(nullable: false),
                    User_id = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
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
                    Age = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Size = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Personality = table.Column<string>(nullable: true),
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
                columns: new[] { "Id", "Contact", "Date", "Description", "Image", "Location", "Title" },
                values: new object[,]
                {
                    { 1, "291987123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1990), "Por favor ajudem me a encontrar a minha cadela numero:925789365", "", "Avenida do mar", "Minha cadelinha desapareceu" },
                    { 2, "291987123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2004), "Ajude-me a encontar o meu gato numero:925789365", "", "Rua da carreira", "Meu gato encontra-se desaparecido" },
                    { 3, "291987123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2018), "numero:925789365", "", "Rua das Pretas", "Se alguem ver este cão que me contacte" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, true, "Admin@admin.com", "Admin", "$2a$11$YYcMhhyRnSz/Po24qfD1n.WWUA74a3G5Eq432DWpP/fFU.oA4YNH." },
                    { 2, false, "spadfnc@gmail.com", "Spad", "$2a$11$mndRST8..kAEXYTdWg03l.zXz42i5Bc.hQtucGtKRzPZbY8okgPjm" },
                    { 3, false, "pata@pata.pt", "PATA", "$2a$11$BjIIUCXP794kKcsEHdkzPegZUqr3.m78Ypq2sbNPUBP5l11Rr2qqu" },
                    { 4, false, "CMF@cmf.com", "Canil Municipal do Funchal", "$2a$11$fhHwln15t6A/kpQAqXiTLu0VdAElUqtivXXIzGESCThHPBKWONEI6" },
                    { 5, false, "amaw@madeiraanimalwelfare.org", "Associação Madeira Animal Welfare", "$2a$11$LPty9nYD2T./ae1lIH/3g.31iH3VpN.O7irXcBjN14CAwjYJGClqS" }
                });

            migrationBuilder.InsertData(
                table: "Associations",
                columns: new[] { "Id", "Adress", "Description", "FoundationDate", "Iban", "Image", "PhoneNumber", "User_id" },
                values: new object[,]
                {
                    { 1, "R. do Matadouro 10, 9050-100 Funchal", "Intervenção Activa na Protecção, Bem-estar e Saúde Animal", "06/30/1897 00:00:00", "PT50000702430012359000733", null, "291220852", 2 },
                    { 2, "Santa cruz", "A Associação PATA – Porque os Animais Também se Amam", "05/08/2006 00:00:00", "233924194", null, "961133214", 3 },
                    { 3, "Funchal", "Canil/Gatil Municipal do Funchal que tem como objectivo principal a recolha e alojamento de animais de companhia que se encontrem abandonados", "02/05/2020 22:54:02", "28374659", null, "291773357", 4 },
                    { 4, "Rua Cidade de Oakland 1 Funchal", "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados", "01/02/2012 00:00:00", "PT50 0007 0000 0008 4526 8682 3", null, "966295555", 5 }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "Association_id", "Breed", "Description", "Image", "Name", "Personality", "Size", "Status", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Mancha no centro da testa", "Napoleão_animal_1.jpg", "Napoleão", null, "30cm", "Adoção", "Gato", 2f },
                    { 8, new DateTime(2019, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Perfeito para apartamentos", "Faisca_animal_8.jpg", "Faisca", null, "15cm", "Adoção", "Gato", 1f },
                    { 18, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Não gosta de Crianças", "Tally_animal_18.jpg", "Tally", null, "20cm", "Adoção", "Gato", 2f },
                    { 22, new DateTime(2018, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Gosta de Cães", "Warp_animal_22.jpg", "Warp", null, "15cm", "Adoção", "Gato", 2f },
                    { 29, new DateTime(2018, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Desaparecido", "Bagel_animal_29.jpg", "Bagel", null, "25cm", "Adoção", "Cão", 5f },
                    { 31, new DateTime(2017, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Desaparecido", "Basil_animal_31.jpg", "Basil", null, "18cm", "Adoção", "Gato", 3f },
                    { 9, new DateTime(2018, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Rafeiro", "Perfeito para apartamentos", "Toby_animal_9.jpg", "Toby", null, "10cm", "Adoção", "Gato", 2f },
                    { 10, new DateTime(2017, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Rafeiro", "Perfeito para apartamentos", "Pipsy_animal_10.jpg", "Pipsy", null, "20cm", "Adoção", "Cão", 10f },
                    { 7, new DateTime(2019, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pastor Alemão", "Não gosta de gatos", "Leão_animal_7.jpg", "Leão", null, "15cm", "Adoção", "Cão", 2f },
                    { 11, new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Rafeiro", "Não têm pelo", "Quirk_animal_11.jpg", "Quirk", null, "10cm", "Adoção", "Gato", 2f },
                    { 19, new DateTime(2016, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Labrador", "Adora água", "Connor_animal_19.jpg", "Connor", null, "50cm", "Adoção", "Cão", 30f },
                    { 32, new DateTime(2016, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Rafeiro", "Desaparecido", "Raisin_animal_32.jpg", "Raisin", null, "20cm", "Adoção", "Cão", 9f },
                    { 13, new DateTime(2016, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rafeiro", "Gosta de crianças", "Barkley_animal_13.jpg", "Barkley", null, "50cm", "Adoção", "Cão", 20f },
                    { 14, new DateTime(2017, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rafeiro", "Não gosta de gato", "Maverick_animal_14.jpg", "Maverick", null, "15cm", "Adoção", "Cão", 10f },
                    { 15, new DateTime(2015, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rafeiro", "Precisa de muito espaço", "Kobe_animal_15.jpg", "Kobe", null, "40cm", "Adoção", "Cão", 30f },
                    { 16, new DateTime(2019, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rafeiro", "Gosta de Crianças", "Dorito_animal_16.jpg", "Dorito", null, "20cm", "Adoção", "Gato", 2f },
                    { 20, new DateTime(2019, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rafeiro", "bebe", "Gaia_animal_20.jpg", "Gaia", null, "10cm", "Adoção", "Cão", 1f },
                    { 12, new DateTime(2018, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Doberman", "Precisa de muito espaço", "Odie_animal_12.jpg", "Odie", null, "72cm", "Adoção", "Cão", 40f },
                    { 6, new DateTime(2016, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Shar-pei", "Gosta de comer comida humida", "Grey_animal_6.jpg", "Grey", null, "50cm", "Adoção", "Cão", 27f },
                    { 30, new DateTime(2018, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rafeiro", "Desaparecido", "Bingo_animal_30.jpg", "Bingo", null, "20cm", "Adoção", "Gato", 3f },
                    { 24, new DateTime(2017, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Muito energetico", "Linus_animal_24.jpg", "Linus", null, "30cm", "Adoção", "Cão", 5f },
                    { 21, new DateTime(2019, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Maine Coon", "Be", "Palmer_animal_21.jpg", "Palmer", null, "5cm", "Adoção", "Gato", 1f },
                    { 3, new DateTime(2016, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Boxer", "Muita energia", "Bob_animal_3.jpg", "Bob", null, "1.20m", "Adoção", "Cão", 25f },
                    { 2, new DateTime(2018, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Cão de pequeno porte", "Bolinhas_animal_2.jpg", "Bolinhas", null, "180 metros", "Adoção", "Cão", 150f },
                    { 23, new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Bom para ter num apartamento", "Rave_animal_23.jpg", "Rave", null, "8cm", "Adoção", "Gato", 1f },
                    { 5, new DateTime(2017, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pastor Alemão", "Dá-se bem com crianças", "Duke_animal_5.jpg", "Duke", null, "50cm", "Adoção", "Cão", 18f },
                    { 25, new DateTime(2019, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Gosta de Cães", "Newton_animal_25.jpg", "Newton", null, "15cm", "Adoção", "Gato", 2f },
                    { 26, new DateTime(2018, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Desaparecido", "Lenny_animal_26.jpg", "Lenny", null, "10cm", "Adoção", "Cão", 10f },
                    { 27, new DateTime(2018, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Desaparecido", "Lenny_animal_27.jpg", "Lenny", null, "26cm", "Adoção", "Cão", 10f },
                    { 17, new DateTime(2017, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Maine Coon", "Precisa de muito espaço", "Rage_animal_17.jpg", "Rage", null, "70cm", "Adoção", "Gato", 5f },
                    { 28, new DateTime(2016, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Desaparecido", "Shakira_animal_28.jpg", "Shakira", null, "20cm", "Adoção", "Gato", 4f },
                    { 4, new DateTime(2017, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ragdoll", "Pelo longo, com orellahs pretas", "Belinha_animal_4.jpg", "Belinha", null, "25 cm", "Adoção", "Gato", 2f }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Association_id", "DateEnd", "DateInit", "Description", "Image", "Location", "Price", "Title", "Type" },
                values: new object[,]
                {
                    { 8, 4, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das adoções", "event_7.jpg", "Ribeira Brava", 25m, "Campanha de adoção", "" },
                    { 5, 2, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das adoções", "event_8.jpg", "Machico", 25m, "Campanha de adoção", "" },
                    { 4, 2, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das vacinas", "event_4.jpg", "Santa cruz", 25m, "Campanha de Vacinação", "" },
                    { 1, 1, new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das vacinas", "event/event_1.jpg", "No nosso recinto", 25m, "Campanha de Vacinação", "" },
                    { 9, 4, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das vacinas", "event_9.jpg", "Machico", 25m, "Campanha de Vacinação", "" },
                    { 2, 1, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Concentração Canina", "event_2.jpg", "Funchal", 30m, "Concentração Canina", "" },
                    { 6, 3, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mostre o potencial do seu melhor amigo", "event_6.jpg", "Funchal", 10m, "Concurso Canino", "" },
                    { 3, 1, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das vacinas", "event_3.jpg", "Funchal", 30m, "Campanha de Vacinação", "" },
                    { 10, 4, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das adoções", "event_10.jpg", "Funchal", 25m, "Campanha de adoção", "" },
                    { 7, 4, new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Promoção no preço das vacinas", "event_5.jpg", "Santana", 25m, "Campanha de Vacinação", "" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Association_id", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 8, 4, "Precismos de doações de ração", "", "" },
                    { 3, 1, "Estamos a ficar sem espaços disponíveis", "", "SPAD - Estamos a ficar sem espaços disponíveis" },
                    { 6, 3, "Ano record a nivel de adoção", "", "Ano record a nivel de adoção" },
                    { 5, 2, "Precisamos de doações de ração", "", "PATA - Precisamos de doações de ração" },
                    { 4, 2, "Numero de animais aumenta no ultimo ano", "", "PATA - Numero de animais aumenta no ultimo ano" },
                    { 9, 4, "Estamos a ficar sem espaços disponíveis", "", "Estamos a ficar sem espaços disponíveis" },
                    { 1, 1, "Ajude a nossa associação a angariar fundos para poder aumentar o espaço", "", "SPAD - Ajude a nossa associação a angariar fundos para poder aumentar o espaço" },
                    { 2, 1, "Ajude a nossa associação doando comida de animal", "", "SPAD - Ajude a nossa associação doando comida de animal" },
                    { 7, 3, "Um obrigado a todos os que ajudaram", "", "Um obrigado a todos os que ajudaram" },
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
