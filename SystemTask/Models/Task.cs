using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace SystemTask.Models
{
    public class Task
    {
        
        public int IdTask { get; set; }
        public string nameTask { get; set; }
        public string description { get; set; }
        public int IdStatus { get; set; }
        public string status { get; set; }

        public Task() {
            status = "";
        }

        public Task(int forGrid)
        {
            
        }
    }
}