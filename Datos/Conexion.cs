using System.Data.SqlClient;

namespace TEST_CRUD.Datos
{
    public class Conexion
    {
        private string stringsql = string.Empty;
        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            stringsql = builder.GetSection("ConnectionStrings:conexion").Value;
        }

        public string getconexion()
        {
            return stringsql;
        }
    }
}
