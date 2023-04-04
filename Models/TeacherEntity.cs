using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First_Migration_Sample.Models
{
    public class TeacherEntity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //new property to our `Teacher` entity called `Gender`
        public string Gender { get; set; }

        [NotMapped]
        public List<string> ClassesTaught { get; set; }
    }
}
