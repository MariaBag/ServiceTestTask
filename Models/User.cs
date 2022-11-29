using System.ComponentModel.DataAnnotations;

namespace ServiceTestTask.Models
{
    public enum Role
    {
        Заказчик, Исполнитель
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
