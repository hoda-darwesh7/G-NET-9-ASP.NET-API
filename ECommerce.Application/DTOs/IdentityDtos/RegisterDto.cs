using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.IdentityDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public string DisplayName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
