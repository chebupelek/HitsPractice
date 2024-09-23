using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_Hits_Practice.Migrations
{
    /// <inheritdoc />
    public partial class company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CompanyDbModel_CompanyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyDbModel",
                table: "CompanyDbModel");

            migrationBuilder.RenameTable(
                name: "CompanyDbModel",
                newName: "Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "CompanyDbModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyDbModel",
                table: "CompanyDbModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CompanyDbModel_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "CompanyDbModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
