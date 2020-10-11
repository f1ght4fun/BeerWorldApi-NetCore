using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BeerWorld.Extensions
{
    public static class ValidationAssertionExtensions
    {
        public static void AssertIsValid<T>(
            this T obj,
            ValidationContext? context = null,
            bool errorOnNull = true,
            bool validateAllProperties = true
        ) where T : class?
        {
            if (obj == null)
            {
                if (!errorOnNull) return;

                throw new ValidationException($"Object {typeof(T).Name} is uninitialized");
            }

            var errors = new List<ValidationResult>();
            var validationContext = context ?? new ValidationContext(obj);

            if (!Validator.TryValidateObject(obj, validationContext, errors, validateAllProperties))
                throw new ValidationException(string.Join("/r/n", errors.Select(vr => vr.ErrorMessage)));
        }
    }
}
