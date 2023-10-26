using System.ComponentModel.DataAnnotations;

namespace StoredProcedure.Models
{
    public class EmployeeViewModel
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}
