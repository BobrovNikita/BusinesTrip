using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;

namespace DBCourse_Azuavchikova.MVC.Controllers
{
    public class PositionController
    {
        private readonly IPositionView _view;
        private readonly IPositionRepository _repository;

        private BindingSource positionBindingSource;

        private IEnumerable<Position>? _positions;

        public PositionController(IPositionView view, IPositionRepository repository)
        {
            _view = view;
            _repository = repository;

            positionBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            PositionList();

            view.SetPositionBindingSource(positionBindingSource);

            _view.Show();
        }

        private void PositionList()
        {
            _positions = _repository.GetAll();
            positionBindingSource.DataSource = _positions;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.NamePosition = string.Empty;
            _view.DateEnter = DateTime.Now;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {            
            var model = new Position();
            model.Id = _view.Id;
            model.Name = _view.NamePosition;
            model.DateEnter = _view.DateEnter;
            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Должность обновлена успешно";
                }
                else
                {
                    _repository.Add(model);
                    _view.Message = "Должность добавлена успешно";
                }
                _view.IsSuccessful = true;
                PositionList();
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
                var model = (Position)positionBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Должность успешно удалена";
                PositionList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Невозможно удалить должность, либо она не выбрана";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (Position)positionBindingSource.Current;
            _view.Id = model.Id;
            _view.NamePosition = model.Name;
            _view.DateEnter = model.DateEnter;
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
                _positions = _repository.GetAllByValue(_view.searchValue);
            else
                _positions = _repository.GetAll();

            positionBindingSource.DataSource = _positions;
        }
    }
}
