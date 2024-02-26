using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleToDo.Controllers.Entities
{
    public class ToDoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public int? Status { get; set; }
        
        public DateTime CreateDate { get; set; }


    }
}
