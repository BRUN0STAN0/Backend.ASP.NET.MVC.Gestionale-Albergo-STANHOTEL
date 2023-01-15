using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace STANHOTEL.Models
{
    public class Shared
    {
        public static SqlConnection GetConnectionToDB()
        {
            string con = ConfigurationManager.ConnectionStrings["connessioneDB"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(con);
            return sqlConn;
        }
        public static SqlDataReader getDataReaderWithoutValue(SqlConnection con, string sql)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sql;
            sqlCommand.Connection = con;
            SqlDataReader r = sqlCommand.ExecuteReader();
            return r;
        }
    }
}