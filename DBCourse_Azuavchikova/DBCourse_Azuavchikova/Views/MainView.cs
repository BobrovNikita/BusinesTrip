using DBCourse_Azuavchikova.MVC.Views.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCourse_Azuavchikova.MVC.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            InitializeBtnEvents();
        }

        private void InitializeBtnEvents()
        {
            PositionBtn.Click += delegate { LoadPosition?.Invoke(this, EventArgs.Empty); };
            EmployeeBtn.Click += delegate { LoadEmployee?.Invoke(this, EventArgs.Empty); };
            BusinesTripBtn.Click += delegate { LoadBusinesTrip?.Invoke(this, EventArgs.Empty); };
            TravelExpensesBtn.Click += delegate { LoadTravelExpenses?.Invoke(this, EventArgs.Empty); };
            TypesTravelExpensesBtn.Click += delegate { LoadTypesTravelExpenses?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler LoadPosition;
        public event EventHandler LoadEmployee;
        public event EventHandler LoadTypesTravelExpenses;
        public event EventHandler LoadTravelExpenses;
        public event EventHandler LoadBusinesTrip;
    }
}
