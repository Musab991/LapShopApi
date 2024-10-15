using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.HardDisk
{
    public class AddHardDiskDto
    {
        [Required]
        public int HardDiskId { get; set; }
        [Required]
        public string HardDiskName { get; set; } = null!;

    } 

}
