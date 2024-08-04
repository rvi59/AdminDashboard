using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoeWeb.Models
{
    public class Brand
    {
        public int Brand_Id { get; set; }

        
        [DisplayName("Brand Name")]
        [Required(ErrorMessage = "Brand Name Is Required")]
        public string Brand_Name { get; set; }

        
        public DateTime Brand_CreatedDate { get; set; }
    }
}