using Euphorolog.Services.DTOs.UsersDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOValidators.UsersDTOValidators
{
    public class UpdateUserInfoRequestDTOValidators : MainDTOValidator<UpdateUserInfoRequestDTO>
    {

        public UpdateUserInfoRequestDTOValidators()
        {
            RuleFor(u => u.fullName)
                .MaximumLength(250);
            RuleFor(u => u.password)
                .MinimumLength(8)
                .MaximumLength(32)
                .Matches("^[0-9a-zA-Z~`!@#$%^&*._+=|:;<>,?-]+$")
                .When(u => u.password != null).WithMessage("Password can only contain Latin letters and these characters: ~`!@#$%^&*._+=|:;<>,?-");

        }
    }
}
