using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Abstactions.Repositories
{
    public interface IBusinesTripRepository
    {
        IEnumerable<BusinesTrip> GetAll();
        IEnumerable<BusinesTrip> GetAllByValue(string value);
        BusinesTrip GetModel(Guid id);
        void Add(BusinesTrip model);
        void Update(BusinesTrip model);
        void Delete(BusinesTrip model);
    }
}
