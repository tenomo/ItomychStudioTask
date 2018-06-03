using System.ComponentModel.DataAnnotations;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.API.Models
{
    public class BookCreateModel
    { 
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
