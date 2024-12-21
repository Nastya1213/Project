using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewsAndUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Review_ConcertId",
                table: "Review",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Concerts_ConcertId",
                table: "Review",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "concert_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Concerts_ConcertId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ConcertId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");
        }
    }
}
