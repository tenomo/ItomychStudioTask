using System.ComponentModel.DataAnnotations;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.API.Models
{
    public class BookEditModel : Book
    {
        [Required]
        public override long Id { get; set; }
    }
}
