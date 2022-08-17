using Euphorolog.Services.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOValidators.AuthDTOValidators
{
    public class SignUpRequestDTOValidator : MainDTOValidator<SignUpRequestDTO>
    {

        public SignUpRequestDTOValidator()
        {
            RuleFor(u => u.userName)
                .NotNull().WithMessage("Username can not be null.")
                .NotEmpty().WithMessage("Username can not be empty.");
            RuleFor(u => u.fullName)
                .NotNull().WithMessage("Fullname can not be null.")
                .NotEmpty().WithMessage("Fullname can not be empty.");
            RuleFor(u => u.eMail)
                .NotNull().WithMessage("Email can not be null.")
                .NotEmpty().WithMessage("Email can not be empty.")
                .EmailAddress().WithMessage("Give a Valid Email");
            RuleFor(u => u.password)
                .NotNull().WithMessage("Password can not be null.")
                .NotEmpty().WithMessage("Password can not be empty")
                .Matches("^[0-9a-zA-Z~`!@#$%^&*._+=|:;<>,?-]+$")
                .When(u => u.password != null).WithMessage("Password can only contain Latin letters and these characters: ~`!@#$%^&*._+=|:;<>,?-");

        }
    }
}
