using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Validation
{
    public class FutureDate : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            return value != null && (DateTime)value >= DateTime.Now; 
        }
    }

}
