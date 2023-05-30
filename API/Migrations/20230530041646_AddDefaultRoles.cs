using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_guid",
                table: "tb_m_account_roles");

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("3fb23dae-feed-41fe-2f6b-08db60bf1e9a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("40ac62a2-392e-4eb2-2f69-08db60bf1e9a"), new DateTime(2023, 5, 30, 11, 16, 46, 413, DateTimeKind.Local).AddTicks(1625), new DateTime(2023, 5, 30, 11, 16, 46, 413, DateTimeKind.Local).AddTicks(1634), "User" },
                    { new Guid("ec576a69-d7ae-42a7-2f6a-08db60bf1e9a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_account_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("3fb23dae-feed-41fe-2f6b-08db60bf1e9a"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("40ac62a2-392e-4eb2-2f69-08db60bf1e9a"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ec576a69-d7ae-42a7-2f6a-08db60bf1e9a"));

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_guid",
                table: "tb_m_account_roles",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
