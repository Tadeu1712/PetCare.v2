using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetCareFinalVersion.Migrations
{
    public partial class Final : Migration
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
                    Age = table.Column<DateTime>(maxLength: 50, nullable: false),
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
                    { 1, "R. do Matadouro 10, 9050-100 Funcha", "Intervenção Activa na Protecção, Bem-estar e Saúde Animal", new DateTime(2020, 1, 30, 16, 26, 42, 572, DateTimeKind.Local).AddTicks(9550), "PT50000702430012359000733", "291220852", 2 },
                    { 2, "Santa cruz", "A Associação PATA – Porque os Animais Também se Amam", new DateTime(2020, 1, 30, 16, 26, 42, 592, DateTimeKind.Local).AddTicks(5680), "233924194", "961133214", 3 },
                    { 3, "Funchal", "Canil/Gatil Municipal do Funchal que tem como objectivo principal a recolha e alojamento de animais de companhia que se encontrem abandonados", new DateTime(2020, 1, 30, 16, 26, 42, 592, DateTimeKind.Local).AddTicks(5710), "28374659", "291773357", 4 },
                    { 4, "Rua Cidade de Oakland 1 Funchal", "Madeira Animal Welfare tem por objetivo controlar a reprodução de canídeos e felideos abandonados", new DateTime(2020, 1, 30, 16, 26, 42, 592, DateTimeKind.Local).AddTicks(5710), "28374659", "966295555", 5 }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "Association_id", "Breed", "Description", "Image", "Name", "Size", "Status", "Type", "Weight" },
                values: new object[] { 1, new DateTime(1995, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rafeiro", "Mancha no centro da testa", "/api/animal/img/Napoleão_animal_1.jpg", "Napoleão", "30cm", "Para adoção", "Gato", 2f });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Association_id", "DateEnd", "DateInit", "Description", "Image", "Location", "Price", "Title", "Type" },
                values: new object[,]
                {
                    { 10, 4, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9430), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9430), "Promoção no preço das adoções", "", "Funchal", 25m, "Campanha de adoção", "" },
                    { 9, 4, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9430), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9420), "Promoção no preço das vacinas", "", "Machico", 25m, "Campanha de Vacinação", "" },
                    { 8, 4, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9420), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9420), "Promoção no preço das adoções", "", "Ribeira Brava", 25m, "Campanha de adoção", "" },
                    { 7, 4, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9420), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9420), "Promoção no preço das vacinas", "", "Santana", 25m, "Campanha de Vacinação", "" },
                    { 6, 3, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9410), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9410), "Mostre o potencial do seu melhor amigo", "", "Funchal", 10m, "Concurso Canino", "" },
                    { 4, 2, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9400), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9400), "Promoção no preço das vacinas", "", "Santa cruz", 25m, "Campanha de Vacinação", "" },
                    { 5, 3, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9410), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9410), "Promoção no preço das adoções", "", "Machico", 25m, "Campanha de adoção", "" },
                    { 2, 1, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9360), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9350), "Concentração Canina", "", "Funchal", 30m, "Concentração Canina", "" },
                    { 1, 1, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(7800), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(7400), "Promoção no preço das vacinas", "", "No nosso recinto", 10m, "Campanha de Vacinação", "" },
                    { 3, 2, new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9400), new DateTime(2020, 1, 30, 16, 26, 42, 593, DateTimeKind.Local).AddTicks(9400), "Promoção no preço das adoções", "", "Funchal", 30m, "Campanha de adoção", "" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Association_id", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 4, 2, "Numero de animais aumenta no ultimo ano", "", "PATA - Numero de animais aumenta no ultimo ano" },
                    { 5, 2, "Precisamos de doações de ração", "", "PATA - Precisamos de doações de ração" },
                    { 9, 4, "Estamos a ficar sem espaços disponíveis", "", "Estamos a ficar sem espaços disponíveis" },
                    { 3, 1, "Estamos a ficar sem espaços disponíveis", "", "SPAD - Estamos a ficar sem espaços disponíveis" },
                    { 6, 3, "Ano record a nivel de adoção", "", "Ano record a nivel de adoção" },
                    { 7, 3, "Um obrigado a todos os que ajudaram", "", "Um obrigado a todos os que ajudaram" },
                    { 2, 1, "Ajude a nossa associação doando comida de animal", "", "SPAD - Ajude a nossa associação doando comida de animal" },
                    { 1, 1, "Ajude a nossa associação a angariar fundos para poder aumentar o espaço", "", "SPAD - Ajude a nossa associação a angariar fundos para poder aumentar o espaço" },
                    { 8, 4, "Precismos de doações de ração", "", "" },
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
