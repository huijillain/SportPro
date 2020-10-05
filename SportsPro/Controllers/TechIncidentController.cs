using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
            private SportsProContext context { get; set; }

            public TechIncidentController(SportsProContext context)
            {
                this.context = context;
            }

            [HttpGet]
        public ViewResult Get(string activeIncident = "All", string activeTechnician = "All")
        {

            var model = new IncidentViewModel
            {
                //ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                //Incidents = context.Incidents.OrderBy(i => i.Title).ToList(),
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                //Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                // Products = context.Products.OrderBy(p => p.Name).ToList(),

            };
            IQueryable<Incident> query = context.Incidents;
            if (activeIncident != "All")
                query = query.Where(i => i.IncidentID.ToString() == activeIncident);
            if (activeTechnician != "All")
                query = query.Where(i => i.Technician.TechnicianID.ToString() == activeTechnician);
            model.Incidents = query.ToList();
            return View(model);
        }

        [HttpGet]
            public IActionResult List(int id)
            {      // an action method gets & sets a session state value
                int? sessionID = HttpContext.Session.GetInt32("sessionID");
             
                var technician = context.Technicians.Find(id);
                var viewModel = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incidents = context
                        .Incidents
                        .Include(i => i.Customer)
                        .Include(i => i.Product)
                        .Where(i => i.TechnicianID == technician.TechnicianID &&
                                    i.DateClosed == null)
                        .ToList()
                };
                return View(viewModel);
            }

            [HttpGet]
            public IActionResult Edit(int id)
            {
                var i = context.Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .Include(i => i.Technician)
                    .SingleOrDefault(i => i.IncidentID == id);
                return View(i);
            }

            [HttpPost]
            public IActionResult Edit(Incident incident)
            {
                var i = context.Incidents.Find(incident.IncidentID);
                i.Description = incident.Description;
                i.DateClosed = incident.DateClosed;
            // an action method gets & sets a session state value
                HttpContext.Session.SetInt32("sessionID", (int)incident.TechnicianID);

                context.Incidents.Update(i);
                context.SaveChanges(); 
                return RedirectToAction("List", new { id = incident.TechnicianID });
            }
        }
    }
