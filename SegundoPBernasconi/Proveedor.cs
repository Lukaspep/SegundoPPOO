using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SegundoPBernasconi
{
    public class Proveedor
    {
        //contador de legajo unico para cada proveedor
        private static int contLegajo = 0;
        public int Legajo { get; private set; } //solo lectura
        public string Nombre { get; set; }
        private List<Pago> pagos;

        public Proveedor(string nombre)       //Constructor
        {
            this.Legajo = ++contLegajo; // Incrementa el contador de legajo y asigna el valor al proveedor
            this.Nombre = nombre;
            this.pagos = new List<Pago>();
        }

        public void AgregarPago(Pago pago) //Metodo para agregar el pago al proveedor
        {
            pagos.Add(pago);
        }

        public List<Pago> ObtenerPagos() // Metodo para obtener la lista de pagos del proveedor
        {
            return pagos;
        }
    }
}
