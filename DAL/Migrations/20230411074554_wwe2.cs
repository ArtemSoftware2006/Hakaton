using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class wwe2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealHasProposals");

            migrationBuilder.AddColumn<int>(
                name: "DealId",
                table: "Proposals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_DealId",
                table: "Proposals",
                column: "DealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Deals_DealId",
                table: "Proposals",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Deals_DealId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_DealId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Proposals");

            migrationBuilder.CreateTable(
                name: "DealHasProposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DealId = table.Column<int>(type: "int", nullable: false),
                    ProposalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealHasProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealHasProposals_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealHasProposals_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DealHasProposals_DealId",
                table: "DealHasProposals",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_DealHasProposals_ProposalId",
                table: "DealHasProposals",
                column: "ProposalId");
        }
    }
}
