using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piranha.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piranha_Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    HasArchive = table.Column<bool>(type: "bit", nullable: false),
                    ArchiveTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ArchiveRoute = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_MediaFolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_MediaFolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Route = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Params",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Params", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PostTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Route = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    PublicUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_Media_Piranha_MediaFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Piranha_MediaFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NavigationTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsHiden = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_Pages_Piranha_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Piranha_Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Piranha_Pages_Piranha_PageTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PageTypeFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CLRType = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PageTypeFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_PageTypeFields_Piranha_PageTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_Posts_Piranha_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Piranha_Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Piranha_Posts_Piranha_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Piranha_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Piranha_Posts_Piranha_PostTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PostTypeFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CLRType = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PostTypeFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_PostTypeFields_Piranha_PostTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PageFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PageFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_PageFields_Piranha_Pages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Piranha_Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piranha_PageFields_Piranha_PageTypeFields_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PageTypeFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_PostFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_PostFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_PostFields_Piranha_Posts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Piranha_Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piranha_PostFields_Piranha_PostTypeFields_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_PostTypeFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Categories_Slug",
                table: "Piranha_Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Media_FolderId",
                table: "Piranha_Media",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PageFields_ParentId_TypeId",
                table: "Piranha_PageFields",
                columns: new[] { "ParentId", "TypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PageFields_TypeId",
                table: "Piranha_PageFields",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Pages_AuthorId",
                table: "Piranha_Pages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Pages_Slug",
                table: "Piranha_Pages",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Pages_TypeId",
                table: "Piranha_Pages",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PageTypeFields_TypeId_InternalId",
                table: "Piranha_PageTypeFields",
                columns: new[] { "TypeId", "InternalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PageTypes_InternalId",
                table: "Piranha_PageTypes",
                column: "InternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Params_InternalId",
                table: "Piranha_Params",
                column: "InternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PostFields_ParentId_TypeId",
                table: "Piranha_PostFields",
                columns: new[] { "ParentId", "TypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PostFields_TypeId",
                table: "Piranha_PostFields",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Posts_AuthorId",
                table: "Piranha_Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Posts_CategoryId_Slug",
                table: "Piranha_Posts",
                columns: new[] { "CategoryId", "Slug" });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Posts_TypeId",
                table: "Piranha_Posts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PostTypeFields_TypeId_InternalId",
                table: "Piranha_PostTypeFields",
                columns: new[] { "TypeId", "InternalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_PostTypes_InternalId",
                table: "Piranha_PostTypes",
                column: "InternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Tags_Slug",
                table: "Piranha_Tags",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piranha_Media");

            migrationBuilder.DropTable(
                name: "Piranha_PageFields");

            migrationBuilder.DropTable(
                name: "Piranha_Params");

            migrationBuilder.DropTable(
                name: "Piranha_PostFields");

            migrationBuilder.DropTable(
                name: "Piranha_Tags");

            migrationBuilder.DropTable(
                name: "Piranha_MediaFolders");

            migrationBuilder.DropTable(
                name: "Piranha_Pages");

            migrationBuilder.DropTable(
                name: "Piranha_PageTypeFields");

            migrationBuilder.DropTable(
                name: "Piranha_Posts");

            migrationBuilder.DropTable(
                name: "Piranha_PostTypeFields");

            migrationBuilder.DropTable(
                name: "Piranha_PageTypes");

            migrationBuilder.DropTable(
                name: "Piranha_Authors");

            migrationBuilder.DropTable(
                name: "Piranha_Categories");

            migrationBuilder.DropTable(
                name: "Piranha_PostTypes");
        }
    }
}
