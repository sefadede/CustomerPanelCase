using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerPanelCase.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullan�c� ad� gereklidir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "�ifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
