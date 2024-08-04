using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using ShoeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoeWeb.Controllers
{
    public class AdminController : Controller
    {
        string Baseurl = "API URL";
        

        List<Brand> BrandLst = new List<Brand>();
        List<Category> CategoryLst = new List<Category>();
        List<Sizze> SizeLst = new List<Sizze>();
        List<User> UserLst = new List<User>();
        List<Product> ProductLst = new List<Product>();
       

        private readonly Cloudinary _cloudinary;

        public AdminController()
        {
            var account = new Account(
                ConfigurationManager.AppSettings["cloudinaryCloudName"],
                ConfigurationManager.AppSettings["cloudinaryApiKey"],
                ConfigurationManager.AppSettings["cloudinaryApiSecret"]);
            _cloudinary = new Cloudinary(account);

        }

        // GET: Admin
        [Authorize]
        public async Task<ActionResult> Dashboard()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Dashboard/GetDashboardCount");

                if (Res.IsSuccessStatusCode)
                {
                   
                    var DashResponse = await Res.Content.ReadAsStringAsync();
                    var dashCount = JsonConvert.DeserializeObject<DashCount>(DashResponse);


                    ViewBag.SportCount = dashCount.SportCount;
                    ViewBag.CasualCount = dashCount.CasualCount;
                    ViewBag.FormalCount = dashCount.FormalCount;
                    ViewBag.CustomerCount = dashCount.CustomerCount;

                    return View();

                }
                else
                {
                    return View();
                }
            } 
        }


        //For BrandList View
        [Authorize]
        public ActionResult BrandList()
        {
            return View();
        }

        //Getting BrandList From Api
        [Authorize]
        public async Task<ActionResult> GetBrandList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Brand/GetAllBrands");

                if (Res.IsSuccessStatusCode)
                {
                    var BrandResponse = Res.Content.ReadAsStringAsync().Result;

                    BrandLst = JsonConvert.DeserializeObject<List<Brand>>(BrandResponse);

                    return Json(BrandLst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var BResponse = "No record Found";
                    return Json(BResponse, JsonRequestBehavior.AllowGet);
                }
                
            }
        }

        [Authorize]
        public ActionResult AddBrand()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddBrand(Brand b)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Brand/PostBrand", b);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("BrandList", "Admin");
                }
                else
                {
                    return View();
                }
            }

        }

        [Authorize]
        public async Task<ActionResult> EditBrand(int id)
        {
            Brand e = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Brand/GetBrandById?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsAsync<Brand>();
                    display.Wait();
                    e = display.Result;
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> EditBrand(Brand b)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("Brand/PutBrand", b);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("BrandList", "Admin");
                }
                else
                {
                    return View(b);
                }
            }

        }

        [Authorize]
        public async Task<ActionResult> DeleteBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("Brand/DeleteBrand?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsStringAsync();
                    display.Wait();

                }
            }
            return RedirectToAction("BrandList", "Admin");


        }

        //----------------------------------------------------//-------------------------------------------------//

        [Authorize]
        public ActionResult CategoryList()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetCategoryList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Category/GetAllCategories");

                if (Res.IsSuccessStatusCode)
                {
                    var CategoryResponse = Res.Content.ReadAsStringAsync().Result;

                    CategoryLst = JsonConvert.DeserializeObject<List<Category>>(CategoryResponse);

                    return Json(CategoryLst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var CResponse = "No record Found";
                    return Json(CResponse, JsonRequestBehavior.AllowGet);
                }


            }
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddCategory(Category c)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Category/PostCategory", c);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList", "Admin");
                }
                else
                {
                    return View(c);
                }
            }
        }

        [Authorize]
        public async Task<ActionResult> EditCategory(int id)
        {
            Category e = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Category/GetCategoryById?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsAsync<Category>();
                    display.Wait();
                    e = display.Result;
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(Category c)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("Category/PutCategory", c);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList", "Admin");
                }
                else
                {
                    return View(c);
                }
            }

        }

        [Authorize]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("Category/DeleteCategory?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsStringAsync();
                    display.Wait();
                }
            }
            return RedirectToAction("CategoryList", "Admin");

        }



        //-------------------------------------------------------//-----------------------------------------------------//

        //For SizeList View
        [Authorize]
        public ActionResult SizeList()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetSizeList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Size/GetAllSize");

                if (Res.IsSuccessStatusCode)
                {
                    var SizeResponse = Res.Content.ReadAsStringAsync().Result;

                    SizeLst = JsonConvert.DeserializeObject<List<Sizze>>(SizeResponse);

                    return Json(SizeLst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var SResponse = "No record Found";
                    return Json(SResponse, JsonRequestBehavior.AllowGet);
                }


                //return View(SizeLst);
            }
        }

        [Authorize]
        public ActionResult AddSize()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddSize(Sizze s)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Size/PostSize", s);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SizeList", "Admin");
                }
                else
                {
                    return View(s);
                }
            }

        }

        [Authorize]
        public async Task<ActionResult> EditSize(int id)
        {
            Sizze e = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Size/GetSizeById?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsAsync<Sizze>();
                    display.Wait();
                    e = display.Result;
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> EditSize(Size s)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("Size/PutSize", s);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SizeList", "Admin");
                }
                else
                {
                    return View(s);
                }
            }

        }

        [Authorize]
        public async Task<ActionResult> DeleteSize(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("Size/DeleteSize?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsStringAsync();
                    display.Wait();

                }
            }
            return RedirectToAction("SizeList", "Admin");

        }


        //-------------------------------------------------------//-----------------------------------------------------//

        [Authorize]
        public ActionResult ProductList()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetProductList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Product/GetAllProduct");

                if (Res.IsSuccessStatusCode)
                {
                    var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                    ProductLst = JsonConvert.DeserializeObject<List<Product>>(ProductResponse);

                    return Json(ProductLst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var PResponse = "No record Found";
                    return Json(PResponse, JsonRequestBehavior.AllowGet);
                }


               
            }
        }

        [Authorize]
        public async Task<ActionResult> AddProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Brand/GetAllBrands");
                HttpResponseMessage Res1 = await client.GetAsync("Category/GetAllCategories");
                HttpResponseMessage Res2 = await client.GetAsync("Size/GetAllSize");


                if (Res.IsSuccessStatusCode)
                {
                    var BrandResponse = Res.Content.ReadAsStringAsync().Result;
                    BrandLst = JsonConvert.DeserializeObject<List<Brand>>(BrandResponse);
                }

                if (Res1.IsSuccessStatusCode)
                {
                    var CategoryResponse = Res1.Content.ReadAsStringAsync().Result;
                    CategoryLst = JsonConvert.DeserializeObject<List<Category>>(CategoryResponse);
                }

                if (Res2.IsSuccessStatusCode)
                {
                    var SizeResponse = Res2.Content.ReadAsStringAsync().Result;
                    SizeLst = JsonConvert.DeserializeObject<List<Sizze>>(SizeResponse);
                }

            }
            ViewBag.BrandId = new SelectList(BrandLst, "Brand_Id", "Brand_Name");
            ViewBag.CatrgoryId = new SelectList(CategoryLst, "Category_Id", "Category_Name");
            ViewBag.SizeId = new SelectList(SizeLst, "Size_Id", "Size_Number");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product p)
        {

            if (ModelState.IsValid == true)
            {
                string myfile = Path.GetFileNameWithoutExtension(p.ImgFile.FileName);
                string extention = Path.GetExtension(p.ImgFile.FileName);
                HttpPostedFileBase postfile = p.ImgFile;
                int length = postfile.ContentLength;

                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg")
                {

                    if (length <= 1000000)
                    {
                        

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(p.ImgFile.FileName, postfile.InputStream)
                        };

                        var uploadResult = _cloudinary.Upload(uploadParams);

                        p.Prod_Image_Path = uploadResult.Url.ToString();

                        var product = new Product
                        {
                            
                            Prod_Name = p.Prod_Name,
                            Prod_ShortName = p.Prod_ShortName,
                            Prod_Price = p.Prod_Price,
                            Prod_Selling = p.Prod_Selling,
                            Prod_Description = p.Prod_Description,
                            BrandId = p.BrandId,
                            SizeId = p.SizeId,
                            CategoryId = p.CategoryId,
                            Prod_Image_Path = p.Prod_Image_Path,
                            Quantity = p.Quantity
                        };


                        using (var client = new HttpClient())
                        {
                           
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = await client.PostAsJsonAsync("Product/PostProduct", product);

                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("ProductList", "Admin");
                            }
                            else
                            {
                                var display = response.Content.ReadAsAsync<Product>();
                                display.Wait();
                                p = display.Result;
                                return View(p);
                            }
                        }

                    }

                }

            }

            return View();
        }

        [Authorize]
        public async Task<ActionResult> EditProduct(int id)
        {
            Product p = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage EditRes = await client.GetAsync("Brand/GetAllBrands");
                HttpResponseMessage EditRes1 = await client.GetAsync("Category/GetAllCategories");
                HttpResponseMessage EditRes2 = await client.GetAsync("Size/GetAllSize");

                if (EditRes.IsSuccessStatusCode)
                {
                    var BrandResponse = EditRes.Content.ReadAsStringAsync().Result;
                    BrandLst = JsonConvert.DeserializeObject<List<Brand>>(BrandResponse);
                }

                if (EditRes1.IsSuccessStatusCode)
                {
                    var CategoryResponse = EditRes1.Content.ReadAsStringAsync().Result;
                    CategoryLst = JsonConvert.DeserializeObject<List<Category>>(CategoryResponse);
                }

                if (EditRes2.IsSuccessStatusCode)
                {
                    var SizeResponse = EditRes2.Content.ReadAsStringAsync().Result;
                    SizeLst = JsonConvert.DeserializeObject<List<Sizze>>(SizeResponse);
                }


                HttpResponseMessage response = await client.GetAsync("Product/GetProductbyId?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsAsync<Product>();
                    display.Wait();
                    p = display.Result;
                    Session["imgprev"] = p.Prod_Image_Path;
                }
            }
            ViewBag.BrandId1 = new SelectList(BrandLst, "Brand_Id", "Brand_Name");
            ViewBag.CategoryId1 = new SelectList(CategoryLst, "Category_Id", "Category_Name");
            ViewBag.SizeId1 = new SelectList(SizeLst, "Size_Id", "Size_Number");

            return View(p);

        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(Product p)
        {

            if (ModelState.IsValid)
            {
                if (p.ImgFile != null)
                {
                    string myfile = Path.GetFileNameWithoutExtension(p.ImgFile.FileName);
                    string extention = Path.GetExtension(p.ImgFile.FileName);
                    HttpPostedFileBase postfile = p.ImgFile;
                    int length = postfile.ContentLength;

                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png")
                    {

                        if (length <= 1000000)
                        {

                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(p.ImgFile.FileName, postfile.InputStream)
                            };

                            var uploadResult = _cloudinary.Upload(uploadParams);

                            p.Prod_Image_Path = uploadResult.Url.ToString();

                            var product = new Product
                            {
                                Prod_Id = p.Prod_Id,
                                Prod_Name = p.Prod_Name,
                                Prod_ShortName = p.Prod_ShortName,
                                Prod_Price = p.Prod_Price,
                                Prod_Selling = p.Prod_Selling,
                                Prod_Description = p.Prod_Description,
                                BrandId = p.BrandId,
                                SizeId = p.SizeId,
                                CategoryId = p.CategoryId,
                                Prod_Image_Path = p.Prod_Image_Path,
                                Quantity = p.Quantity
                            };

                            using (var client = new HttpClient())
                            {

                                client.BaseAddress = new Uri(Baseurl);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response = await client.PutAsJsonAsync("Product/PutProduct", product);

                                if (response.IsSuccessStatusCode)
                                {
                                    string delImag = Session["imgprev"].ToString();

                                    string[] parts = delImag.Split('/');
                                    string lastPart = parts[parts.Length - 1];
                                    string public_id = lastPart.Substring(0, lastPart.LastIndexOf('.'));

                                    var deletionParams = new DeletionParams(public_id);
                                    var result = _cloudinary.Destroy(deletionParams);

                                    return RedirectToAction("ProductList", "Admin");
                                }
                                else
                                {
                                    var display = response.Content.ReadAsAsync<Product>();
                                    display.Wait();
                                    p = display.Result;
                                    return View(p);
                                }
                            }
                        }
                    }
                }
                else
                {

                    p.Prod_Image_Path = Session["imgprev"].ToString();

                    var product = new Product
                    {
                        Prod_Id = p.Prod_Id,
                        Prod_Name = p.Prod_Name,
                        Prod_ShortName = p.Prod_ShortName,
                        Prod_Price = p.Prod_Price,
                        Prod_Selling = p.Prod_Selling,
                        Prod_Description = p.Prod_Description,
                        BrandId = p.BrandId,
                        SizeId = p.SizeId,
                        CategoryId = p.CategoryId,
                        Prod_Image_Path = p.Prod_Image_Path,
                        Quantity = p.Quantity
                    };
                    
                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //HttpResponseMessage response = await client.PutAsJsonAsync("Product/PutProduct?Id="+p.Prod_Id+"", product);
                        HttpResponseMessage response = await client.PutAsJsonAsync("Product/PutProduct", product);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ProductList", "Admin");
                        }
                        else
                        {
                            var display = response.Content.ReadAsAsync<Product>();
                            display.Wait();
                            p = display.Result;
                            return View(p);
                        }
                    }

                }
            }
            return View(p);
        }
        [Authorize]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("Product/DeleteProduct?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(display);
                    var imagePath = product.Prod_Image_Path;

                    string[] parts = imagePath.Split('/');
                    string lastPart = parts[parts.Length - 1];
                    string public_id = lastPart.Substring(0, lastPart.LastIndexOf('.'));

                    var deletionParams = new DeletionParams(public_id);
                    var result = _cloudinary.Destroy(deletionParams);


                    // display.Wait();

                }
            }
            return RedirectToAction("ProductList", "Admin");

        }

        [Authorize]
        public ActionResult CustomerList()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetCustomerList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Customer/GetAllCustomers");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;

                    UserLst = JsonConvert.DeserializeObject<List<User>>(CustomerResponse);

                    return Json(UserLst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var SResponse = "No record Found";
                    return Json(SResponse, JsonRequestBehavior.AllowGet);
                }


                //return View(SizeLst);
            }
        }

        [Authorize]
        public async Task<ActionResult> EditCustomer(int id)
        {
            User e = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Customer/GetCustomerById?id=" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var display = response.Content.ReadAsAsync<User>();
                    display.Wait();
                    e = display.Result;
                }
            }
            return View(e);

        }


        [HttpPost]
        public async Task<ActionResult> EditCustomer(User s)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("Customer/PutCustomers", s);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CustomerList", "Admin");
                }
                else
                {
                    return View(s);
                }
            }

        }

    }
}

