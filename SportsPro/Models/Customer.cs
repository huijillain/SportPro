using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Required.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Required.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Required.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Required.")]
		public string City { get; set; }

		[Required(ErrorMessage = "Required.")]
		public string State { get; set; }

		[Required]
		public string PostalCode { get; set; }

		[Required]
		public string CountryID { get; set; }
		public Country Country { get; set; }

		public string Phone { get; set; }

		[Required(ErrorMessage = "Please enter a valid email address.")]
		public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}