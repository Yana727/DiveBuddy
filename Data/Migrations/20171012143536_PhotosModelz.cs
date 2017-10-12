using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiveBuddy.Data.Migrations
{
    public partial class PhotosModelz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotosModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApplicationUserID = table.Column<string>(type: "text", nullable: true),
                    BuisnessId = table.Column<int>(type: "int4", nullable: false),
                    PicName = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosModel_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotosModel_BuisnessModel_BuisnessId",
                        column: x => x.BuisnessId,
                        principalTable: "BuisnessModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotosModel_ApplicationUserID",
                table: "PhotosModel",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosModel_BuisnessId",
                table: "PhotosModel",
                column: "BuisnessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotosModel");
        }
    }
}
