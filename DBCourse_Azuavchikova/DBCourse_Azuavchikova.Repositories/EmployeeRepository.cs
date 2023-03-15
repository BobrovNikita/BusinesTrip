using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBCourse_Azuavchikova.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(BusinesTripsPayments context) : base(context)
        {
        }

        public void Add(Employee model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(Employee model)
        {
            using (var context = new BusinesTripsPayments())
            {
                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using(var context = new BusinesTripsPayments())
                return context.Employees.Include(p => p.Position).ToList();
        }

        public IEnumerable<Employee> GetAllByValue(string value)
        {
            using(var context = new BusinesTripsPayments())
                return context.Employees.Include(p => p.Position).Where(e => 
                                                                e.Surname.Contains(value) || 
                                                                e.Name.Contains(value) || 
                                                                e.LastName.Contains(value) ||
                                                                e.Position.Name.Contains(value)
                                                                ).ToList();
        }

        public Employee GetModel(Guid id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);

            return employee;
        }

        public void Update(Employee model)
        {
            using(var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Update(model);
                context.SaveChanges();
            }
        }
    }
}
