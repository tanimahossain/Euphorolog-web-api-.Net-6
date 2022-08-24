using Euphorolog.Services.DTOs.StoriesDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOValidators.StoriesDTOValidators
{
    public class PostStoryRequestDTOValidator : MainDTOValidator<PostStoryRequestDTO>
    {

        public PostStoryRequestDTOValidator()
        {
            RuleFor(s => s.storyTitle)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(s => s.storyDescription)
                .NotNull()
                .NotEmpty()
                .MaximumLength(5000);

        }
    }
}
