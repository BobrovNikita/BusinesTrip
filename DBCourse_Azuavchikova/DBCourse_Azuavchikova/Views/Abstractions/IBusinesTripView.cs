﻿using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCourse_Azuavchikova.MVC.Views.Abstractions
{
    public interface IBusinesTripView
    {
        Guid Id { get; set; }
        EmployeeViewModel Employee { get; set; }
        string Destination { get; set; }
        string Goal { get; set; }
        string Basis { get; set; }
        int Mark { get; set; }
        DateTime DateStart { get; set; }
        DateTime DateEnd { get; set; }


        string searchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler OrderPrintEvent;
        event EventHandler CerteficatePrintEvent;

        void SetEmployeeBindingSource(BindingSource source);
        void SetBusinesTripBindingSource(BindingSource source);
        void Show();
    }
}
