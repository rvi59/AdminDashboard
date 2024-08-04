using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoeWeb.Models
{
    public class Category
    {
       
            public int Category_Id { get; set; }

            [DisplayName("Category Name")]
            [Required(ErrorMessage = "Category Name Is Required")]
            public string Category_Name { get; set; }

            public DateTime Category_CreatedDate { get; set; }

           
       
    }
}