using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Cliente_NaturalApp
{
    public class NaturalDB
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Prueba;" +
            "Integrated Security=true";
        public List<Natural> Get()
        {
            List<Natural> list = new List<Natural>();
            string queryString = "SELECT Nombre, Apellido, Cedula, Edad FROM Natural";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Natural oNatural = new Natural();
                        oNatural.Nombre = reader.GetString(1);
                        oNatural.Apellido = reader.GetString(2);
                        oNatural.Cedula = reader.GetString(3);
                        oNatural.Edad = reader.GetInt32(4);
                        list.Add(oNatural);
                    }
                    reader.Close();
                }catch (Exception ex)
                {
                    throw new Exception($"Ha ocurrido un error con al BD: {ex}");
                }
            }
            return list;
        }

        public void Add(Natural natural)
        {
            string queryString = "INSERT INTO Natural(Nombre, Apellido, Cedula, Edad)" +
                "VALUES(@nombre, @apellido, @cedula, @edad)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@nombre", natural.Nombre);
                command.Parameters.AddWithValue("@apellido", natural.Apellido);
                command.Parameters.AddWithValue("@cedula", natural.Cedula);
                command.Parameters.AddWithValue("@edad", natural.Edad);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }catch (Exception ex)
                {
                    throw new Exception($"Ha ocurrido un error con la BD: {ex}");
                }
            }
        }
    }
    public class Natural
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
    }
}
