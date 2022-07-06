using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente_NaturalApp
{
    internal class ClientesDB
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Prueba;
Integrated Security=True";

        public List<Cliente> Get()
        {
            List<Cliente> list = new List<Cliente>();

            // Proporciona a la cadena de consulta
            string queryString = "SELECT Direccion, Telefono, Email FROM Cliente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Crea el objeto Comando
                SqlCommand command = new SqlCommand(queryString, connection);

                // Abra la conexión en un bloque try/catch.
                // Cree y ejecute el DataReader
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cliente oClientes = new Cliente();
                        oClientes.Direccion = reader.GetString(1);
                        oClientes.Telefono = reader.GetString(2);
                        oClientes.Email = reader.GetString(3);
                        list.Add(oClientes);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la BD" + ex.Message);
                }
            }
            return list;
        }

        public void Add(Cliente cliente)
        {
            string queryString = "INSERT INTO Cliente(Direccion, Telefono, Email)" +
                "VALUES(@direccion, @telefono, @email)";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@email", cliente.Email);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Hay un error en al BD: {ex}");
                }
            }
        }

        public void Update(Cliente cliente)
        {
            string queryString = "UPDATE Cliente SET Direccion = @direccion" +
                "Telefono = @telefono" +
                "Email = @email" +
                "WHERE Id = @id";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@direccion", cliente.Direccion);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@email", cliente.Email);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public void Delete(int id)
        {
            string queryString = "DELETE FROM Cliente WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    throw new Exception($"Ha ocurrido un error en la BD: {ex}");
                }
            }
        }
        
    }

    public class Cliente
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

    }
}
