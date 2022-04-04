using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsMVC.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countryes",
                columns: table => new
                {
                    IdCountry = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(nullable: true),
                    CountryPhoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countryes", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    FriendId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Birth = table.Column<DateTime>(nullable: false),
                    FriendsPhoto = table.Column<string>(nullable: true),
                    FriendsFriendId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.FriendId);
                    table.ForeignKey(
                        name: "FK_Friends_Friends_FriendsFriendId",
                        column: x => x.FriendsFriendId,
                        principalTable: "Friends",
                        principalColumn: "FriendId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendsFriendId",
                table: "Friends",
                column: "FriendsFriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countryes");

            migrationBuilder.DropTable(
                name: "Friends");
        }
    }
}
