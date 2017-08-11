using System.Collections.Generic;
using System.Linq;

namespace AglTest.Models
{
	public class Owner
	{
		public string Name { get; set; }

		public string Gender { get; set; }

		public int Age { get; set; }

		public List<Pet> Pets { get; set; }

		public List<Cat> Cats => Pets?.Where(p => p is Cat).Cast<Cat>().ToList() ?? new List<Cat>();

		public bool GenderIs(GenderEnum gender) => Gender.ToLowerInvariant() == gender.ToString().ToLowerInvariant();
	}
}