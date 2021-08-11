using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;
using WebAPI2.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebAPI2.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        
    }

    public class AccountService:IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                FirstName=dto.FirstName,
                LastName=dto.LastName,
                Nationality = dto.Nationality,
                DateOfBirth = dto.DateOFBirth,
                RoleID = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _dbContext.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
