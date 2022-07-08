using Newtonsoft.Json;
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

            //Se inicia el flujo con el archivo
            using (Stream myStream = new FileStream("empleadoJson.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                //Se serializa la lista de empleados
                string jsonString = JsonConvert.SerializeObject(empleados);

                //Se sobreescribira el archivo con la clase StreamWriter con el archivo myStream como parametro
                using(StreamWriter writer = new StreamWriter(myStream))
                {
                    //Se sobreescribe la lista de Empleados serializada en el archivo
                    writer.WriteLine(jsonString);
                }
            }
            //Se lee el archivo
            dataGridView1.DataSource = LeerEmpleados();
        }

        public List<Empleado> LeerEmpleados()
        {
            //Se inicial el flujo con el archivo que contiene el Json
            using(Stream myStream = new FileStream("empleadoJson.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                //Se verifica si hay escrito algo en el archivo y sino se retorna la lista vacia
                if (myStream.Length == 0)
                {
                    return empleados;
                }

                //Se leera el archivo con la clase StreamReader
                using(StreamReader reader = new StreamReader(myStream))
                {
                    //Se lee la lista de empleados serializada y se guarda en una variable
                    string json = reader.ReadToEnd();

                    //Se deserializa el json hacia la lista para posteriormente retornarla
                    //y mostrarla en el DataGrid o para cualquier otro uso
                    empleados = JsonConvert.DeserializeObject<List<Empleado>>(json);
                }
            }
            return empleados;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LeerEmpleados();
        }
    }
}
