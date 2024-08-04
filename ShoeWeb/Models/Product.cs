using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoeWeb.Models
{
    public class Product
    {
        public int Prod_Id { get; set; }


        [DisplayName("Name")]
        [Required(ErrorMessage = "Name Is Required")]
        public string Prod_Name { get; set; }

        [DisplayName("Short Name")]
        [Required(ErrorMessage = "Short Name Is Required")]
        public string Prod_ShortName { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price Is Required")]
        
        public float Prod_Price { get; set; }

        [DisplayName("Selling Price")]
        [Required(ErrorMessage = "Selling Price Is Required")]
        public float Prod_Selling { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description Is Required")]
        public string Prod_Description { get; set; }

        
        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity Is Required")]
        public int Quantity { get; set; }

        [DisplayName("Brand")]
        [Required(ErrorMessage = "Select Brand")]
        public int BrandId { get; set; }       
        public Brand tblBrand { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }
        public Category tblCategory { get; set; }

        [DisplayName("Size")]
        [Required(ErrorMessage = "Select Size")]
        public int SizeId { get; set; }
        public Sizze tblSize { get; set; }

        public DateTime Prod_CreatedDate { get; set; }

        [DisplayName("Image")]
        public string Prod_Image_Path { get; set; }  //For storing Image in Database

        public HttpPostedFileBase ImgFile { get; set; }    //For storing image in folder


        
        public string Brand_Name { get; set; }
        public string Category_Name { get; set; }
        public string Size_Number { get; set; }

    }
}