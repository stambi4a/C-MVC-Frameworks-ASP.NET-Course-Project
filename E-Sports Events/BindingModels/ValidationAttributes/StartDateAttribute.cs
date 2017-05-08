namespace BindingModels.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StartDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var result = (DateTime)value;
            if (result < DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
