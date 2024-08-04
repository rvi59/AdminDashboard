using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeWeb.Models
{
    public class User
    {
       
        public int User_Id { get; set; }

        [DisplayName("Name")]
        public string U_UserName { get; set; }

        public string U_Password { get; set; }

        public string U_ConfirmPassword { get; set; }

        [DisplayName("Email")]
        public string U_Email { get; set; }

        [DisplayName("FirstName")]
        public string U_FirstName { get; set; }

        public string U_LastName { get; set; }

        public bool UserType { get; set; }
        [DisplayName("Is Active")]
        public bool isActive { get; set; }

    }
}