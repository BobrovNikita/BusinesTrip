using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBCourse_Azuavchikova.Repositories
{
    public class TravelExpensesRepository : BaseRepository, ITravelExpensesRepository
    {
        public TravelExpensesRepository(BusinesTripsPayments context) : base(context)
        {
        }

        public void Add(TravelExpenses model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(TravelExpenses model)
        {
            using (var context = new BusinesTripsPayments())
            {
                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<TravelExpenses> GetAll()
        {
            using (var context = new BusinesTripsPayments())
                return context.TravelExpenses.Include(p => p.BusinesTrip).Include(t => t.TypesTravelExpenses).ToList();
        }

        public IEnumerable<TravelExpenses> GetAllBetweenDate(DateTime startDate, DateTime endDate)
        {
            using (var context = new BusinesTripsPayments())
                return context.TravelExpenses.Include(p => p.BusinesTrip).Include(t => t.TypesTravelExpenses).Where(e =>
                                                                e.DatePayments >= startDate && e.DatePayments <= endDate
                                                                ).ToList();
        }

        public IEnumerable<TravelExpenses> GetAllByValue(string value)
        {
            using (var context = new BusinesTripsPayments())
                return context.TravelExpenses.Include(p => p.BusinesTrip).Include(t => t.TypesTravelExpenses).Where(e =>
                                                                e.PurposePayments.Contains(value) ||
                                                                e.NameExpense.Contains(value) ||
                                                                e.TypesTravelExpenses.Name.Contains(value) ||
                                                                e.BusinesTrip.Destination.Contains(value)
                                                                ).ToList();
        }

        public TravelExpenses GetModel(Guid id)
        {
            var travelExpenses = _db.TravelExpenses.FirstOrDefault(e => e.Id == id);

            return travelExpenses;
        }

        public void Update(TravelExpenses model)
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
