using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AHNet.Web.Migrations
{
    public partial class contenttags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostContentTag",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(nullable: false),
                    ContentTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostContentTag", x => new { x.BlogPostId, x.ContentTagId });
                    table.ForeignKey(
                        name: "FK_BlogPostContentTag_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostContentTag_ContentTags_ContentTagId",
                        column: x => x.ContentTagId,
                        principalTable: "ContentTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostContentTag_BlogPostId",
                table: "BlogPostContentTag",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostContentTag_ContentTagId",
                table: "BlogPostContentTag",
                column: "ContentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTags_Name",
                table: "ContentTags",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostContentTag");

            migrationBuilder.DropTable(
                name: "ContentTags");
        }
    }
}
