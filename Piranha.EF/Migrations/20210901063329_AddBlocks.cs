using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piranha.EF.Migrations
{
    public partial class AddBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piranha_Pages_Piranha_PageTypes_PageTypeId",
                table: "Piranha_Pages");

            migrationBuilder.AlterColumn<string>(
                name: "PageTypeId",
                table: "Piranha_Pages",
                type: "nvarchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "TypeId",
                table: "Piranha_Pages",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Piranha_BlockTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_BlockTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Blocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    View = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_Blocks_Piranha_BlockTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_BlockTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_BlockFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FieldId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CLRType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_BlockFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_BlockFields_Piranha_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Piranha_Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_BlockFields_BlockId_RegionId_FieldId_SortOrder",
                table: "Piranha_BlockFields",
                columns: new[] { "BlockId", "RegionId", "FieldId", "SortOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Blocks_TypeId",
                table: "Piranha_Blocks",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piranha_Pages_Piranha_PageTypes_PageTypeId",
                table: "Piranha_Pages",
                column: "PageTypeId",
                principalTable: "Piranha_PageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piranha_Pages_Piranha_PageTypes_PageTypeId",
                table: "Piranha_Pages");

            migrationBuilder.DropTable(
                name: "Piranha_BlockFields");

            migrationBuilder.DropTable(
                name: "Piranha_Blocks");

            migrationBuilder.DropTable(
                name: "Piranha_BlockTypes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Piranha_Pages");

            migrationBuilder.AlterColumn<string>(
                name: "PageTypeId",
                table: "Piranha_Pages",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Piranha_Pages_Piranha_PageTypes_PageTypeId",
                table: "Piranha_Pages",
                column: "PageTypeId",
                principalTable: "Piranha_PageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
