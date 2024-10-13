
namespace BuisnessLibrary.Dto.Os
{
    public class OsDto
    {

        [Required]
        public int OsId { get; set; }
        [Required]
        public string OsName { get; set; } = null!;
        public string? ImageName { get; set; } 
        [Required]
        public bool CurrentState { get; set; }


    }

}
