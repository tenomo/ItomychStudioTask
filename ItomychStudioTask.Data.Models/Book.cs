using System.ComponentModel.DataAnnotations;

namespace ItomychStudioTask.Data.Models
{
    public class Book
    {
        public virtual long Id { get; set; }
        [Required]
        [StringLength(maximumLength:30, MinimumLength = 1)]
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
