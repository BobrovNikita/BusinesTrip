using DBCourse_Azuavchikova.Data.Validation;
using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.Data.Entities
{
    public class Position
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Наименование является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Наименование должно быть от 3 до 50 символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Дата ввода является обязательным полем")]
        [CheckDate(ErrorMessage = "Дата должна быть не больше чем на 10 лет вперед")]
        public DateTime DateEnter { get; set; }



        public List<Employee> Employees { get; set; }
    }
}
