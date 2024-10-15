using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Dto.Account
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]

        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword  { get; set; } = null!;
        public string Email{ get; set; }


    } 
}
