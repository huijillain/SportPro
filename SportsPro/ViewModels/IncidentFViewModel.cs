using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

namespace SportsPro.Models
{
    public class IncidentFViewModel
    {
        public List<Incident> Incidents { get; set; }
        public string Filter { get; set; }
    }
}
