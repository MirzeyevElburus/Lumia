using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }   
}
    