using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Controllers
{
    public class TypesTravelExpensesController
    {
        private readonly ITypeTravelExpensesView _view;
        private readonly ITypesTravelExpensesRepository _repository;

        private BindingSource TTEBindingSource;

        private IEnumerable<TypesTravelExpenses>? _typesTravelExpenses;

        public TypesTravelExpensesController(ITypeTravelExpensesView view, ITypesTravelExpensesRepository repository)
        {
            _view = view;
            _repository = repository;

            TTEBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            TypeTravelExpensesList();

            view.SetTypeTravelExpensesBindingSource(TTEBindingSource);

            _view.Show();
        }

        private void TypeTravelExpensesList()
        {
            _typesTravelExpenses = _repository.GetAll();
            TTEBindingSource.DataSource = _typesTravelExpenses;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.NameTTE = string.Empty;
            _view.Rate = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new TypesTravelExpenses();
            model.Id = _view.Id;
            model.Name = _view.NameTTE;
            model.Rate = _view.Rate;
            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Тип расходов обновлен успешно";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Тип расходов добавлен успешно";
                }
                _view.IsSuccessful = true;
                TypeTravelExpensesList();
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
                var model = (TypesTravelExpenses)TTEBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Тип расходов успешно удалена";
                TypeTravelExpensesList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Невозможно удалить тип расходов, либо она не выбрана";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (TypesTravelExpenses)TTEBindingSource.Current;
            _view.Id = model.Id;
            _view.NameTTE = model.Name;
            _view.Rate = model.Rate;
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
                _typesTravelExpenses = _repository.GetAllByValue(_view.searchValue);
            else
                _typesTravelExpenses = _repository.GetAll();

            TTEBindingSource.DataSource = _typesTravelExpenses;
        }
    }
}
