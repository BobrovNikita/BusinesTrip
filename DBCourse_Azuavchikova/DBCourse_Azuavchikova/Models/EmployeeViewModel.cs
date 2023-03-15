using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.MVC.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public Guid PositionId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

    }
}
