using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Account_AccountNIK",
                table: "tb_ts_Account_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Account_NIK",
                table: "tb_t_Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Education_EducationId",
                table: "tb_t_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_Profiling",
                table: "tb_t_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_ts_Account_Role",
                table: "tb_ts_Account_Role");

            migrationBuilder.RenameTable(
                name: "tb_t_Profiling",
                newName: "tb_t_Profiling");

            migrationBuilder.RenameTable(
                name: "tb_ts_Account_Role",
                newName: "tb_t_Account_Role");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_Profiling_EducationId",
                table: "tb_t_Profiling",
                newName: "IX_tb_t_Profiling_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_ts_Account_Role_RoleId",
                table: "tb_t_Account_Role",
                newName: "IX_tb_t_Account_Role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_ts_Account_Role_AccountNIK",
                table: "tb_t_Account_Role",
                newName: "IX_tb_t_Account_Role_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_Profiling",
                table: "tb_t_Profiling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_Account_Role",
                table: "tb_t_Account_Role",
                column: "AccountRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Account_Role_tb_m_Account_AccountNIK",
                table: "tb_t_Account_Role",
                column: "AccountNIK",
                principalTable: "tb_m_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Account_Role_tb_m_Role_RoleId",
                table: "tb_t_Account_Role",
                column: "RoleId",
                principalTable: "tb_m_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Account_NIK",
                table: "tb_t_Profiling",
                column: "NIK",
                principalTable: "tb_m_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Education_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId",
                principalTable: "tb_m_Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Account_Role_tb_m_Account_AccountNIK",
                table: "tb_t_Account_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Account_Role_tb_m_Role_RoleId",
                table: "tb_t_Account_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Account_NIK",
                table: "tb_t_Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Education_EducationId",
                table: "tb_t_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_Profiling",
                table: "tb_t_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_Account_Role",
                table: "tb_t_Account_Role");

            migrationBuilder.RenameTable(
                name: "tb_t_Profiling",
                newName: "tb_t_Profiling");

            migrationBuilder.RenameTable(
                name: "tb_ts_Account_Role",
                newName: "tb_t_Account_Role");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_Profiling_EducationId",
                table: "tb_t_Profiling",
                newName: "IX_tb_t_Profiling_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_Account_Role_RoleId",
                table: "tb_ts_Account_Role",
                newName: "IX_tb_ts_Account_Role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_Account_Role_AccountNIK",
                table: "tb_ts_Account_Role",
                newName: "IX_tb_ts_Account_Role_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_Profiling",
                table: "tb_t_Profiling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_ts_Account_Role",
                table: "tb_ts_Account_Role",
                column: "AccountRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Account_AccountNIK",
                table: "tb_ts_Account_Role",
                column: "AccountNIK",
                principalTable: "tb_m_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ts_Account_Role_tb_m_Role_RoleId",
                table: "tb_ts_Account_Role",
                column: "RoleId",
                principalTable: "tb_m_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Account_NIK",
                table: "tb_t_Profiling",
                column: "NIK",
                principalTable: "tb_m_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_m_Education_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId",
                principalTable: "tb_m_Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
