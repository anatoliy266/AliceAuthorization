using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class AliceClinicService
    {
        public AliceClinicService()
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

        public DataSet Call(string procedure, string session, string action, string target = null, string[] item = null)
        {
            DataSet data = new DataSet();
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
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);
            connection.Close();
            return data;
        }
    }
}
