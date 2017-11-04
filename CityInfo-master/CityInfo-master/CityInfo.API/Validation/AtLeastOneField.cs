using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CityInfo.API.Validation
{
    public class AtLeastOneField: ValidationAttribute
    {
        // Have to override IsValid
        public override bool IsValid(object value)
        {

            //  Need to use reflection to get properties of "value"...
            var typeInfo = value.GetType();

            var propertyInfo = typeInfo.GetProperties();

            foreach (var property in propertyInfo)
            {
                if (null != property.GetValue(value, null))
                {
                    // We've found a property with a value
                    return true;
                }
            }

             //All properties were null.
            return false;
        }
    }
}
