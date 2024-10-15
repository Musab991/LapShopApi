using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.ItemType
{
    public class ItemTypeUpdateDto
    {

        [Required]
        public int ItemTypeId { get; set; }
        [Required]
        public string ItemTypeName { get; set; } = null!;
        [Required]
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        [Required]
        public string ImageName { get; set; } = null!;
       

    }

}
