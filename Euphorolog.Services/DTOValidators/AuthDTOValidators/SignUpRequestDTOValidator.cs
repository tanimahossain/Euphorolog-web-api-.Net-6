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
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[a-z]+[0-9a-z]+$").WithMessage("Only use latin letters or digits, starting with a letter");
            RuleFor(u => u.fullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(u => u.eMail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(400);
            RuleFor(u => u.password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(32)
                .Matches("^[0-9a-zA-Z~`!@#$%^&*._+=|:;<>,?-]+$")
                .When(u => u.password != null).WithMessage("Password can only contain Latin letters and these characters: ~`!@#$%^&*._+=|:;<>,?-");

        }
    }
}
