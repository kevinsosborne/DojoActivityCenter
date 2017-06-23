using System.ComponentModel.DataAnnotations;
using System;
namespace DojoActivityCenter.Models
{
    public class ActivityViewModel: BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "The Title must be at least 2 characters")]
        public string Title {get;set;}
        
        [Required(ErrorMessage = "Please select a time")]
        public DateTime Time {get;set;}
        
        [Required(ErrorMessage = "Please select a date")]
        public DateTime Date {get;set;}
        
        [Required(ErrorMessage = "Please select duration length")]
        public int Duration {get; set;}
        
        [Required(ErrorMessage = "Please select Duration Type")]
        public string DurationType {get; set;}

        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage = "The Description must be at least 10 characters")]
        public string Description {get; set;}
    }
}