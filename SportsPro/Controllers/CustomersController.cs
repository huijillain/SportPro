﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private SportsProContext context { get; set; }
        public CustomersController(SportsProContext context)
        {
            this.context = context;
        }
        [TempData]
        public string Message { get; set; }

        [Route("Customers")]// Add Route
        public IActionResult Index()
        {
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
        public RedirectToActionResult Edit(Customer customer)
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

