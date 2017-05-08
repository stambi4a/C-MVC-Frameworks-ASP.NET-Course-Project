namespace BindingModels.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class EndDateAttribute : ValidationAttribute
    {
        private readonly string startDate;

        public EndDateAttribute(string startDate)
        {
            this.startDate = startDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (EventBindingModel)validationContext.ObjectInstance;
            var startDateInfo =
                model.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(p => p.Name == this.startDate);
            if (startDateInfo == null)
            {
                return new ValidationResult("Start Date has no value");
            }

            var startDateValue = (DateTime)startDateInfo.GetValue(model);
            var endDateValue = (DateTime)value;
            if (endDateValue < startDateValue)
            {
                return new ValidationResult
                    ("The StartDate must be anterior to the EndDate");
            }

            return ValidationResult.Success;
        }
    }
}
