using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetCareFinalVersion.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LostAnimalPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
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
