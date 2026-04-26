using Microsoft.Data.SqlClient;
using System.Data;

namespace NIAUNIVERSITYPANELAPI.Models
{
    public class DbHelper
    {
        private readonly string _connection;

        public DbHelper(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }

        public DataTable ExecuteSP(string spName, SqlParameter[] parameters)
        {
            using SqlConnection con = new SqlConnection(_connection);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public async Task<DataTable> ExecuteDataAsync(string procName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connection))
            using (SqlCommand cmd = new SqlCommand(procName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    await con.OpenAsync();
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
