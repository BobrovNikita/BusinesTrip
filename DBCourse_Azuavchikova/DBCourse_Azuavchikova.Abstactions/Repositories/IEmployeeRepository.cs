using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Abstactions.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAllByValue(string value);
        Employee GetModel(Guid id);
        void Add(Employee model);
        void Update(Employee model);
        void Delete(Employee model);
    }
}
