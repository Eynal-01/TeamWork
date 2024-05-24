using System.ComponentModel.DataAnnotations;

namespace TeamWork.ClientApp.Models
{

        public class RegisterViewModel
        {
            [Required]
            public string? Username { get; set; }
            [Required(ErrorMessage = "Please enter a password")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }
        public string ?City { get; set; }
        public int SeriaNo { get; set; }
        public DateTime DateTime { get; set; }

    }
    
}
