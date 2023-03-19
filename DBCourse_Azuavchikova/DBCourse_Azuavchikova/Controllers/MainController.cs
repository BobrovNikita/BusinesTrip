using DBCourse_Azuavchikova.MVC.Views.Abstractions;
using DBCourse_Azuavchikova.MVC.Views;
using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Repositories;

namespace DBCourse_Azuavchikova.MVC.Controllers
{
    public class MainController
    {
        private readonly IMainView _mainView;

        public MainController(IMainView mainView)
        {
            _mainView = mainView;

            _mainView.LoadPosition += LoadPosition;
            _mainView.LoadEmployee += LoadEmployee;
            _mainView.LoadBusinesTrip += LoadBusinesTrip;
            _mainView.LoadTravelExpenses += LoadTravelExpenses;
            _mainView.LoadTypesTravelExpenses += LoadTypesTravelExpenses;
        }

        private void LoadPosition(object? sender, EventArgs e)
        {
            IPositionView view = PositionView.GetInstance((MainView)_mainView);
            IPositionRepository repository = new PositionRepository(new Data.BusinesTripsPayments());
            new PositionController(view, repository);
        }

        private void LoadEmployee(object? sender, EventArgs e)
        {
            IEmployeeView view = EmployeeView.GetInstance((MainView)_mainView);
            IEmployeeRepository repository = new EmployeeRepository(new Data.BusinesTripsPayments());
            IPositionRepository positionRepository = new PositionRepository(new Data.BusinesTripsPayments());
            new EmployeeController(view, repository, positionRepository);
        }

        private void LoadBusinesTrip(object? sender, EventArgs e)
        {
            IBusinesTripView view = BusinesTripView.GetInstance((MainView)_mainView);
            IBusinesTripRepository repository = new BusinesTripRepository(new Data.BusinesTripsPayments());
            IEmployeeRepository employeeRepository = new EmployeeRepository(new Data.BusinesTripsPayments());
            new BusinesTripController(view, repository, employeeRepository);
        }

        private void LoadTravelExpenses(object? sender, EventArgs e)
        {
            ITravelExpensesView view = TravelExpensesView.GetInstance((MainView)_mainView);
            ITravelExpensesRepository repository = new TravelExpensesRepository(new Data.BusinesTripsPayments());
            IBusinesTripRepository businesTripRepository = new BusinesTripRepository(new Data.BusinesTripsPayments());
            ITypesTravelExpensesRepository typeTravelRepository = new TypeTravelExpensesRepository(new Data.BusinesTripsPayments());
            new TravelExpensesController(view, repository, businesTripRepository, typeTravelRepository);
        }

        private void LoadTypesTravelExpenses(object? sender, EventArgs e)
        {
            ITypeTravelExpensesView view = TypesTravelExpensesView.GetInstance((MainView)_mainView);
            ITypesTravelExpensesRepository repository = new TypeTravelExpensesRepository(new Data.BusinesTripsPayments());
            new TypesTravelExpensesController(view, repository);
        }
    }
}
