using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SportsPro.Controllers
{
    public class CustomersController : Controller
    {
        [Authorize(Roles = "Admin")]
        private SportsProContext context { get; set; }

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public CustomersController(UserManager<IdentityUser> userManager,
                                   SignInManager<IdentityUser> signInManager, SportsProContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<JsonResult> IsEmailAvailable(string email)
        {
            var customer = await userManager.FindByEmailAsync(email);
            if (customer == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
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

        [HttpPost]
        public async Task<IActionResult> Add(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(customer, model.Email);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(customer, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
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

