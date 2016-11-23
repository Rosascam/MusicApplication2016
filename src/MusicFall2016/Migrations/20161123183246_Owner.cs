using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class Owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Playlist",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_OwnerId",
                table: "Playlist",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_AspNetUsers_OwnerId",
                table: "Playlist",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_AspNetUsers_OwnerId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_OwnerId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Playlist");
        }
    }
}
