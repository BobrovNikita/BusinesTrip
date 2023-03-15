using DBCourse_Azuavchikova.Data.Validation;
using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.Data.Entities
{
    public class BusinesTrip
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Место является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Место должно быть от 3 до 50 символов")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Цель является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Цель должно быть от 3 до 50 символов")]
        public string Goal { get; set; }

        [Required(ErrorMessage = "Основание является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Основание должно быть от 3 до 50 символов")]
        public string Basis { get; set; }

        [Required(ErrorMessage = "Оценка является обязательным полем")]
        [Range(1, 10, ErrorMessage = "Оценка должен быть от 1 до 10")]
        public int Mark { get; set; }

        [Required(ErrorMessage = "Дата начала является обязательным полем")]
        [CheckDate(ErrorMessage = "Дата начала должна быть не больше чем на 10 лет вперед")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Дата конца является обязательным полем")]
        [CheckDate(ErrorMessage = "Дата конца должна быть не больше чем на 10 лет вперед")]
        public DateTime DateEnd { get; set; }


        [Required(ErrorMessage = "Сотрудник является обязательным полем")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }



        public List<TravelExpenses> TravelExpenses { get; set; }
    }
}
