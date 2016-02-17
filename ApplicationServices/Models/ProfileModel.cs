using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Models
{
	public class ProfileModel
	{
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool DisplayFullName { get; set; }
		public string Street { get; set; }
		public int Number { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public int Gender { get; set; }
		public string Picture { get; set; }
	}
}
