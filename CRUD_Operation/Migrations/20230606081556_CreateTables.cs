using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Operation.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblEmployees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeSalary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployees", x => x.employeeId);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployeeAttendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    isPresent = table.Column<bool>(type: "bit", nullable: false),
                    isAbsent = table.Column<bool>(type: "bit", nullable: false),
                    isOffday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployeeAttendances", x => x.id);
                    table.ForeignKey(
                        name: "FK_TblEmployeeAttendances_TblEmployees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "TblEmployees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEmployeeAttendances_employeeId",
                table: "TblEmployeeAttendances",
                column: "employeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblEmployeeAttendances");

            migrationBuilder.DropTable(
                name: "TblEmployees");
        }
    }
}
