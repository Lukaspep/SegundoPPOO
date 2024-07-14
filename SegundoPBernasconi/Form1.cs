using SegundoParcialPOO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoPBernasconi
{
    public partial class Form1 : Form
    {
        // Instancia de la clase ListaProveedores
        private ListaDeProveedores listProveedor = new ListaDeProveedores();
        public Form1()
        {
            InitializeComponent();
            // Inicializacion de las columnas del DataGridView para proveedores (dgv1)
            dgv1.Columns.Add("Legajo", "Legajo");
            dgv1.Columns.Add("Nombre", "Nombre");

            // Inicializacion de las columnas del DataGridView para pagos (dgv2)
            dgv2.Columns.Add("ID", "ID");
            dgv2.Columns.Add("Importe", "Importe $");
            dgv2.Columns.Add("Fecha", "Fecha");
            dgv2.Columns.Add("TipoDePago", "TipoDePago");
            dgv2.Columns.Add("Recargo", "Recargo");
            dgv2.Columns.Add("Total", "Total");
        }
        // Metodo para verificar si un texto contiene numeros
        private bool ContieneNumeros(string texto)
        {
            return texto.Any(char.IsDigit);
        }
        // Metodo para mostrar un proveedor en el DataGridView de proveedores
        private void MostrarProveedorEnGrid(Proveedor proveedor)
        {
            // Verificar si las columnas ya estan agregadas
            if (dgv1.Columns.Count == 0)
            {
                dgv1.Columns.Add("Legajo", "Legajo");
                dgv1.Columns.Add("Nombre", "Nombre");
            }
            // Agregar una nueva fila con los datos del proveedor
            dgv1.Rows.Add(proveedor.Legajo, proveedor.Nombre);
        }
        // Metodo para limpiar y actualizar el DataGridView de proveedores
        private void LimpiarGrid()
        {
            dgv1.Rows.Clear();
            foreach (var proveedor in listProveedor.ObtenerTodos())
            {
                dgv1.Rows.Add(proveedor.Legajo, proveedor.Nombre);
            }
        }
        // Metodo para limpiar y actualizar el DataGridView de pagos de un proveedor
        private void LimpiarGridPagos(Proveedor proveedor)
        {
            dgv2.Rows.Clear();
            if (proveedor != null)
            {
                foreach (var pago in proveedor.ObtenerPagos())
                {
                    decimal recargo = pago.CalcularRecargo(DateTime.Now);
                    decimal totalConRecargo = pago.CalcularTotalAPagar(DateTime.Now);
                    dgv2.Rows.Add(pago.ID, pago.Importe, pago.FechaVencimiento, pago.TipoPago, recargo, totalConRecargo);
                }
            }
        }
        // Metodo manejador del evento de cambio de seleccion en el DataGridView de proveedores
        private void ActualizarPagosAlSeleccionarProveedor(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv1.SelectedRows[0];
                int legajoProveedor = Convert.ToInt32(selectedRow.Cells["Legajo"].Value);
                Proveedor proveedor = listProveedor.ObtenerProveedorPorLegajo(legajoProveedor);
                LimpiarGridPagos(proveedor);
            }
        }
        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    throw new Exception("Por favor, complete todos los campos.");
                }
                if (ContieneNumeros(txtNombre.Text))
                {
                    throw new Exception("El campo nombre proveedor no puede contener números.");
                }
                // Crear un nuevo proveedor y agregarlo a la lista
                Proveedor nuevoProveedor = new Proveedor(txtNombre.Text);
                listProveedor.AgregarProveedor(nuevoProveedor);
                // Mostrar el nuevo proveedor en el DataGridView
                MostrarProveedorEnGrid(nuevoProveedor);

                MessageBox.Show("El proveedor fue dado de alta correctamente.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dgv1.SelectedRows[0];

                    int idProveedor = Convert.ToInt32(row.Cells["Legajo"].Value);

                    string newNombre = txtNombre.Text;
                    // Modificar el proveedor en la lista
                    listProveedor.ModificarProveedor(idProveedor, newNombre);

                    // Actualizar el DataGridView de proveedores
                    LimpiarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al modificar el proveedor", MessageBoxButtons.OK);
                }
            }
        }
        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgv1.SelectedRows)
                {
                    int idProveedor = Convert.ToInt32(row.Cells["Legajo"].Value);
                    Proveedor proveedorAEliminar = listProveedor.ObtenerTodos().FirstOrDefault(proveedor => proveedor.Legajo == idProveedor);
                    if (proveedorAEliminar != null)
                    {
                        listProveedor.EliminarProveedor(proveedorAEliminar);
                        LimpiarGridPagos(null);
                    }
                }
                LimpiarGrid();
                
                MessageBox.Show("Proveedor eliminado(s) exitosamente.", "Eliminar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    throw new Exception("Seleccione al proveedor a pagar");
                }
                DataGridViewRow selectedRow = dgv1.SelectedRows[0];
                int legajoProveedor = Convert.ToInt32(selectedRow.Cells["Legajo"].Value);
                Proveedor proveedor = listProveedor.ObtenerProveedorPorLegajo(legajoProveedor);

                decimal importe = Convert.ToDecimal(txtImporte.Text);
                DateTime fechaDeVenc = dtpFecha.Value;
                string metodoDePago = comboBox1.SelectedItem.ToString();

                // Crear un nuevo pago y agregarlo al proveedor
                Pago newPago = new Pago(importe, fechaDeVenc, metodoDePago);
                proveedor.AgregarPago(newPago);

                // Actualizar el DataGridView de pagos
                LimpiarGridPagos(proveedor);

                MessageBox.Show("El pago fue agregado al proveedor", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
