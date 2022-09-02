using Euphorolog.Services.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euphorolog.Services.CustomExceptions;

namespace Euphorolog.Services.DTOValidators
{
    public class MainDTOValidator<T> : AbstractValidator<T>
    {
        public virtual void ValidateDTO(T DTO)
        {
            var result = this.Validate(DTO);
            string err = "";
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    err += $"{error.PropertyName}: {error.ErrorMessage}\n";
                }
                throw new BadRequestException(err);
            }
        }
    }
}
