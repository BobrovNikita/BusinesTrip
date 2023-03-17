using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;

namespace DBCourse_Azuavchikova.MVC.Views.Abstractions
{
    public interface ITravelExpensesView
    {
        Guid Id { get; set; }
        BusinesTripViewModel BusinesTrip { get; set; }
        TypesTravelExpenses TypesTravelExpenses { get; set; }
        string PurposePayments { get; set; }
        DateTime DatePayments { get; set; }
        int SumPayments { get; set; }
        string NameExpense { get; set; }


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

        void SetTravelExpensesViewBindingSource(BindingSource source);
        void SetBusinesTripBindingSource(BindingSource source);
        void SetTypeTravelExpensesBindingSource(BindingSource source);
        void Show();
    }
}
