using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoApp.Models;

namespace ToDoApp.DAL
{
    public class ToDoDAL
    {
        string CS = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public List<TodoModel> GetAllTodos()
        {
            List<TodoModel> list = new List<TodoModel>();
            using(SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ToDo", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);  
                DataTable data = new DataTable();
                conn.Open();
                adapter.Fill(data);
                conn.Close();

                foreach (DataRow row in data.Rows)
                {
                    list.Add(new TodoModel { 
                        Id = Convert.ToInt32(row["ID"]),
                        Title = Convert.ToString(row["Title"]),
                        Description = Convert.ToString(row["Description"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        Status = Convert.ToString(row["Status"])

                    });
                    
                }
            }

            return list;
        }

        public List<TodoModel> GetATodo(int Id)
        {
            List<TodoModel> list = new List<TodoModel>();
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SP_FETCHTODO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                conn.Open();
                adapter.Fill(data);
                conn.Close();

                foreach (DataRow row in data.Rows)
                {
                    list.Add(new TodoModel
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Title = Convert.ToString(row["Title"]),
                        Description = Convert.ToString(row["Description"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        Status = Convert.ToString(row["Status"])

                    });

                }
            }

            return list;
        }

        public bool Create(TodoModel todo)
        {
            int success = 0;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SP_CREATETODO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@Date", todo.Date);
                cmd.Parameters.AddWithValue("@Status", todo.Status);





                conn.Open();
                success = cmd.ExecuteNonQuery();
                conn.Close();
            }
             return success != 0;

        }

        public bool Update(int Id, TodoModel todo)
        {
            int success = 0;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SP_UPDATETODO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@Date", todo.Date);
                cmd.Parameters.AddWithValue("@Status", todo.Status);

                conn.Open();
                success = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return success != 0;

        }

        public bool Delete(TodoModel todo)
        {
            int success = 0;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM ToDo WHERE Id = @Id", conn);
                
                conn.Open();
                success = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return success != 0;

        }




    }

}