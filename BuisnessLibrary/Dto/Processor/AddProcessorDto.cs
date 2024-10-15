using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Processor
{
    public class AddProcessorDto
    {
        [Required]
        public int GpuId { get; set; }
        [Required]
        public string GpuName { get; set; } = null!;

    }
}
