using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Controllers
{
    public class BusinesTripController
    {
        private readonly IBusinesTripView _view;
        private readonly IBusinesTripRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;

        private BindingSource buninesTripBindingSource;
        private BindingSource employeeBindingSource;

        private IEnumerable<BusinesTripViewModel>? _businesTrips;
        private IEnumerable<EmployeeViewModel>? _employees;

        public BusinesTripController(IBusinesTripView view, IBusinesTripRepository repository, IEmployeeRepository employeeRepository)
        {
            _view = view;
            _repository = repository;
            _employeeRepository = employeeRepository;

            buninesTripBindingSource = new BindingSource();
            employeeBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadBTList();
            LoadCombobox();

            view.SetBusinesTripBindingSource(buninesTripBindingSource);
            view.SetEmployeeBindingSource(employeeBindingSource);

            _view.Show();
        }

        private void LoadBTList()
        {
            _businesTrips = _repository.GetAll().Select(e => new BusinesTripViewModel
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
            buninesTripBindingSource.DataSource = _businesTrips;
        }

        private void LoadCombobox()
        {
            _employees = _employeeRepository.GetAll().Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Surname = e.Surname,
                Name= e.Name,
                LastName = e.LastName,
                Position = e.Position.Name,
                PositionId = e.PositionId,
            });
            employeeBindingSource.DataSource = _employees;
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
                var model = (BusinesTripViewModel)buninesTripBindingSource.Current;
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
            var model = (BusinesTripViewModel)buninesTripBindingSource.Current;
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
                _businesTrips = _repository.GetAllByValue(_view.searchValue).Select(e => new BusinesTripViewModel
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
                _businesTrips = _repository.GetAll().Select(e => new BusinesTripViewModel
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

            buninesTripBindingSource.DataSource = _businesTrips;
        }
    }
}
