using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.Os
{
    public class OsUpdateDto
    {

        [Required]
        public int OsId { get; set; }
        [Required]
        public string OsName { get; set; } = null!;
        [Required]
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        public string ?ImageName { get; set; }
       

    }

}
