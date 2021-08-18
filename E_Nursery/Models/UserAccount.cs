using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_Nursery.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",ErrorMessage = "Name Can Only Contain Alphabets")]
        public string FirstName { get; set; }
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name Can Only Contain Alphabets")]
        public string LastName { get; set; }
        [RegularExpression("^[6-9]{1}[0-9]{9}$", ErrorMessage = "Please enter a 10 digit valid phone number")]
        public string Phone { get; set; }
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,32}$", ErrorMessage = "Password should contain atleast one uppercase,one lowercase and one number ")]
        // [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{6,32}$", ErrorMessage = "Password should contain atleast one uppercase,one lowercase and one number ")], ErrorMessage = "Password should contain atleast one uppercase,one lowercase and one number ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Recovery Question is required")]
        public string PasswordRecoveryQuestion { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
       [RegularExpression("^[1-9]{1}[0-9]{5}$", ErrorMessage = "Please Enter A Valid 6-Digit Pincode")]
        public string Pincode { get; set; }


        public string Status { get; set; }
    }
}