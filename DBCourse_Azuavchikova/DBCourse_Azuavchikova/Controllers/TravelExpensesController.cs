using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;

namespace DBCourse_Azuavchikova.MVC.Controllers
{
    public class TravelExpensesController
    {
        private readonly ITravelExpensesView _view;
        private readonly ITravelExpensesRepository _repository;
        private readonly IBusinesTripRepository _businesTripRepository;
        private readonly ITypesTravelExpensesRepository _typeTravelEexpensesRepository;

        private BindingSource travelExpensesBindingSource;
        private BindingSource businesTripBindingSource;
        private BindingSource typeTravelBindindSource;

        private IEnumerable<TravelExpensesViewModel>? _travelExpenses;
        private IEnumerable<BusinesTripViewModel>? _businesTrips;
        private IEnumerable<TypesTravelExpenses>? _typeTravelExpenses;

        public TravelExpensesController(ITravelExpensesView view, ITravelExpensesRepository repository, IBusinesTripRepository businesTripRepository, ITypesTravelExpensesRepository typesTravelExpensesRepository)
        {
            _view = view;
            _repository = repository;
            _businesTripRepository = businesTripRepository;
            _typeTravelEexpensesRepository = typesTravelExpensesRepository;

            travelExpensesBindingSource = new BindingSource();
            businesTripBindingSource = new BindingSource();
            typeTravelBindindSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadBTList();
            LoadCombobox();

            view.SetBusinesTripBindingSource(travelExpensesBindingSource);
            view.SetBusinesTripBindingSource(businesTripBindingSource);
            view.SetTypeTravelExpensesBindingSource(typeTravelBindindSource);

            _view.Show();
        }

        private void LoadBTList()
        {
            _travelExpenses = _repository.GetAll().Select(e => new TravelExpensesViewModel
            {
                Id = e.Id,
                BusinesTripId = e.BusinesTripId,
                TypesTravelExpensesId = e.TypesTravelExpensesId,
                DatePayments = e.DatePayments,
                NameExpense = e.NameExpense,
                PurposePayments = e.PurposePayments,
                SumPayments = e.SumPayments,
                TypeExpense = e.TypesTravelExpenses.Name,
                Destination = e.BusinesTrip.Destination,

            }).ToList();
            travelExpensesBindingSource.DataSource = _travelExpenses;
        }

        private void LoadCombobox()
        {
            _businesTrips = _businesTripRepository.GetAll().Select(e => new BusinesTripViewModel
            {
                Id = e.Id,
                Destination= e.Destination
            });
            businesTripBindingSource.DataSource = _businesTrips;

            _typeTravelExpenses = _typeTravelEexpensesRepository.GetAll().Select(e => new TypesTravelExpenses
            {
                Id = e.Id,
                Name = e.Name,
            });
            typeTravelBindindSource.DataSource = _typeTravelExpenses;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.Destination = string.Empty;
            _view.Goal = string.Empty;
            _view.Basis = string.Empty;
            _view.DateStart = DateTime.Now;
            _view.DateEnd = DateTime.Now;
            _view.Mark = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.Employee == null)
            {
                CleanViewFields();
                _view.Message = "Значения в комбобоксе нет";
                return;
            }

            var model = new BusinesTrip();
            model.Id = _view.Id;
            model.EmployeeId = _view.Employee.Id;
            model.Destination = _view.Destination;
            model.Goal = _view.Goal;
            model.Basis = _view.Basis;
            model.DateStart = _view.DateStart;
            model.DateEnd = _view.DateEnd;
            model.Mark = _view.Mark;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Служебная командировка была изменена";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Служебная командировка была добавлена";
                }
                _view.IsSuccessful = true;
                LoadBTList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.Message = ex.Message;
            }
        }

        private void DeleteSelected(object? sender, EventArgs e)
        {
            try
            {
                var model = (BusinesTripViewModel)travelExpensesBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }

                var entity = new BusinesTrip();
                entity.Id = model.Id;
                entity.EmployeeId = model.EmployeeId;
                entity.Destination = model.Destination;
                entity.Goal = model.Goal;
                entity.Basis = model.Basis;
                entity.DateStart = model.DateStart;
                entity.DateEnd = model.DateEnd;
                entity.Mark = model.Mark;

                _repository.Delete(entity);
                _view.IsSuccessful = true;
                _view.Message = "Удалено успешно";
                LoadBTList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Невозможно удалить командировку, либо она не выбрана";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (BusinesTripViewModel)travelExpensesBindingSource.Current;
            _view.Id = model.Id;
            _view.Employee.Id = model.EmployeeId;
            _view.Destination = model.Destination;
            _view.Goal = model.Goal;
            _view.Basis = model.Basis;
            _view.DateStart = model.DateStart;
            _view.DateEnd = model.DateEnd;
            _view.Mark = model.Mark;

            _view.IsEdit = true;
        }

        private void Add(object? sender, EventArgs e)
        {
            _view.IsEdit = false;
        }

        private void Search(object? sender, EventArgs e)
        {
            bool emptyValue = String.IsNullOrWhiteSpace(_view.searchValue);

            if (emptyValue == false)
                _travelExpenses = _repository.GetAllByValue(_view.searchValue).Select(e => new BusinesTripViewModel
                {
                    Id = e.Id,
                    EmployeeId = e.EmployeeId,
                    Destination = e.Destination,
                    Goal = e.Goal,
                    Mark = e.Mark,
                    Basis = e.Basis,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                    EmpSurname = e.Employee.Surname,
                    EmpName = e.Employee.Name,
                }).ToList();
            else
                _travelExpenses = _repository.GetAll().Select(e => new BusinesTripViewModel
                {
                    Id = e.Id,
                    EmployeeId = e.EmployeeId,
                    Destination = e.Destination,
                    Goal = e.Goal,
                    Mark = e.Mark,
                    Basis = e.Basis,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                    EmpSurname = e.Employee.Surname,
                    EmpName = e.Employee.Name,
                }).ToList();

            travelExpensesBindingSource.DataSource = _travelExpenses;
        }
    }
}
