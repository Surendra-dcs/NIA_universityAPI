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
    }
}
