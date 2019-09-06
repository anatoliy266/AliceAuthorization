using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Services
{

    class AliceLunchService
    {
        public AliceLunchService()
        {

        }

        private SqlConnection Connect()
        {
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.InitialCatalog = "u0775820_aliceLunchService";
            builder.UserID = "u0775820_dev";
            builder.Password = "kzixAWn35ahvZBB";
            connection.ConnectionString = builder.ConnectionString;
            return connection;
        }

        private SqlConnection ConnectLocal()
        {
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(localdb)\\MSSQLLocalDB";
            builder.InitialCatalog = "AliceLunchService";
            connection.ConnectionString = builder.ConnectionString;
            return connection;
        }

        public DataSet Call(string procedure, string session, string action, string target = null, object[] item = null, string[] itemDesc = null)
        {
            DataSet data = new DataSet();
            //SqlConnection connection = ConnectLocal();
            SqlConnection connection = Connect();
            connection.Open();
            SqlCommand command = new SqlCommand(procedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sessionId", session);
            command.Parameters.AddWithValue("@action", action);
            if (target != null)
            {
                command.Parameters.AddWithValue("@target", target);
            }
            if (item != null)
            {
                DataTable items = new DataTable();
                items.Columns.Add("arrayValue", typeof(String));
                foreach (var word in item)
                {
                    items.Rows.Add(word);
                }
                command.Parameters.AddWithValue("@item", items);
            }
            if (itemDesc != null && item != null)
            {
                if (itemDesc.Count() == item.Count())
                {
                    DataTable itemsDesc = new DataTable();
                    foreach (var desc in itemDesc)
                    {
                        itemsDesc.Rows.Add(desc);
                    }
                    command.Parameters.AddWithValue("@itemDesc", itemsDesc);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            
            adapter.Fill(data);
            connection.Close();
            return data;
        }

        public void Execute(string text)
        {
            //SqlConnection connection = ConnectLocal();
            SqlConnection connection = Connect();
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = text;
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
