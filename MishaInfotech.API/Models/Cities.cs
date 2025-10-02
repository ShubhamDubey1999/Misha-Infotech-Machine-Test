using System.ComponentModel.DataAnnotations;

namespace MishaInfotech.API.Models
{
	public class Cities
	{
		[Key]
		public int CityId { get; set; }
		public int StateId { get; set; }
		public string Name { get; set; }
	}
}
