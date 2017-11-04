using System;
using System.ComponentModel.DataAnnotations;


namespace CityInfo.API.Validation
{
    public class DateRangeFromToday: RangeAttribute
    {
        public DateRangeFromToday(int until)            
        : base(typeof(DateTime), DateTime.Today.ToString(), DateTime.Today.AddDays(until-1).ToString())              
        {  }
    }
}
