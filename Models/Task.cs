using System.ComponentModel.DataAnnotations;

namespace ServiceTestTask.Models
{
    public enum Status
    {
        Новая,
        [Display(Name = "В работе")]
        Вработе,
        Выполнено
    }

    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Worker { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
