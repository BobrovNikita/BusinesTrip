namespace DBCourse_Azuavchikova.MVC.Views.Abstractions
{
    public interface IMainView
    {
        event EventHandler LoadPosition;
        event EventHandler LoadEmployee;
        event EventHandler LoadTypesTravelExpenses;
        event EventHandler LoadTravelExpenses;
        event EventHandler LoadBusinesTrip;
    }
}
