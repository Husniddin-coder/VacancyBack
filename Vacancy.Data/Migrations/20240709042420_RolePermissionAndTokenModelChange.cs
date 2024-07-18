using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vacancy.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissionAndTokenModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_permissions_permission_id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_roles_role_id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_users_givenby_id",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "token_model",
                newName: "token_model",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "role_permissions",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_role_id",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_permission_id",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_givenby_id",
                schema: "auth",
                table: "role_permissions",
                newName: "IX_role_permissions_givenby_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                schema: "auth",
                table: "role_permissions",
                column: "permission_id",
                principalSchema: "auth",
                principalTable: "permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_roles_role_id",
                schema: "auth",
                table: "role_permissions",
                column: "role_id",
                principalSchema: "auth",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_users_givenby_id",
                schema: "auth",
                table: "role_permissions",
                column: "givenby_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_roles_role_id",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_users_givenby_id",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                schema: "auth",
                table: "role_permissions");

            migrationBuilder.RenameTable(
                name: "token_model",
                schema: "auth",
                newName: "token_model");

            migrationBuilder.RenameTable(
                name: "role_permissions",
                schema: "auth",
                newName: "RolePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_role_id",
                table: "RolePermissions",
                newName: "IX_RolePermissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_permission_id",
                table: "RolePermissions",
                newName: "IX_RolePermissions_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_givenby_id",
                table: "RolePermissions",
                newName: "IX_RolePermissions_givenby_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_permissions_permission_id",
                table: "RolePermissions",
                column: "permission_id",
                principalSchema: "auth",
                principalTable: "permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_roles_role_id",
                table: "RolePermissions",
                column: "role_id",
                principalSchema: "auth",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_users_givenby_id",
                table: "RolePermissions",
                column: "givenby_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
