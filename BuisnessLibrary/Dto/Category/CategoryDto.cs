using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Category
{
    public class CategoryDto
    {

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        [Required]
        public string ImageName { get; set; } = null!;
        [Required]
        public bool CurrentState { get; set; }


    }

}
