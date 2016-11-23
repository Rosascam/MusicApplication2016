using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicFall2016.Migrations
{
    public partial class Playlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistID);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistAlbums",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false),
                    PlaylistID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistAlbums", x => new { x.AlbumId, x.PlaylistID });
                    table.ForeignKey(
                        name: "FK_PlaylistAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistAlbums_Playlist_PlaylistID",
                        column: x => x.PlaylistID,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistAlbums_AlbumId",
                table: "PlaylistAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistAlbums_PlaylistID",
                table: "PlaylistAlbums",
                column: "PlaylistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistAlbums");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
