using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleado
{
    [Serializable]
    public class Empleado
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public float Salario { get; set; }
        public float Comision { get; set; }
    }
}
