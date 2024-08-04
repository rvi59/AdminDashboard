using Newtonsoft.Json;
using ShoeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ShoeWeb.Controllers
{
    public class AccountController : Controller
    {
        
        string Baseurl = "API URL";
        List<User> UserList = new List<User>();
        

        public ActionResult UserRegistration()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UserRegistration(User u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Account/PostUser", u);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserLogin", "Account");
                }
                else
                {
                    return View(u);
                }
            }
        }


        public ActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginUser u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Account/UserLogin", u);

                if (response.IsSuccessStatusCode)
                {

                    var responseUser = await response.Content.ReadAsAsync<LoginUser>();

                    HttpCookie cookie = new HttpCookie("userlog");
                    if (u.RememberMe == true)
                    {
                        
                        cookie["username"] = u.U_Email;
                        cookie["password"] = u.U_Password;
                        cookie.Expires = DateTime.Now.AddDays(1);

                        HttpContext.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);

                        HttpContext.Response.Cookies.Add(cookie);
                    }

                    Session["usrName"] = responseUser.U_UserName;

                    if (responseUser.UserType == true)
                    {
                        //return RedirectToAction("Index", "Home");
                        return Redirect("http://onlineShooe.somee.com/");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    //return RedirectToAction("UserLogin", "Account");
                    return View();
                    //ModelState.AddModelError("", "Invalid email or password");
                    //return View(u);
                }
            }
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("UserLogin", "Account");
        }

    }
}