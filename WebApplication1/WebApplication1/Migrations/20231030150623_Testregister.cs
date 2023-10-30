using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Testregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVoyages_AspNetUsers_UserId",
                table: "UserVoyages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserVoyages",
                newName: "TripUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserVoyages_UserId",
                table: "UserVoyages",
                newName: "IX_UserVoyages_TripUserId1");

            migrationBuilder.AddColumn<int>(
                name: "TripUserId",
                table: "UserVoyages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TripUserVoyage",
                columns: table => new
                {
                    TripUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoyagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripUserVoyage", x => new { x.TripUsersId, x.VoyagesId });
                    table.ForeignKey(
                        name: "FK_TripUserVoyage_AspNetUsers_TripUsersId",
                        column: x => x.TripUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripUserVoyage_Voyages_VoyagesId",
                        column: x => x.VoyagesId,
                        principalTable: "Voyages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripUserVoyage_VoyagesId",
                table: "TripUserVoyage",
                column: "VoyagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVoyages_AspNetUsers_TripUserId1",
                table: "UserVoyages",
                column: "TripUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVoyages_AspNetUsers_TripUserId1",
                table: "UserVoyages");

            migrationBuilder.DropTable(
                name: "TripUserVoyage");

            migrationBuilder.DropColumn(
                name: "TripUserId",
                table: "UserVoyages");

            migrationBuilder.RenameColumn(
                name: "TripUserId1",
                table: "UserVoyages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVoyages_TripUserId1",
                table: "UserVoyages",
                newName: "IX_UserVoyages_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVoyages_AspNetUsers_UserId",
                table: "UserVoyages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
