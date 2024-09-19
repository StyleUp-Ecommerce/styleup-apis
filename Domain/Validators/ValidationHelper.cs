using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Validators.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public static class ValidationHelper
    {
        public static void ThrowIfValidationFails<T>(this IValidator<T> validator, T entity)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                var errorDetails = validationResult.Errors.Select(error => new ErrorCodeDetail
                {
                    Code = $"Invalid field {error.PropertyName}",
                    Message = error.ErrorMessage,
                }).ToList();

                throw new DomainException(
                    message: "Validation data failed.",
                    errorCode: "VALIDATION_DATA_FAILED",
                    httpCode: 400,
                    details: errorDetails
                );
            }
        }
    }
}
