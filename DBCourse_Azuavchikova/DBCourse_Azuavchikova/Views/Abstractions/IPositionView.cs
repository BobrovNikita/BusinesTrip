using DBCourse_Azuavchikova.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Views.Abstractions
{
    public interface IPositionView
    {
        Guid Id { get; set; }
        string NamePosition { get; set; }
        DateTime DateEnter { get; set; }

        string searchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetPositionBindingSource(BindingSource source);
        void Show();
    }
}
