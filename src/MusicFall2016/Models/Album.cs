﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class Album
    {

        public int AlbumID { get; set; }
        [Required(ErrorMessage = "Please enter in a title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please type in price!")]
        [Range(0.01, 100.0, ErrorMessage = "Price must be in between $.01 and $100!")]
        public decimal Price { get; set; }

        // Foreign key
        public int ArtistID { get; set; }
        // Navigation property
        public Artist Artist { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }


    }
}
