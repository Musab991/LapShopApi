using BuisnessLibrary.Bl.Account;
using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;
using System.Data.Entity.Infrastructure;


namespace BuisnessLibrary.Dto.Account
{
    public static class AccountMapper
    {
        public static ApplicationUser ConvertDto(RegisterUserDto user)
        {
            if (user is null)
            {
                throw new NullReferenceException(nameof(user));
            }

           return new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
            };

        }

    }
    }
