using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Models
{
    public class TravelExpensesViewModel
    {
        public Guid Id { get; set; }
        public Guid BusinesTripId { get; set; }
        public Guid TypesTravelExpensesId { get; set; }

        public string PurposePayments { get; set; }
        public DateTime DatePayments { get; set; }
        public int SumPayments { get; set; }
        public string NameExpense { get; set; }

        public string TypeExpense { get; set; }
        public string Destination { get; set; }

    }
}
