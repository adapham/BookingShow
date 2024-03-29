﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1615_Group2.DAL
{
    public class DAO
    {
        string strConn;
        public DAO()
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            // strConn = conf["ConnectionStrings:DbConnection"];
            strConn = conf.GetConnectionString("DbConnection");
        }

        public DataTable GetDataTable(string sql)
        {

            //create object connection
            SqlConnection conn = new SqlConnection(strConn);

            //create object command
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Connection = conn;

            //create DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            return dt;
            
        }
        public DataTable GetDataTable1(string sql)
        {

            //create object connection
            SqlConnection conn = new SqlConnection(strConn);

            //create object command
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Connection = conn;

            //create DataAdapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            dt.Rows.Add(0, 0, "on");

            return dt;

        }
        
        //Insert, Update, delete depend cmd
        public void Update(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(strConn);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
