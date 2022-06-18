using Microsoft.Data.SqlClient;
using SE1615_Group2.DTL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1615_Group2.DAL
{
    public class ShowDAO : DAO
    {
        static ShowDAO Instance;
        ShowDAO() { }
        static ShowDAO() => Instance = new ShowDAO();
        public static ShowDAO GetInstance() => Instance;

        public DataTable GetDataTable() => GetDataTable("select * from shows");

        public Show GetById(int id)
        {
            DataTable dt = GetDataTable("select * from shows where showId = " + id);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            Show show = new Show
            {
                ShowId = (int)row["showId"],
                RoomId = (int)row["roomId"],
                FilmId = (int)row["filmId"],
                ShowDate = (DateTime)row["ShowDate"],
                Status = (bool)row["status"],
                Slot = (int)row["slot"],
                Price = (decimal)row["price"]
            };
            return show;

        }
        public DataTable GetByDateAndRoomID(int roomID, DateTime showDate)
        {
            return GetDataTable("select * from shows where RoomID=" + roomID + " and ShowDate='" + showDate.ToString("u") + "'");
        }
        public void Update(Show show)
        {
            SqlCommand cmd = new SqlCommand("Update shows set filmId = @filmId, slot = @slot, price = @price " +
                "where showId = @showId");
            cmd.Parameters.AddWithValue("@filmId", show.FilmId);
            cmd.Parameters.AddWithValue("@slot", show.Slot);
            cmd.Parameters.AddWithValue("@price", show.Price);
            cmd.Parameters.AddWithValue("@showId", show.ShowId);
            
            Update(cmd);

        }
        public void Create(Show show)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO shows (RoomID,FilmID,ShowDate,Price,Status,Slot) values (@roomId, @filmId, @date, @price, 1, @slot)");
            cmd.Parameters.AddWithValue("@roomId", show.RoomId);
            cmd.Parameters.AddWithValue("@filmId", show.FilmId);
            cmd.Parameters.AddWithValue("@slot", show.Slot);
            cmd.Parameters.AddWithValue("@price", show.Price); 
            cmd.Parameters.AddWithValue("@date", show.ShowDate);
            Update(cmd); 

        }
        public void Delete(int showId)
        {
            SqlCommand cmd = new SqlCommand("Delete from  shows where showId = @showId");
            cmd.Parameters.AddWithValue("@showId", showId);
            
            Update(cmd); 

        }

        public DataTable Search(int filmID, DateTime dateTime, int roomID )
        {
            DataTable dt;
            if (filmID == 0)
                 dt= GetDataTable("select * from shows where  ShowDate = '" + dateTime.ToString("u") + "' and RoomID = '" + roomID + "'");
            else
             dt = GetDataTable("select * from shows where FilmID = '" + filmID + "' and ShowDate = '" + dateTime.ToString("u") + "' and RoomID = '" + roomID + "'");

            if (dt.Rows.Count == 0) return null;
            return dt;
        }

    }
}
