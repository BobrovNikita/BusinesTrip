using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Abstactions.Repositories
{
    public interface IPositionRepository : IDisposable
    {
        IEnumerable<Position> GetAll();
        IEnumerable<Position> GetAllByValue(string value);
        Position GetModel(Guid id);

        void Add (Position model);
        void Update (Position model);
        void Delete (Position model);
    }
}
