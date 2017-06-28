using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using SystemTask.Models;


namespace SystemTask.BDHelper
{
    public class BDHelperTask
    {
        private static String ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\TEST MVC\TaskProg\SystemTask\SystemTask\App_Data\DB.mdf';Integrated Security=True";
        private static SqlConnection ThisConnection;
        
        private void GetNewConnection()
        {
            ThisConnection = new SqlConnection(ConnectionString);
            if (ThisConnection.State != System.Data.ConnectionState.Open)
                ThisConnection.Open();
        }
       
        //Add a new task
        public bool AddNewTask(Task newTask)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("AddTask", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTask", newTask.IdTask);
                command.Parameters.AddWithValue("@nameTask", newTask.nameTask);
                command.Parameters.AddWithValue("@description", newTask.description);
                command.Parameters.AddWithValue("@IdStatus", newTask.IdStatus);

                i = command.ExecuteNonQuery();
            }
            ThisConnection.Close();
            return i >= 1;
        }

        //get list of Task
        public List<Task> GetAllTask()
        {
            GetNewConnection();
            List<Task> list = new List<Task>();
            using (SqlCommand command = new SqlCommand("GetAllTask", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Task tas = new Task();
                    tas.IdTask = Convert.ToInt32(reader["IdTask"]);
                    tas.nameTask = Convert.ToString(reader["nameTask"]);
                    tas.description = Convert.ToString(reader["description"]);
                    tas.IdStatus = Convert.ToInt32(reader["IdStatus"]);
                    list.Add(tas);
                }
                ThisConnection.Close();
                return list;
            }

        }

        //get list of Task
        public List<Task> GetAllTaskGrid()
        {
            GetNewConnection();
            List<Task> list = new List<Task>();
            using (SqlCommand command = new SqlCommand("GetAllTask", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Task tas = new Task(1);
                    tas.IdTask = Convert.ToInt32(reader["IdTask"]);
                    tas.nameTask = Convert.ToString(reader["nameTask"]);
                    tas.description = Convert.ToString(reader["description"]);
                    tas.IdStatus = Convert.ToInt32(reader["IdStatus"]);
                    tas.status = Convert.ToString(reader["nameStatus"]);
                    
                    list.Add(tas);
                }
                ThisConnection.Close();
                return list;
            }

        }

        //get list of Task
        public int GetCountTask()
        {
            GetNewConnection();
            int total = 0;
            using (SqlCommand command = new SqlCommand("GetCountTask", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {                    
                    total = Convert.ToInt32(reader["Total"]);
                    
                }
                ThisConnection.Close();
                return total;
            }

        }

        //Update task
        public bool UpdateTask(Task t)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("UpdateTask", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTask", t.IdTask);
                command.Parameters.AddWithValue("@nameTask", t.nameTask);
                command.Parameters.AddWithValue("@description", t.description);
                command.Parameters.AddWithValue("@IdStatus", t.IdStatus);

                i = command.ExecuteNonQuery();
            }
            ThisConnection.Close();
            return i >= 1;
        }

        //Delete Task
        public bool DeleteTask(int idTask)
        {
            GetNewConnection();
            int i;
            using (SqlCommand command = new SqlCommand("DeleteTaskById", ThisConnection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTask", idTask);

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