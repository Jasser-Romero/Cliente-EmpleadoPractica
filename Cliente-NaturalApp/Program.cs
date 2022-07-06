using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente_NaturalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClientesDB db = new ClientesDB();
            NaturalDB db1 = new NaturalDB();
            int tipo;
            Console.WriteLine("Seleccione un tipo:");
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Natural");
            tipo = int.Parse(Console.ReadLine());

            switch (tipo)
            {
                case 1:
                    Cliente cliente = new Cliente();
                    Console.WriteLine("Escriba su direccion:");
                    cliente.Direccion = Console.ReadLine();

                    Console.WriteLine("Escriba su telefono:");
                    cliente.Telefono = Console.ReadLine();

                    Console.WriteLine("Escriba su Email:");
                    cliente.Email = Console.ReadLine();

                    db.Add(cliente);
                    Console.WriteLine("Se ha registrado al cliente");
                    break;
                case 2:
                    Natural natural = new Natural();
                    Console.WriteLine("Escriba su nombre:");
                    natural.Nombre = Console.ReadLine();

                    Console.WriteLine("Escriba sus apellidos:");
                    natural.Apellido = Console.ReadLine();

                    Console.WriteLine("Escriba su cedula:");
                    natural.Cedula = Console.ReadLine();

                    Console.WriteLine("Escriba su edad:");
                    natural.Edad = int.Parse(Console.ReadLine());

                    db1.Add(natural);
                    Console.WriteLine("Se ha registrado al natural");
                    break;
                default:
                    Console.WriteLine("Ingreso un dato invalido");
                    break;
            }
        }
    }
}
