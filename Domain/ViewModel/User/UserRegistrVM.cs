using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class UserRegistrVM
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
