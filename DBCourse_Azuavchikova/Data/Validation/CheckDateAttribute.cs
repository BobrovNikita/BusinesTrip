using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.Data.Validation
{
    public class CheckDateAttribute : ValidationAttribute
    {
        public CheckDateAttribute()
        {

        }
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                var dt = (DateTime)value;

                if (
                        dt.Year >= DateTime.Now.Year-1 &&
                        dt.Year <= DateTime.Now.Year + 10
                    )
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
