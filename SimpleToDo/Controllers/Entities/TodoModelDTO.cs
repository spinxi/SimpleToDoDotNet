using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleToDo.Controllers.Entities
{
    public class TodoModelDTO
    {
        [Required]
        public required string Name { get; set; }
        public int? Status { get; set; }
    }
}
