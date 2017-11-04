using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CityInfo.API.Validation;
using CityInfo.API.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.ViewModels
{
    
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Boss { get; set; }         
        public string Member1 { get; set; }
        public string Member2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public int Overtime1 { get; set; }
        public int Overtime2 { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DateRangeFromToday(7)]
        public DateTime Date { get; set; }  
        
        [MaxLength(250, ErrorMessage ="Comment is too Long")]
        public string Comment { get; set; }
        
        public string Reason { get; set; }



    }
}
