using System.ComponentModel.DataAnnotations;

namespace MishaInfotech.API.Models
{
	public class States
	{
		[Key]
		public int StateId { get; set; }
		public string Name { get; set; }

	}
}
