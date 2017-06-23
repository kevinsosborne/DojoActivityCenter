using System.ComponentModel.DataAnnotations;
using System;
namespace DojoActivityCenter.Models
{
    public class RegisterViewModel: BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must be all letters")]
        public string FirstName {get;set;}
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must be all letters")]
        public string LastName {get;set;}
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email {get;set;}
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [Required(ErrorMessage = "Confirmation password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation must match!")]
        public string Confirm_password {get; set;}
    }
}