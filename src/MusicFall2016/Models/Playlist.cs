﻿using System.Collections.Generic;
using static MusicFall2016.Models.MusicDbContext;

namespace MusicFall2016.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }

        public ApplicationUser Owner { get; set; }

        public string name { get; set; }

        public List<PlaylistAlbums> Albums { get; set; }
    }
}
