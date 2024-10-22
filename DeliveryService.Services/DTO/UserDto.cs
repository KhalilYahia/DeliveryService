using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]        
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 30 char")]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "this is a very long phone number ")]
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        [Required]
        public string Roll { get; set; }

       
        

    }
}
