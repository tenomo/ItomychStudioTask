using System.ComponentModel.DataAnnotations;

namespace ItomychStudioTask.API.Models
{
    public class BookEditModel  
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
