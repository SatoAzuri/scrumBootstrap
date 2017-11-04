using System;
using System.ComponentModel.DataAnnotations;


namespace CityInfo.API.Validation
{
    public class PastDate: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            return dateTime <= DateTime.Now;

        }
    }
}
