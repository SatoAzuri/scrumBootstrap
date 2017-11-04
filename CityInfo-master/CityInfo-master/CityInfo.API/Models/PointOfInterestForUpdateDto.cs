using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Validation;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"(^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$)", ErrorMessage = "Password restrictions: \n1. Minimum 8 caracters \n2. Must contain at leat one uppercase letter \n3. Must contain at leat one lowercase letter\n4. Must contain at leat one special caracter")]
        public string Password { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]       
        [DateRangeFromToday(7)]
        public DateTime HireDate { get; set; }
    }
}
