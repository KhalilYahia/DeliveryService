using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.DTO
{
    public class RegsiterDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "يجب أن يكون طول الاسم بين 3 و30 حرفا")]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The length of password should be more than 6 char")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage = "This must match the password")]
        public string RepeatPassword { get; set; }

        [MaxLength(25, ErrorMessage = "Phone number is so long!")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "UserName should be between 3 and 30 char")]
        public string UserName { get; set; }
    }
}
