using ASP_CORE.HelpValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Dto
{
    public class UserDto
    {
        /// <summary>ID</summary>
        /// <example></example>
        [Required]
        public int Id { get; set; }

        /// <summary>LastName</summary>
        /// <example>Selitsky</example>
        public string LastName { get; set; }
        /// <summary>FirstName</summary>
        /// <example>Danil</example>
        public string FirstName { get; set; }

        /// <summary>Email</summary>
        /// <example>danil@dan.com</example>

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        /// <summary>Password</summary>
        /// <example>123qweASD</example>
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }

        /// <summary>status</summary>
        /// <example>active|passive</example>
        [Required(ErrorMessage = "The status is required")]
        [StatusValidation]
        public string Status { get; set; }

        /// <summary>role</summary>
        /// <example>admin|customer|HR</example>
        [Required(ErrorMessage = "The role is required")]
        [RoleValidation]
        public string Role { get; set; }
    }
}
