using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using SystemTask.Models;

namespace SystemTask.BDHelper
{
    public class AccessDB
    {
        private static String ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\TEST MVC\TaskProg\SystemTask\SystemTask\App_Data\DB.mdf';Integrated Security=True";
        private static SqlConnection ThisConnection;
        //private static SqlCommand Comman;

        /*public static SqlConnection GetConnection()
        {
            Connection = new SqlConnection(ConnectionString);
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            return Connection;
        }

            public static void CloseConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }
        */

        private void GetNewConnection()
        {
            ThisConnection = new SqlConnection(ConnectionString);
            if (ThisConnection.State != System.Data.ConnectionState.Open)
                ThisConnection.Open();           
        }
        /*
        private void GetNewConnection()
        {
            ThisConnection = new SqlConnection(ConnectionString);
        }  */             

        //Add a new Status
        public bool AddNewStatus(Status newSta)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("AddStatus", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdStatus", newSta.idStatus);
                command.Parameters.AddWithValue("@nameStatus", newSta.nameStatus);

                i = command.ExecuteNonQuery();                           
            }
            ThisConnection.Close();
            return i >= 1;
        }

        //get list of Status
        public List<Status> GetAllStatus()
        {
            GetNewConnection();
            List<Status> list = new List<Status>();
            using (SqlCommand command = new SqlCommand("GetAllStatus", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader= command.ExecuteReader();
                while (reader.Read())
                {
                    Status st = new Status();
                    st.idStatus = Convert.ToInt32(reader["IdStatus"]);
                    st.nameStatus = Convert.ToString(reader["nameStatus"]);                    
                    list.Add(st);
                }
                ThisConnection.Close();
                return list;
            }

        }

        //Update Status
        public bool UpdateStatus(Status stat)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("UpdateStatus", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdStatus", stat.idStatus);
                command.Parameters.AddWithValue("@nameStatus", stat.nameStatus);
                
                i = command.ExecuteNonQuery();
            }
            ThisConnection.Close();
            return i >= 1;
        }

        //Delete Status
        public bool DeleteStatus(int idStatus)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("EliminarStatusById", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdStatus", idStatus);
                
                i = command.ExecuteNonQuery();
            }
            ThisConnection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}