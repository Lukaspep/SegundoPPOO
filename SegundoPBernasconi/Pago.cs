using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SegundoPBernasconi
{
    public class Pago
    {
        private static int contID = 0; // contador de ID unico para cada pago
        public int ID { get; private set; }
        public decimal Importe { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string TipoPago { get; private set; }

        public Pago(decimal importe, DateTime fechaVencimiento, string tipoPago) //Constructor de Pago
        {
            this.ID = ++contID;
            this.Importe = importe;
            this.FechaVencimiento = fechaVencimiento;
            this.TipoPago = tipoPago;
        }

        public decimal CalcularRecargo(DateTime fechaPago) // Metodo que me permite calcular el recargo que se le aplica, en base al metodo de pago elegido
        {
            decimal recargo = 0;

            if (fechaPago > FechaVencimiento)
            {
                if (TipoPago == "Efectivo")
                {
                    recargo = Importe * 0.01m;
                }
                else if (TipoPago == "Cheque")
                {
                    recargo = Importe * 0.10m;
                }
            }

            return recargo;
        }

        public decimal CalcularTotalAPagar(DateTime fechaPago) //Metodo para calcular la suma total
        {
            return Importe + CalcularRecargo(fechaPago);
        }
    }
}
