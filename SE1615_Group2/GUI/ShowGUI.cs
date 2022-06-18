using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SE1615_Group2.DAL;
using SE1615_Group2.DTL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1615_Group2.GUI
{
    public partial class ShowGUI : Form
    {
        public ShowGUI()
        {
            InitializeComponent();
            if (Settings.UserName.Equals("admin"))
            {

            }
                binGrid();
            cboFilmGUI.DataSource = FilmDAO.GetInstance().GetDataTable1();
            
            
            
            cboFilmGUI.DisplayMember = "Title";
            cboFilmGUI.ValueMember = "FilmID";

            cboRoom.DataSource = RoomDAO.GetInstance().GetDataTable();
            cboRoom.DisplayMember = "Name";
            cboRoom.ValueMember = "RoomID";

        }
        void binGrid()
        {
            DataTable dt = ShowDAO.GetInstance().GetDataTable();
            dataGridView1.DataSource = dt;
            int count = dt.Columns.Count;

            //booking
            DataGridViewButtonColumn btnBooking = new DataGridViewButtonColumn
            {
                Name = "Booking",
                Text = "Booking",
                UseColumnTextForButtonValue = true,
            };
            if (Settings.UserName.Equals("admin"))
            {
                dataGridView1.Columns.Insert(count, btnBooking);
            }

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            if (Settings.UserName.Equals("admin"))
            {
                    dataGridView1.Columns.Insert(count+1, btnEdit);
            }
            

            //delete
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
            };
            if (Settings.UserName.Equals("admin"))
            {
                dataGridView1.Columns.Insert(count+2, btnDelete);
            }

            
            dataGridView1.Columns["showid"].Visible = false;
            dataGridView1.Columns["status"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Show show = new Show
            {
                RoomId = (int)cboRoom.SelectedValue,
                //FilmId = (int)cboFilmGUI.SelectedValue,
                ShowDate = dateTimePicker1.Value,
                ShowId = 0
                
            };
            ShowAddEditGUI f = new ShowAddEditGUI(show);
            DialogResult dr = f.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
            {
                int showId = (int)dataGridView1.Rows[e.RowIndex].Cells["showId"].Value;
                Show show = ShowDAO.GetInstance().GetById(showId);

                ShowAddEditGUI f = new ShowAddEditGUI(show);
                DialogResult dr = f.ShowDialog();
                MessageBox.Show("That show is edited");
               

            }
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                int showId = (int)dataGridView1.Rows[e.RowIndex].Cells["showId"].Value;
                Show show = ShowDAO.GetInstance().GetById(showId);
                DialogResult dr = MessageBox.Show("Do you sure to delete show " + showId, "Warning", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ShowDAO.GetInstance().Delete(showId);
                    

                }

            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


           
        }

        private void cbxFilmGUI_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int RoomId = (int)cboRoom.SelectedValue;
            int FilmId = (int)cboFilmGUI.SelectedValue;
            DateTime ShowDate = dateTimePicker1.Value;
            MessageBox.Show(FilmId.ToString());
            DataTable dt = ShowDAO.GetInstance().Search(FilmId, ShowDate, RoomId);
            dataGridView1.DataSource = dt;

        }
        public void RefreshShow()
        {
            DataTable dt = ShowDAO.GetInstance().GetDataTable();
            dataGridView1.DataSource = dt;
        }
    }
}
