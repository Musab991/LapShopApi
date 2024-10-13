
namespace BuisnessLibrary.Dto.ItempType
{
    public class ItemTypeDto
    {

        [Required]
        public int ItemTypeId { get; set; }
        [Required]
        public string ItemTypeName { get; set; } = null!;
        [Required]
        public string ImageName { get; set; } = null!;
        [Required]
        public bool CurrentState { get; set; }


    }

}
