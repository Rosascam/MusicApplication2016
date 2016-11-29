using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicFall2016.Models
{
    public class MusicDbContext : IdentityDbContext <ApplicationUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlist { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistAlbums>()
            .HasKey(t => new { t.AlbumId, t.PlaylistID });

            modelBuilder.Entity<PlaylistAlbums>()
                .HasOne(pt => pt.Album)
                .WithMany(p => p.Playlists)
                .HasForeignKey(pt => pt.AlbumId);

            modelBuilder.Entity<PlaylistAlbums>()
                .HasOne(pt => pt.Playlist)
                .WithMany(t => t.Albums)
                .HasForeignKey(pt => pt.PlaylistID);


        }
        public class PlaylistAlbums
        {
            public int AlbumId { get; set; }
            public Album Album { get; set; }
            public int PlaylistID { get; set; }
            public Playlist Playlist { get; set; }
        }

    }

}
