using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.Repositories
{
    public class TypeTravelExpensesRepository : BaseRepository, ITypesTravelExpensesRepository
    {
        public TypeTravelExpensesRepository(BusinesTripsPayments context) : base(context)
        {
        }

        public void Add(TypesTravelExpenses model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(TypesTravelExpenses model)
        {
            using (var context = new BusinesTripsPayments())
            {
                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<TypesTravelExpenses> GetAll()
        {
            using (var context = new BusinesTripsPayments())
                return context.TypesTravelExpenses.ToList();
        }

        public IEnumerable<TypesTravelExpenses> GetAllByValue(string value)
        {
            using (var context = new BusinesTripsPayments())
                return context.TypesTravelExpenses.Where(p => p.Name.Contains(value) || 
                                                              p.Rate.ToString().Contains(value)).ToList();
        }

        public TypesTravelExpenses GetModel(Guid id)
        {
            var typeTravelExpenses = _db.TypesTravelExpenses.FirstOrDefault(p => p.Id == id);

            return typeTravelExpenses;
        }

        public void Update(TypesTravelExpenses model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.TypesTravelExpenses.Update(model);
                context.SaveChanges();
            }
        }
    }
}
