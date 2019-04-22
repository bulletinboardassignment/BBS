using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EBBS.Data;

namespace EBBS.Models
{

    [Table("User")]
    public class LoginViewModel
    {

        [Key]
        public int UserId { get; set; }
       
     
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is Required")]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string username { get; set; }

        

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }



        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }


    [Table("User")]
    public class RegisterViewModel
    {


        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string firstname { get; set; }


        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string lastname { get; set; }


        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is Required")]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string username { get; set; }

        [Required(ErrorMessage = "Security Question is Required")]
        [DisplayName("Security Question")]
        public int questionId { get; set; }


        [DisplayName("Answer")]
        [Required(ErrorMessage = "Answer is Required")]
        [StringLength(50, MinimumLength = 2)]
        public string answer { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}
