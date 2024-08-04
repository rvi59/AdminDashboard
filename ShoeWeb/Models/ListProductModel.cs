using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeWeb.Models
{
    public class ListProductModel
    {

        public int Prod_Id { get; set; }
        public string Prod_Name { get; set; }
        public string Prod_ShortName { get; set; }
        public float Prod_Price { get; set; }
        public float Prod_Selling { get; set; }
        public string Prod_Description { get; set; }
        public string Prod_Image_Path { get; set; }

        public string Brand_Name { get; set; }
        public string Category_Name { get; set; }
        public string Size_Number { get; set; }
    }
}