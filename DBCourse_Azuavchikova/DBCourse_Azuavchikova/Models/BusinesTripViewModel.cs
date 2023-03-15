using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Models
{
    public class BusinesTripViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Destination { get; set; }
        public string Goal { get; set; }
        public string Basis { get; set; }
        public int Mark { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
    }
}
