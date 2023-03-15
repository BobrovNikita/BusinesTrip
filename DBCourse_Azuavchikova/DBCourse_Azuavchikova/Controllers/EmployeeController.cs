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
    public class EmployeeController
    {
        private readonly IEmployeeView _view;
        private readonly IEmployeeRepository _repository;
        private readonly IPositionRepository _positionRepository;

        private BindingSource employeeBindingSource;
        private BindingSource positionBindingSource;

        private IEnumerable<EmployeeViewModel>? _employees;
        private IEnumerable<Position>? _positions;

        public EmployeeController(IEmployeeView view, IEmployeeRepository repository, IPositionRepository positionRepository)
        {
            _view = view;
            _repository = repository;
            _positionRepository = positionRepository;

            employeeBindingSource = new BindingSource();
            positionBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadEmpList();
            LoadCombobox();

            view.SetEmployeeBindingSource(employeeBindingSource);
            view.SetPositionBindingSource(positionBindingSource);

            _view.Show();
        }

        private void LoadEmpList()
        {
            _employees = _repository.GetAll().Select(e => new EmployeeViewModel
            {
                Id= e.Id,
                PositionId = e.PositionId,
                Surname= e.Surname,
                Name= e.Name,
                LastName = e.LastName,
                Position = e.Position.Name
            }).ToList();
            employeeBindingSource.DataSource = _employees;
        }

        private void LoadCombobox()
        {
            _positions = _positionRepository.GetAll();
            positionBindingSource.DataSource = _positions;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.Position = new Position();
            _view.Surname =string.Empty;
            _view.NameEmp = string.Empty;
            _view.LastName = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.Position == null)
            {
                CleanViewFields();
                _view.Message = "Значения в комбобоксе нет";
                return;
            }

            var model = new Employee();
            model.Id = _view.Id;
            model.PositionId = _view.Position.Id;
            model.Surname = _view.Surname;
            model.Name = _view.NameEmp;
            model.LastName = _view.LastName;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Сотрудник был изменен";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Сотрудник был добавлен";
                }
                _view.IsSuccessful = true;
                LoadEmpList();
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
                var model = (EmployeeViewModel)employeeBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }

                var entity = new Employee();
                entity.Id = model.Id;
                entity.PositionId = model.PositionId;
                entity.Surname = model.Surname;
                entity.Name = model.Name;
                entity.LastName = model.LastName;

                _repository.Delete(entity);
                _view.IsSuccessful = true;
                _view.Message = "Удалено успешно";
                LoadEmpList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Невозможно удалить сотрудника, либо она не выбрана";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (EmployeeViewModel)employeeBindingSource.Current;
            _view.Id = model.Id;
            _view.Position.Id = model.PositionId;
            _view.Surname = model.Surname;
            _view.NameEmp = model.Name;
            _view.LastName = model.LastName;
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
                _employees = _repository.GetAllByValue(_view.searchValue).Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    PositionId = e.PositionId,
                    Surname = e.Surname,
                    Name = e.Name,
                    LastName = e.LastName,
                    Position = e.Position.Name
                }).ToList();
            else
                _employees = _repository.GetAll().Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    PositionId = e.PositionId,
                    Surname = e.Surname,
                    Name = e.Name,
                    LastName = e.LastName,
                    Position = e.Position.Name
                }).ToList();

            employeeBindingSource.DataSource = _employees;
        }
    }
}
