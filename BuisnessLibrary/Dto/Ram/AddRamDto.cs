using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Ram
{
    public class AddRamDto
    {
        [Required]
        public int RamId { get; set; }
        [Required]
        public int RamSize { get; set; }

    } 

}
