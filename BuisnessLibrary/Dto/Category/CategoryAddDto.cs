using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.Category
{
    public class CategoryAddDto
    {

        [Required]
        public string CategoryName { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        [Required]
        public string ImageName { get; set; } = null!;
        [Required]
        public bool ShowInHomePage { get; set; }

    }
        
}
