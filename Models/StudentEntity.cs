using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First_Migration_Sample.Models
{
    [Table("Students")]
    public class StudentEntity
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public List<string> ClassesEnrolled { get; set; }
    }
}
