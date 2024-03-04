using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeesHead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeesStructure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    FeesHeadId = table.Column<int>(type: "int", nullable: false),
                    IsFixOrPercentage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    MinValue = table.Column<double>(type: "float", nullable: true),
                    MaxValue = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeesStructure_FeesHead",
                        column: x => x.FeesHeadId,
                        principalTable: "FeesHead",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeesStructure_FeesStructure",
                        column: x => x.TypeId,
                        principalTable: "VehicleType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeesStructure_FeesHeadId",
                table: "FeesStructure",
                column: "FeesHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesStructure_TypeId",
                table: "FeesStructure",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeesStructure");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FeesHead");

            migrationBuilder.DropTable(
                name: "VehicleType");
        }
    }
}
