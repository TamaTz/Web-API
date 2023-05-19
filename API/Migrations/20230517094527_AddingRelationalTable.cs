using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelationalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bookings_room_guid",
                table: "tb_tr_bookings",
                column: "room_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations",
                column: "university_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_guid",
                table: "tb_m_account_roles",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid",
                principalTable: "tb_m_roles",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_guid",
                table: "tb_m_accounts",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations",
                column: "university_guid",
                principalTable: "tb_m_universities",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_rooms_room_guid",
                table: "tb_tr_bookings",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_roles_role_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_guid",
                table: "tb_m_accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_rooms_room_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_bookings_room_guid",
                table: "tb_tr_bookings");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles");
        }
    }
}
