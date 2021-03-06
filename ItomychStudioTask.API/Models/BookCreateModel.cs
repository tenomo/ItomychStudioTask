﻿using System.ComponentModel.DataAnnotations;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.API.Models
{
    public class BookCreateModel
    {
        /// <summary>
        /// Title of the book.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Title { get; set; }
        /// <summary>
        /// Description of the book.
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Category id.
        /// </summary>
        [Required]
        public long CategoryId { get; set; }

    }
}
