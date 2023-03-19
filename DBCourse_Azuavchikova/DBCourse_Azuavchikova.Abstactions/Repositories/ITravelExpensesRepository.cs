﻿using DBCourse_Azuavchikova.Data.Entities;

namespace DBCourse_Azuavchikova.Abstactions.Repositories
{
    public interface ITravelExpensesRepository
    {
        IEnumerable<TravelExpenses> GetAll();
        IEnumerable<TravelExpenses> GetAllByValue(string value);
        IEnumerable<TravelExpenses> GetAllBetweenDate(DateTime startDate, DateTime endDate);

        TravelExpenses GetModel(Guid id);
        void Add(TravelExpenses model);
        void Update(TravelExpenses model);
        void Delete(TravelExpenses model);
    }
}
