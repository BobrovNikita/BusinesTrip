using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;

namespace DBCourse_Azuavchikova.MVC.Views
{
    public partial class TravelExpensesView : Form, ITravelExpensesView
    {
        private string? _message;
        private bool _isSuccessful;
        private bool _isEdit;

        public Guid Id
        {
            get => Guid.Parse(IdTxt.Text);
            set => IdTxt.Text = value.ToString();
        }
        public BusinesTripViewModel BusinesTrip
        {
            get => (BusinesTripViewModel)BusinesTripCmb.SelectedItem;
            set => BusinesTripCmb.SelectedItem = value;
        }
        public TypesTravelExpenses TypesTravelExpenses
        {
            get => (TypesTravelExpenses)TypeTravelExpensesCmb.SelectedItem;
            set => TypeTravelExpensesCmb.SelectedItem = value;
        }
        public string PurposePayments
        {
            get => PurposePaymentTxt.Text;
            set => PurposePaymentTxt.Text = value;
        }
        public DateTime DatePayments
        {
            get => DateDtp.Value;
            set => DateDtp.Value = value;
        }
        public int SumPayments
        {
            get
            {
                if (!int.TryParse(SumExpersesTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(SumExpersesTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    SumExpersesTxt.Text = value.ToString();
                }
                else
                    SumExpersesTxt.Text = string.Empty;
            }
        }
        public string NameExpense
        {
            get => NameExpersesTxt.Text;
            set => NameExpersesTxt.Text = value;
        }
        public string searchValue
        {
            get => SearchTxb.Text;
            set => SearchTxb.Text = value;

        }
        public bool IsEdit
        {
            get => _isEdit;
            set => _isEdit = value;
        }
        public bool IsSuccessful
        {
            get => _isSuccessful;
            set => _isSuccessful = value;
        }
        public string Message
        {
            get => _message;
            set => _message = value;
        }
        DateTime ITravelExpensesView.DateFirst
        {
            get => DateFirst.Value;
            set => DateFirst.Value = value;
        }
        DateTime ITravelExpensesView.DateSecond
        {
            get => DateSecond.Value;
            set => DateSecond.Value = value;
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler FilterEvent;

        public TravelExpensesView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            CloseBtn.Click += delegate { this.Close(); };
            IdTxt.Text = Guid.Empty.ToString();
        }

        private void AssosiateAndRaiseViewEvents()
        {
            //Search
            SearchBtn.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            SearchTxb.KeyDown += (s, e) =>
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add new
            AddBtn.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.TabPages.Remove(tabPage1);
                tabPage2.Text = "Добавление";
            };

            //Edit
            EditBtn.Click += delegate
            {
                if (dataGridView1.Rows.Count >= 1)
                {
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Add(tabPage2);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabPage2.Text = "Редактирование";
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись");
                }
            };

            //Delete
            DeleteBtn.Click += delegate
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить эту запись?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Save 
            SaveBtn.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(tabPage1);
                    tabControl1.TabPages.Remove(tabPage2);
                }

                MessageBox.Show(Message);
            };

            //Cancel
            CancelBtn.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
            };

            FilterBtn.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
        }

        public void SetBusinesTripBindingSource(BindingSource source)
        {
            BusinesTripCmb.DataSource = source;
            BusinesTripCmb.DisplayMember = "Destination";
            BusinesTripCmb.ValueMember = "Id";
        }

        public void SetTravelExpensesViewBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        public void SetTypeTravelExpensesBindingSource(BindingSource source)
        {
            TypeTravelExpensesCmb.DataSource = source;
            TypeTravelExpensesCmb.DisplayMember = "Name";
            TypeTravelExpensesCmb.ValueMember = "Id";
        }

        private static TravelExpensesView? instance;

        public static TravelExpensesView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new TravelExpensesView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;

                instance.BringToFront();
            }

            return instance;
        }
    }
}
