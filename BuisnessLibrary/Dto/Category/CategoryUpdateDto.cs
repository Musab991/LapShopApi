using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.Category
{
    public class CategoryUpdateDto
    {

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        [Required]
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        [Required]
        public string ImageName { get; set; } = null!;
        [Required]
        public bool ShowInHomePage { get; set; }

    }
        
}
