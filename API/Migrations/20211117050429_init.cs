using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Employee", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_Account_tb_m_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Education",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Education", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_tb_m_Education_tb_m_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_m_University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_Profiling_tb_m_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_Profiling_tb_m_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_m_Education",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Education_UniversityId",
                table: "tb_m_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_Profiling_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_Profiling");

            migrationBuilder.DropTable(
                name: "tb_m_Account");

            migrationBuilder.DropTable(
                name: "tb_m_Education");

            migrationBuilder.DropTable(
                name: "tb_m_Employee");

            migrationBuilder.DropTable(
                name: "tb_m_University");
        }
    }
}
