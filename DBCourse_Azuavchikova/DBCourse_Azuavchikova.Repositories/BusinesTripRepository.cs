using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBCourse_Azuavchikova.Repositories
{
    public class BusinesTripRepository : BaseRepository, IBusinesTripRepository
    {
        public BusinesTripRepository(BusinesTripsPayments context) : base(context)
        {
        }

        public void Add(BusinesTrip model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(BusinesTrip model)
        {
            using (var context = new BusinesTripsPayments())
            {
                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<BusinesTrip> GetAll()
        {
            using (var context = new BusinesTripsPayments())
                return context.BusinesTrips.Include(p => p.Employee).ToList();
        }

        public IEnumerable<BusinesTrip> GetAllByValue(string value)
        {
            using (var context = new BusinesTripsPayments())
                return context.BusinesTrips.Include(p => p.Employee).Where(e =>
                                                                e.Employee.Surname.Contains(value) ||
                                                                e.Employee.Name.Contains(value) ||
                                                                e.Destination.Contains(value) ||
                                                                e.Goal.Contains(value)
                                                                ).ToList();
        }

        public BusinesTrip GetModel(Guid id)
        {
            var businesTrip = _db.BusinesTrips.FirstOrDefault(e => e.Id == id);

            return businesTrip;
        }

        public void Update(BusinesTrip model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Update(model);
                context.SaveChanges();
            }
        }
    }
}
