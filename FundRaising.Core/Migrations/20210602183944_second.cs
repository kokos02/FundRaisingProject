using Microsoft.EntityFrameworkCore.Migrations;

namespace FundRaising.Core.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Rewards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rewards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    FundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.FundId);
                    table.ForeignKey(
                        name: "FK_Funds_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funds_ProjectId",
                table: "Funds",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Funds_UserId",
                table: "Funds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rewards");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Rewards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
