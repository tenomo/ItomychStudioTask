using System;
using System.ComponentModel.DataAnnotations;

namespace ItomychStudioTask.API.Models
{
    public class PaginationModel
    {
        private int _page;

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Page
        {
            get { return _page; }
            set { _page = value -1; }
        }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Rows { get; set; }
    }
}
