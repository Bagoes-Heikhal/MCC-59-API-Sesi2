using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddRoleTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "tb_m_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Role", x => x.RoleId);
                });

          

           

            

            migrationBuilder.CreateTable(
                name: "tb_ts_Account_Role",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ts_Account_Role", x => x.AccountRoleId);
                    table.ForeignKey(
                        name: "FK_tb_ts_Account_Role_tb_m_Account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_m_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_tb_ts_Account_Role_AccountNIK",
                table: "tb_ts_Account_Role",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ts_Account_Role_RoleId",
                table: "tb_ts_Account_Role",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_ts_Account_Role");

            migrationBuilder.DropTable(
                name: "tb_m_Role");

        }
    }
}
