using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.ItempType
{
    public class ItemTypeAddDto
    {

        [Required]
        public string ItemTypeName { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        [Required]
        public string ImageName { get; set; } = null!;

    }

}
