using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddRoleTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "tb_ts_Account_Role",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role",
                column: "RoleId",
                principalTable: "tb_m_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "tb_ts_Account_Role",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role",
                column: "RoleId",
                principalTable: "tb_m_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
