using DBCourse_Azuavchikova.Data.Validation;
using System.ComponentModel.DataAnnotations;

namespace DBCourse_Azuavchikova.Data.Entities
{
    public class TravelExpenses
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Назначение выплат является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назначение выплат должно быть от 3 до 50 символов")]
        public string PurposePayments { get; set; }

        [Required(ErrorMessage = "Дата выплаты является обязательным полем")]
        [CheckDate(ErrorMessage = "Дата выплаты должна быть не больше чем на 10 лет вперед")]
        public DateTime DatePayments { get; set; }

        [Required(ErrorMessage = "Сумма выплаты является обязательным полем")]
        [Range(1, 1000000, ErrorMessage = "Сумма выплаты должна быть от 1 до 1 миллиона")]
        public int SumPayments { get; set; }

        [Required(ErrorMessage = "Наименование расхода является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Наименование выплат должно быть от 3 до 50 символов")]
        public string NameExpense { get; set; }

        [Required(ErrorMessage = "Служебная командировка является обязательным полем")]
        public Guid BusinesTripId { get; set; }
        public BusinesTrip BusinesTrip { get; set; }

        [Required(ErrorMessage = "Вид расходов является обязательным полем")]
        public Guid TypesTravelExpensesId { get; set; }
        public TypesTravelExpenses TypesTravelExpenses { get; set; }
    }
}
