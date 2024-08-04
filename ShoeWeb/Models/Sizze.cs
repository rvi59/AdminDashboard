using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoeWeb.Models
{
    public class Sizze
    {
        public int Size_Id { get; set; }

        [DisplayName("Size")]
        [Required(ErrorMessage = "Size Is Required")]
        public int Size_Number { get; set; }
        public DateTime Size_CreatedDate { get; set; }
    }
}