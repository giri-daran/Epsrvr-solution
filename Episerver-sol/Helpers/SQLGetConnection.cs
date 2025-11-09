using Microsoft.Data.SqlClient;

namespace EpiserverBH.Helpers
{
    public class SQLGetConnection
    {
        public static string conStr;

        public static SqlConnection GetConnection()
        {

            try
            {

                SqlConnection connection = new SqlConnection(conStr);
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
