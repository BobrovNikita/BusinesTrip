using DBCourse_Azuavchikova.Data;

namespace DBCourse_Azuavchikova.Repositories
{
    public abstract class BaseRepository
    {
        protected BusinesTripsPayments _db;

        public BaseRepository(BusinesTripsPayments context)
        {
            _db = context;
        }
    }
}
