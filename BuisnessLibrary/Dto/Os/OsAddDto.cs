using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.Os
{
    public class OsAddDto
    {

        [Required]
        public string OsName { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool CurrentState { get; set; }
        public string ?ImageName { get; set; } 

    }

}
