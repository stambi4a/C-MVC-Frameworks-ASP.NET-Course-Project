namespace BindingModels.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateOfBirth : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var result = (DateTime)value;
            if (result > DateTime.Today.AddYears(-14) || result <= DateTime.Today.AddYears(-70))
            {
                return false;
            }

            return true;
        }
    }
}
