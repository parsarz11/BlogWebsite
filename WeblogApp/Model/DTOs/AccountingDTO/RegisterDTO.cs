using System.ComponentModel.DataAnnotations;

namespace WeblogApp.Model.DTOs.AccountingDTO
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

        public string domainName { get; set; }
    }
}
