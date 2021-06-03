using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFirstWebAPI.Models;

namespace TheFirstWebAPI.Services
{
    //FluentValidation
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            //RuleFor(user => user.Id).NotNull();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Phone).NotEmpty().Length(13);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
        }
    }
}
