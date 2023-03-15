using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.Data.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Фамилия является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Фамилия должно быть от 3 до 50 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Имя является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Отчество является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Отчество должно быть от 3 до 50 символов")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Должность является обязательным полем")]
        public Guid PositionId { get; set; }
        public Position Position { get; set; }


        public List<BusinesTrip> BusinesTrips { get; set; }
    }
}
