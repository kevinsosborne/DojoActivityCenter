using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoActivityCenter.Models;
using System.Linq;

namespace DojoActivityCenter.Controllers
{
    public class LoginController : Controller
    {
        private CenterContext _context;

        public LoginController(CenterContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel validation)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = validation.FirstName,
                    LastName = validation.LastName,
                    Email = validation.Email,
                    Password = validation.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            HttpContext.Session.SetInt32("user_id", newUser.UserId);
            return RedirectToAction("Home", "Action");
                
            }
            else{
                ViewBag.Errors = ModelState.Values;
                return View("index");
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            List<User> ReturnedEmail = _context.Users.Where(user => user.Email == Email).ToList();
            if(ReturnedEmail.Count > 0)
            {
                if(ReturnedEmail[0].Password == Password)
                {
                    HttpContext.Session.SetInt32("user_id", ReturnedEmail[0].UserId);
                    return RedirectToAction("Home","Action");
                }
            }
            ViewBag.Errors = new List<string>();
            ViewBag.EmailErrors = "Invalid Combination";
            return View("index");
        }
    }
}
