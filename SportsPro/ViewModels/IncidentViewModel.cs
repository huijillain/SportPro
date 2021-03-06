﻿using SportsPro.Models;
using System.Collections.Generic;

namespace SportsPro.ViewModels
{
    public class IncidentViewModel
    {
        public List<Customer> Customers { get; set; }
        public string FilterString { get; set; }

        public Incident ActiveIncident { get; set; }
        public Technician ActiveTechnician { get; set; }
        public string Action { get; set; }

        public Incident CurrentIncident { get; set; }

        public List<Product> Products { get; set; }

        private List<Incident> incidents;

        public List<Incident> Incidents
        {
            get => incidents;
            set
            {
                incidents = value;
            }
        }
        public int? TechnicianID { get; set; }

        private List<Technician> technicians;
        public List<Technician> Technicians
        {
            get => technicians;
            set
            {
                technicians = value;
            }
        }
        public string CheckActiveIncident(int i) =>
            i.ToString() == ActiveIncident.ToString() ? "active" : "";
        public string CheckActiveTechnician(int t) =>
            t.ToString() == ActiveTechnician.ToString() ? "active" : "";

    }
}