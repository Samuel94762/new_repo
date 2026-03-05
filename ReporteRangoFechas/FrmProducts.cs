using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace ReporteRangoFechas
{
    public partial class FrmProducts : Form
    {
        private bool esNuevo = false;
        private bool esEditar = false;

        public FrmProducts()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtNombre, "Ingrese nombre del producto");
            ttMensaje.SetToolTip(pxImagen, "Agregue imagen del producto");
            txtIdProducto.Enabled = false;
        }
        //Mostrar Mensaje de confirmación
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Bike Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Mostrar Mensaje de error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Bike Store", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Limpiar()
        {
            txtIdProducto.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtModeloAño.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            pxImagen.Image = global::ReporteRangoFechas.Properties.Resources.BiciIcono;
        }
        //Habilitar los controles del formulario 
        private void Habilitar (bool valor)
        {
            txtIdProducto.ReadOnly = !valor;
            txtNombre.ReadOnly = !valor;
            txtModeloAño.ReadOnly = !valor;
            txtPrecio.ReadOnly = !valor;
            btnCargar.Enabled = valor;
            btnLimpiar.Enabled = valor;
        }
        //Habilitar los botones
        private void Botones()
        {
            if (esNuevo || esEditar)
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }
        //Metodo para ocultar columnas
        private void OcultarColumnas()
        {
            if(dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false;
                dataListado.Columns[1].Visible = false;
            }
        }
        //Metodo visualizar registros
        private void Mostrar()
        {
            dataListado.DataSource = Nproducts.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            tabControl1.SelectedIndex = 0;
        }
        private void BuscarProducto()
        {
            dataListado.DataSource = Nproducts.BuscarNombre(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void txtModeloAño_TextChanged(object sender, EventArgs e)
        {

        }
        private void FrmProducts_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                pxImagen.Image = Image.FromFile(dialog.FileName);
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pxImagen.Image = global::ReporteRangoFechas.Properties.Resources.BiciIcono;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            esEditar = false;
            Botones();
            Limpiar();
            Habilitar(true);
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (txtNombre.Text == string.Empty || txtModeloAño.Text == string.Empty ||
                    txtPrecio.Text == string.Empty)
                {
                    MensajeError("Faltó ingresar algunos datos, serán marcados");
                    errorIcono.SetError(txtNombre, "Ingrese el nombre del producto");
                    errorIcono.SetError(txtModeloAño, "Ingrese el modelo del producto");
                    errorIcono.SetError(txtPrecio, "Ingrese el precio del producto");

                }
                else { }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
