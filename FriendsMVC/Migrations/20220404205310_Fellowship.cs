using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsMVC.Migrations
{
    public partial class Fellowship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Friends_FriendsFriendId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FriendsFriendId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendsFriendId",
                table: "Friends");

            migrationBuilder.CreateTable(
                name: "Fellows",
                columns: table => new
                {
                    FellowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fellows", x => x.FellowId);
                });

            migrationBuilder.CreateTable(
                name: "Fellowships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendId = table.Column<int>(nullable: false),
                    FellowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fellowships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fellowships_Fellows_FellowId",
                        column: x => x.FellowId,
                        principalTable: "Fellows",
                        principalColumn: "FellowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fellowships_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "FriendId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fellowships_FellowId",
                table: "Fellowships",
                column: "FellowId");

            migrationBuilder.CreateIndex(
                name: "IX_Fellowships_FriendId",
                table: "Fellowships",
                column: "FriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fellowships");

            migrationBuilder.DropTable(
                name: "Fellows");

            migrationBuilder.AddColumn<int>(
                name: "FriendsFriendId",
                table: "Friends",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendsFriendId",
                table: "Friends",
                column: "FriendsFriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Friends_FriendsFriendId",
                table: "Friends",
                column: "FriendsFriendId",
                principalTable: "Friends",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
