using Euphorolog.Services.DTOs.StoriesDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOValidators.StoriesDTOValidators
{
    public class UpdateStoryRequestDTOValidator : MainDTOValidator<UpdateStoryRequestDTO>
    {

        public UpdateStoryRequestDTOValidator()
        {
            RuleFor(s => s.storyTitle)
                .MaximumLength(250);
            RuleFor(s => s.storyDescription)
                .MaximumLength(5000);

        }
    }
}
