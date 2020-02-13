using MySql.Data.MySqlClient;

namespace Tutorial.SqlConn
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            var host = "localhost";
            var port = 3306;
            var database = "Console";
            var username = "root";
            var password = "";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}