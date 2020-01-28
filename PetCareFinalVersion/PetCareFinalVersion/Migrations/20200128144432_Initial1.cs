using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetCareFinalVersion.Migrations
{
    public partial class Initial1 : Migration
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
                    Age = table.Column<string>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Size = table.Column<string>(nullable: false),
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
                table: "Users",
                columns: new[] { "Id", "Admin", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, true, "Admin@admin.com", "Admin", "secret123" },
                    { 2, false, "Artur@artur.com", "Artur", "secret123" },
                    { 3, false, "Ricardo@ricardo.com", "Ricardo", "secret123" },
                    { 4, false, "Ruben@ruben.com", "Ruben", "secret123" },
                    { 5, false, "Tadeu@tadeu.com", "Tadeu", "secret123" }
                });

            migrationBuilder.InsertData(
                table: "Associations",
                columns: new[] { "Id", "Adress", "Description", "FoundationDate", "Iban", "PhoneNumber", "User_id" },
                values: new object[,]
                {
                    { 1, "Rua dos milagres", "Spad", new DateTime(2020, 1, 28, 14, 44, 32, 59, DateTimeKind.Local).AddTicks(7620), "233924194", "291876234", 2 },
                    { 2, "Rua dos milagres", "Spad", new DateTime(2020, 1, 28, 14, 44, 32, 95, DateTimeKind.Local).AddTicks(8400), "233924194", "291876234", 3 },
                    { 3, "Rua dos Vinagres", "Ajuda do Animal", new DateTime(2020, 1, 28, 14, 44, 32, 95, DateTimeKind.Local).AddTicks(8440), "28374659", "291745637", 4 },
                    { 4, "Rua dos Vinagres", "Ajuda do Animal", new DateTime(2020, 1, 28, 14, 44, 32, 95, DateTimeKind.Local).AddTicks(8440), "28374659", "291745637", 5 }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "Association_id", "Breed", "Description", "Image", "Name", "Size", "Status", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, "1", 1, "Rafeiro", "Mancha no centro da testa", "", "Napoleão", "30cm", "Nem sei", "Gato", 2 },
                    { 2, "2", 1, "Rafeiro", "Cão de pequeno porte", "", "Bolinhas", "180 metros", "Nem sei", "Cão", 150 },
                    { 3, "4", 1, "Boxer", "Muita energia", "", "Bob", "1.20m", "nem sei", "Cão", 25 },
                    { 4, "3", 2, "Ragdoll", "Pelo longo, com orellahs pretas", "", "Belinha", "25 cm", "Nem sei", "Gato", 2 },
                    { 5, "3", 2, "Pastor Alemão", "Dá-se bem com crianças", "", "Duke", "50cm", "Nem sei", "Cão", 18 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Association_id", "DateEnd", "DateInit", "Description", "Image", "Location", "Price", "Title", "Type" },
                values: new object[] { 1, 1, new DateTime(2020, 1, 28, 14, 44, 32, 97, DateTimeKind.Local).AddTicks(1850), new DateTime(2020, 1, 28, 14, 44, 32, 97, DateTimeKind.Local).AddTicks(1400), "", "wasd", "", 2m, "", "" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Association_id", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Dá-se bem com crianças, cão muito energetico", "", "Bob procura nova casa " },
                    { 2, 1, "Napoleão é um gato muito amigavel", "", "Ajude-nos a encontrar novo dono para Napoleão." },
                    { 3, 2, "Ajude a nossa associação a angariar fundos para poder aumentar o espaço", "", "Ajude a nossa associação a angariar fundos para poder aumentar o espaço" },
                    { 4, 2, "Ajude a nossa associação doando comida de animal", "", "Ajude a nossa associação doando comida de animal" }
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
