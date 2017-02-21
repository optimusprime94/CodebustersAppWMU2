using System.ComponentModel.DataAnnotations;

namespace CodebustersAppWMU2.Models
{
    /* As you can see, this class is for creating new task. It does not include taskId as it's auto-generated
     * Dto's (Data transfer object) is an object that carries data between processes.  
     */
    public class TaskCreationDto
    {

        [Required]
        [MaxLength(50)]
        public string BeginDateTime { get; set; }
        [MaxLength(50)]
        public string DeadlineDateTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Requirements { get; set; }
    }
}
