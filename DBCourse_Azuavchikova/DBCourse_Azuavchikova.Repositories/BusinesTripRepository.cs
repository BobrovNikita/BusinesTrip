using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.Repositories
{
    internal class BusinesTripRepository : BaseRepository, IBusinesTripRepository
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
            var employee = _db.BusinesTrips.FirstOrDefault(e => e.Id == id);

            return employee;
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
