using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductsController : Controller
    {
        private SportsProContext context { get; set; }

        public ProductsController(SportsProContext context)
        {
            this.context = context;
        }

        [Route("Products")] //Add Route
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Products = context.Products.ToList();
            return View("Edit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Products = context.Products.ToList();
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {  // TempData to display messages when openration is successful
                    TempData["message"] = $"{product.Name} was added.";
                    context.Products.Add(product);
                }
                else
                { // TempData to display messages when openration is successful
                    TempData["message"] = $"{product.Name} was edited.";
                    context.Products.Update(product);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Products = context.Products.ToList();
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            TempData["message"] = $"Deleted Product {product.Name}.";
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Products");              // Redirect
        }
    }

}
