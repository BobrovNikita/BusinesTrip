using DBCourse_Azuavchikova.Abstactions.Repositories;
using DBCourse_Azuavchikova.Data;
using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Repositories
{
    public class PositionRepository : BaseRepository, IPositionRepository
    {
        public PositionRepository(BusinesTripsPayments context) : base(context)
        {
        }

        public void Add(Position model)
        {
            using (var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(Position model)
        {
            using (var context = new BusinesTripsPayments())
            {
                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<Position> GetAll()
        {
            using(var context = new BusinesTripsPayments())
                return context.Positions.ToList();
        }   

        public IEnumerable<Position> GetAllByValue(string value)
        {
            using(var context = new BusinesTripsPayments())
                return context.Positions.Where(p => p.Name.Contains(value)).ToList();
        }

        public Position GetModel(Guid id)
        {
            var position = _db.Positions.FirstOrDefault(p => p.Id == id);

            return position;
        }

        public void Update(Position model)
        {
            using(var context = new BusinesTripsPayments())
            {
                new Data.Validation.ModelDataValidation().Validate(model);

                context.Positions.Update(model);
                context.SaveChanges();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
