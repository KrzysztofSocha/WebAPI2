using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Entities;

namespace WebAPI2.Models.Validators
{

    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.DateOFBirth)
                .NotEmpty();
            RuleFor(x => x.RoleId)
                .NotEmpty();
            RuleFor(x => x.Nationality)
                .NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(u => u.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
             {
                 var emailInUse=dbContext.Users.Any(u => u.Email == value);
                 if (emailInUse)
                 {
                     context.AddFailure("Email", "That email is taken");
                 }
             });

        }
    }
}
