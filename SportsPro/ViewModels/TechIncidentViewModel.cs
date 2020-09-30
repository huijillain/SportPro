using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

namespace SportsPro.ViewModels
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; }
        public List<Incident> Incidents { get; set; } 
    }
}
