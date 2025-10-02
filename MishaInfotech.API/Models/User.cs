using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MishaInfotech.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Hobbies { get; set; }
        public string? Image { get; set; }
        public bool Terms { get; set; }
        [NotMapped]
        public string? StateName { get; set; }
    }
}
