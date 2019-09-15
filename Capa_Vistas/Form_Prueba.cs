using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Datos.Conexion;
using Capa_Datos.Entidades;
namespace Capa_Vistas
{
    public partial class Form_Prueba : Form
    {
        public Form_Prueba()
        {
            InitializeComponent();

            if (MetodosDB.ComprobarDB() != 1)
            {
                MessageBox.Show("Fail");
            }
        }

        public void refresh() {
            dataGridView1.DataSource = null;
            List<Producto> l = MetodosDB.Productos();
            dataGridView1.DataSource = l;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var O = new Producto() {
                Codigo = 4428463,
                Nombre = "Prueba c#",
                Precio = 30,
                Stock = 20,
                Marca = 1,
                Categoria = 1
            };

            if (MetodosDB.AgregarProducto(O) != 1)
            {
                MessageBox.Show("Fail");

            }
            else { MessageBox.Show("Realizado correctamente"); refresh();}
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
