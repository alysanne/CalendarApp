using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BucksCalendar.Areas.Identity.Validations
{
    public class RoleAttribute : ValidationAttribute
    {
        public RoleAttribute()
        {
            ValidRoles = new List<string> { "Teacher", "Student" };
        }

        public List<string> ValidRoles { get; }

        public string GetErrorMessage(string role) =>
            $"'{role}' is not a valid role.";
        
        public override bool IsValid(object value)
        {
            var role = (string) value;

            if (ValidRoles.Contains(role))
            {
                return true;
            }

            return false;
        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var role = (string) value;

            if (ValidRoles.Contains(role))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage(role));
        }
    }
}
