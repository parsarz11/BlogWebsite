using System.ComponentModel.DataAnnotations;

namespace WeblogApp.Model
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
