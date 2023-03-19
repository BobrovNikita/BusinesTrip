using Azure.Core;
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
            view.FilterEvent += Filtration;

            LoadBTList();
            LoadCombobox();

            view.SetTravelExpensesViewBindingSource(travelExpensesBindingSource);
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

            if (_businesTrips.ToList().Count > 0)
            {
                typeTravelBindindSource.DataSource = _typeTravelExpenses;
            }

            businesTripBindingSource.DataSource = _businesTrips;

            _typeTravelExpenses = _typeTravelEexpensesRepository.GetAll().Select(e => new TypesTravelExpenses
            {
                Id = e.Id,
                Name = e.Name,
            });

            if (_typeTravelExpenses.ToList().Count > 0)
            {
                typeTravelBindindSource.DataSource = _typeTravelExpenses;
            }
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.BusinesTrip = new BusinesTripViewModel();
            _view.TypesTravelExpenses = new TypesTravelExpenses();
            _view.PurposePayments = string.Empty;
            _view.DatePayments = DateTime.Now;
            _view.SumPayments = -1;
            _view.NameExpense = string.Empty;

        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.BusinesTrip == null || _view.TypesTravelExpenses == null)
            {
                CleanViewFields();
                _view.Message = "Значения в комбобоксе нет";
                return;
            }

            var model = new TravelExpenses();
            model.Id = _view.Id;
            model.BusinesTripId = _view.BusinesTrip.Id;
            model.TypesTravelExpensesId = _view.TypesTravelExpenses.Id;
            model.PurposePayments = _view.PurposePayments;
            model.DatePayments = _view.DatePayments;
            model.NameExpense = _view.NameExpense;
            model.SumPayments = _view.SumPayments;


            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Командировоные расходы были изменене";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Командировоные расходы были добавлены";
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
                var model = (TravelExpensesViewModel)travelExpensesBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }

                var entity = new TravelExpenses();
                entity.Id = model.Id;
                entity.BusinesTripId = model.BusinesTripId;
                entity.TypesTravelExpensesId = model.TypesTravelExpensesId;
                entity.PurposePayments = _view.PurposePayments;
                entity.DatePayments = _view.DatePayments;
                entity.SumPayments = _view.SumPayments;
                entity.NameExpense = _view.NameExpense;

                _repository.Delete(entity);
                _view.IsSuccessful = true;
                _view.Message = "Удалено успешно";
                LoadBTList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Невозможно удалить расходы, либо они не выбраны";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (TravelExpensesViewModel)travelExpensesBindingSource.Current;
            _view.Id = model.Id;
            _view.BusinesTrip.Id = model.BusinesTripId;
            _view.TypesTravelExpenses.Id = model.TypesTravelExpensesId;
            _view.PurposePayments = model.PurposePayments;
            _view.DatePayments = model.DatePayments;
            _view.NameExpense = model.NameExpense;
            _view.SumPayments = model.SumPayments;

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
                _travelExpenses = _repository.GetAllByValue(_view.searchValue).Select(e => new TravelExpensesViewModel
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
            else
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

        private void Filtration(object? sender, EventArgs e)
        {
            _travelExpenses = _repository.GetAllBetweenDate(_view.DateFirst, _view.DateSecond).Select(e => new TravelExpensesViewModel
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
            }).ToList(); ;

            travelExpensesBindingSource.DataSource = _travelExpenses;
        }
    }
}
