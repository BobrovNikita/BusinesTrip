using DBCourse_Azuavchikova.Data.Entities;
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
    public partial class BusinesTripView : Form, IBusinesTripView
    {
        private string? _message;
        private bool _isSuccessful;
        private bool _isEdit;
        public Guid Id
        {
            get => Guid.Parse(IdTxt.Text);
            set => IdTxt.Text = value.ToString();
        }
        public Employee Employee
        {
            get => (Employee)EmpCmb.SelectedItem;
            set => EmpCmb.SelectedItem = value;
        }
        public string Destination
        {
            get => DestinationTxt.Text;
            set => DestinationTxt.Text = value;
        }
        public string Goal
        {
            get => GoalTxt.Text;
            set => GoalTxt.Text = value;
        }
        public string Basis
        {
            get => BasisTxt.Text;
            set => BasisTxt.Text = value;
        }
        public int Mark
        {
            get
            {
                if (!int.TryParse(MarkTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(MarkTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    MarkTxt.Text = value.ToString();
                }
                else
                    MarkTxt.Text = string.Empty;
            }
        }
        public DateTime DateStart
        {
            get => DateStartDtp.Value;
            set => DateStartDtp.Value = value;
        }
        public DateTime DateEnd
        {
            get => DateEndDtp.Value;
            set => DateEndDtp.Value = value;
        }
        public string searchValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsEdit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public BusinesTripView()
        {
            InitializeComponent();
        }


        public void SetEmployeeBindingSource(BindingSource source)
        {
            throw new NotImplementedException();
        }

        public void SetPositionBindingSource(BindingSource source)
        {
            throw new NotImplementedException();
        }
    }
}
