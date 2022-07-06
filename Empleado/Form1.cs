using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Empleado
{
    public partial class Form1 : Form
    {
        long cantidad = 0;
        long n = 0;
        List<Empleado> empleados = new List<Empleado>();
        public Form1()
        {
            InitializeComponent();
        }
        #region Campos vacios
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if(!int.TryParse(txtCodigo.Text, out int codigo) || !float.TryParse(txtComision.Text, out float comision)
                || !float.TryParse(txtSalario.Text, out float salario))
            {
                MessageBox.Show("No escriba letras en campos numericos");
                return;
            }
            Empleado empleado = new Empleado()
            {
                Codigo = codigo,
                Comision = comision,
                Direccion = txtDireccion.Text,
                Nombre = txtNombre.Text,
                Salario = salario
            };
            empleados.Add(empleado);

            Stream myStream = new FileStream("empleado.txt", FileMode.Open, FileAccess.Write);
            XmlSerializer serializador = new XmlSerializer(typeof(List<Empleado>));
            serializador.Serialize(myStream, empleados);
            myStream.Close();
            dataGridView1.DataSource = LeerEmpleados();
        }

        public List<Empleado> LeerEmpleados()
        {
            
            Stream myStream = new FileStream("empleado.txt", FileMode.OpenOrCreate, FileAccess.Read);
            if (myStream.Length == 0)
            {
                myStream.Close();
                return new List<Empleado>();
            }
            XmlSerializer deserializador = new XmlSerializer(typeof(List<Empleado>));
            empleados = (List<Empleado>)deserializador.Deserialize(myStream);
            myStream.Close();
            return empleados;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LeerEmpleados();
        }
    }
}
