using Microsoft.Data.SqlClient;

namespace firstpr 
{
    public static class DatabaseConnection
    {
        private static readonly string ConnectionString =
            "Server=.;Database=School_DB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}