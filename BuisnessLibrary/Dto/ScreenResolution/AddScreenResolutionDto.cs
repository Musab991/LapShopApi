using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.ScreenResolution
{
    public class AddScreenResolutionDto
    {
        [Required]
        public int ScreenResolutionId { get; set; }
        [Required]
        public string ScreenResolutionName { get; set; } = null!;

    } 

}
