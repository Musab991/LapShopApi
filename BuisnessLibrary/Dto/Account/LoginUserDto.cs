﻿namespace BuisnessLibrary.Dto.Account
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}
