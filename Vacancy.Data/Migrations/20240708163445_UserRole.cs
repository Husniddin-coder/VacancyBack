using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vacancy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                schema: "auth",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                schema: "auth",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                schema: "auth",
                table: "users",
                column: "role_id",
                principalSchema: "auth",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                schema: "auth",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_role_id",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role_id",
                schema: "auth",
                table: "users");
        }
    }
}
