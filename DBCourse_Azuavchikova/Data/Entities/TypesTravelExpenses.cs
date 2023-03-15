
using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.Data.Entities
{
    public class TypesTravelExpenses
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 50 символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Название является обязательным полем")]
        [Range(1, 1000000, ErrorMessage = "Тариф должен быть от 1 до 1 миллиона")]
        public int Rate { get; set; }


        public List<TravelExpenses> TravelExpenses { get; set; }
    }
}
