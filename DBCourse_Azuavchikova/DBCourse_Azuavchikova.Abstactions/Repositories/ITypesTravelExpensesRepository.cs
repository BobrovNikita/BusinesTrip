using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Abstactions.Repositories
{
    public interface ITypesTravelExpensesRepository
    {
        IEnumerable<TypesTravelExpenses> GetAll();
        IEnumerable<TypesTravelExpenses> GetAllByValue(string value);
        TypesTravelExpenses GetModel(Guid id);
        void Add(TypesTravelExpenses model);
        void Update(TypesTravelExpenses model);
        void Delete(TypesTravelExpenses model);
    }
}
