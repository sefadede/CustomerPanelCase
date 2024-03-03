using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerPanelCase.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanýcý adý gereklidir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Þifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
