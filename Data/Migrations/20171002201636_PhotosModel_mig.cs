using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiveBuddy.Data.Migrations
{
    public partial class PhotosModel_mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotosModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "text", nullable: true),
                    BuisnessId = table.Column<int>(type: "int4", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Height = table.Column<int>(type: "int4", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<int>(type: "int4", nullable: false)
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
