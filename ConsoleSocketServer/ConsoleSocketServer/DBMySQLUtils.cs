using System;
using MySql.Data.MySqlClient;

namespace Tutorial.SqlConn
{
    public class DBMySQLUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            var connectionString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;
            var connection = new MySqlConnection(connectionString);

            return connection;
        }
    }
}