using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Gpu
{
    public class AddGpuDto
    {
        [Required]
        public int GpuId { get; set; }
        [Required]
        public string GpuName { get; set; } = null!;

    } 

}
