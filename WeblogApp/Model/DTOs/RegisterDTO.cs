using System.ComponentModel.DataAnnotations;

namespace WeblogApp.Model.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
