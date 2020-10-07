using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SportsPro.Controllers
{
    public class CustomersController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomersController(SportsProContext context)
        {
            this.context = context;
        }

        //private readonly UserManager<IdentityUser> customerManager;
        //public CustomersController(UserManager<IdentityUser> customerManager)
        //{
        //    this.customerManager = customerManager;
        //}

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> IsEmailInUse(string email)
        //{
        //    var customer = await Customer.FindByEmailAsync(email);
        //    if (customer == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Email {email} is already in use.");
        //    }
        //}


        //from ASP.NET book with remote validation
        //public JsonResult CheckEmail(string email, int CustomerID)
        //{
        //    bool hasEmail = Utility.CheckEmail(email);
        //    if (hasEmail)
        //        return Json($"Email address {email} is already registered.");
        //    else
        //        return Json(true);
        //}

        //public JsonResult CheckEmail(string emailAddress)
        //{
        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        TempData["okEmail"] = true;
        //        return Json(true);
        //    }
        //    else return Json(msg);
        //}

        public static class Check
        {
            public static string EmailExits(SportsProContext ctx, string email)
            {
                string msg = "";
                if (!string.IsNullOrEmpty(email))
                {
                    var customer = ctx.Customers.FirstOrDefault(
                        c => c.Email.ToLower() == email.ToLower());
                    if (customer != null)
                        msg = $"Email address {email} aleready in use.";
                }
                return msg;
            }
        }


        [TempData]
        public string Message { get; set; }

        [Route("Customers")]// Add Route
        public IActionResult Index()
        {
            ViewBag.Action = "Edit";
            var Customers = context.Customers.OrderBy(g => g.FirstName).ToList();
            return View(Customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    Message = $"Added Customer {customer.FullName}";
                    context.Customers.Add(customer);
                }
                else
                {
                    Message = $"Edited Customer {customer.FullName}";
                    context.Customers.Update(customer);
                }

                context.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
                return View(customer);
            }
    }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            Message = $"Deleted Customer {customer.FullName}";
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}

