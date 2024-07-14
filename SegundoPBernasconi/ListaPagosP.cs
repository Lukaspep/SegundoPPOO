using SegundoPBernasconi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcialPOO
{
    internal class ListaProveedoresEnumerator : IEnumerator
    {
        private List<Proveedor> _proveedores;
        private int posicion = -1;
        public ListaProveedoresEnumerator(List<Proveedor> proveedor) // Constructor que recibe la lista de proveedores
        {
            _proveedores = proveedor;
        }

        public object Current
        {
            get
            {
                if (posicion < 0 || posicion >= _proveedores.Count)
                    throw new InvalidOperationException();
                return _proveedores[posicion];
            }
        }

        public bool MoveNext() // Metodo para mover el enumerador a la siguiente posicion
        {
            posicion++;
            return (posicion < _proveedores.Count);
        }

        public void Reset() // Metodo para resetear el enumerador a la posicion inicial
        {
            posicion = -1;
        }

        public void Dispose()
        {
            
        }
    }
}
