using DBCourse_Azuavchikova.Data.Entities;
using DBCourse_Azuavchikova.MVC.Models;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;

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
        public EmployeeViewModel Employee
        {
            get => (EmployeeViewModel)EmpCmb.SelectedItem;
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

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public BusinesTripView()
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
        }

        public void SetEmployeeBindingSource(BindingSource source)
        {
            EmpCmb.DataSource = source;
            EmpCmb.DisplayMember = "Surname";
            EmpCmb.ValueMember = "Id";
        }

        public void SetBusinesTripBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        private static BusinesTripView? instance;

        public static BusinesTripView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new BusinesTripView();
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
