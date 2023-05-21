using System.ComponentModel.DataAnnotations;

namespace Read_Excel_File.Areas.Client.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
