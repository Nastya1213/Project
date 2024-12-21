using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AddReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConcertId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConcertId",
                table: "Tickets",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_TicketId",
                table: "OrderTickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTickets_Tickets_TicketId",
                table: "OrderTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Concerts_ConcertId",
                table: "Tickets",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "concert_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTickets_Tickets_TicketId",
                table: "OrderTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Concerts_ConcertId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ConcertId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderTickets_TicketId",
                table: "OrderTickets");
        }
    }
}
