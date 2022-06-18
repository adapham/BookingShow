using SE1615_Group2.DAL;
using SE1615_Group2.DTL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SE1615_Group2.GUI
{
    public partial class ShowAddEditGUI : Form
    {
        int add;
        public ShowAddEditGUI(Show show)
        {
            InitializeComponent();
            if (show.ShowId == 0) add = 1;
            else add = 0;

            //ComboBox cmb = new ComboBox();
            //string hoten = cmb.Text;//Tương ứng với DisplayMember
            //int ma = int.Parse(cmb.SelectedValue.ToString());//Tương ứng với ValueMember

            if (add == 0)//update
            {
                dtpShowDate.Value = show.ShowDate;
                cboRoomId.DataSource = RoomDAO.GetInstance().GetDataTable();
                DataTable dt = ShowDAO.GetInstance().GetByDateAndRoomID(show.RoomId, show.ShowDate);
                cboFilmId.DataSource = FilmDAO.GetInstance().GetDataTable();

                List<int> availSlot = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (DataRow row in dt.Rows)
                {
                    availSlot.Remove((int)row["Slot"]);
                }
                //availSlot.Add(show.Slot);
                cboSlot.DataSource = availSlot;
                cboSlot.SelectedItem = show.Slot;

                cboFilmId.DisplayMember = "title";// lay ten "title" trong database
                cboFilmId.ValueMember = "filmid";// lay ten "filmid" trong database
                cboFilmId.SelectedValue = show.FilmId;



                cboRoomId.DisplayMember = "Name";
                cboRoomId.ValueMember = "RoomId";
                cboRoomId.SelectedValue = show.RoomId;
                txtPrice.Text = show.Price.ToString();
                txtShowID.Text= show.ShowId.ToString();

            }
            if (add == 1) //create
            {
                dtpShowDate.Value = show.ShowDate;
                cboRoomId.DataSource = RoomDAO.GetInstance().GetDataTable();
                DataTable dt = ShowDAO.GetInstance().GetByDateAndRoomID(show.RoomId, show.ShowDate);
                cboFilmId.DataSource = FilmDAO.GetInstance().GetDataTable();


                List<int> availSlot = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (DataRow row in dt.Rows)
                {
                    availSlot.Remove((int)row["Slot"]);

                }
                availSlot.Add(show.Slot);
                cboSlot.DataSource = availSlot;
                cboSlot.SelectedItem = show.Slot;


                cboRoomId.DisplayMember = "Name";
                cboRoomId.ValueMember = "RoomId";
                cboRoomId.SelectedValue = show.RoomId;

                cboFilmId.DisplayMember = "title";// lay ten "title" trong database
                cboFilmId.ValueMember = "filmid";// lay ten "filmid" trong database
                cboFilmId.SelectedValue = show.FilmId;


                txtShowID.Text = "0";
            }
        }

        private void dtpShowDate_ValueChanged(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (txtShowID.Text != "0")
            {

                Show show = new Show
                {
                    FilmId = (int)cboFilmId.SelectedValue,
                    Slot = (int)cboSlot.SelectedValue,
                    Price = Convert.ToDecimal(txtPrice.Text),


                    ShowId = Convert.ToInt32(txtShowID.Text)
                };
                ShowDAO.GetInstance().Update(show);
                ShowGUI showGUI = new ShowGUI();
                showGUI.RefreshShow();

            }
            else
            {

                Show show = new Show
                {
                    RoomId = (int)cboRoomId.SelectedValue,
                    FilmId = (int)cboFilmId.SelectedValue,
                    ShowDate = dtpShowDate.Value,
                    Slot = (int)cboSlot.SelectedValue,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    ShowId = Convert.ToInt32(txtShowID.Text)
                };

                ShowDAO.GetInstance().Create(show);
                MessageBox.Show("A new  show is added");
                ShowGUI showGUI = new ShowGUI();
                showGUI.RefreshShow();
            }


        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            //Regex myRegularExpression = new
            //                Regex("[^0-9]");
            //if (!myRegularExpression.IsMatch(txtPrice.Text))
            //{
            //    MessageBox.Show("Tuyen, Khai");
            //}
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPrice.Text, "[^0-9]"))
            {
                string temp = txtPrice.Text;
                MessageBox.Show("Please enter only numbers.");

                txtPrice.Text = txtPrice.Text.Remove(txtPrice.Text.Length - 1);
                txtPrice.Text = temp;

            }
        }
    }
}
