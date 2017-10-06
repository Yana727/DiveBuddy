using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiveBuddy.Data.Migrations
{
    public partial class AddBusinessType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "BuisnessModel",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "BuisnessModel");

            migrationBuilder.CreateTable(
                name: "PhotosModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    BuisnessId = table.Column<int>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false)
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
    }
}
