namespace BindingModels.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StartDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var result = (DateTime)value;
            if (result < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
