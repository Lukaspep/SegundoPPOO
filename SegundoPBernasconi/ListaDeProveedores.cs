using SegundoPBernasconi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SegundoParcialPOO
{
    internal class ListaDeProveedores : IEnumerable
    {
        // Lista de proveedores
        private List<Proveedor> proveedores;

        public ListaDeProveedores() // Constructor que inicializa la lista de proveedores
        {
            proveedores = new List<Proveedor>();
        }

        public void AgregarProveedor(Proveedor proveedor) // Metodo para agregar un proveedor a la lista
        {
            proveedores.Add(proveedor);
        }

        public void EliminarProveedor(Proveedor proveedor) // Metodo para eliminar un proveedor de la lista
        {
            proveedores.Remove(proveedor);
        }

        public void ModificarProveedor(int legajo, string nombre) // Metodo para modificar el nombre de un proveedor identificado por su legajo
        {
            foreach (Proveedor proveedor in proveedores)
            {
                if (proveedor.Legajo == legajo)
                {
                    proveedor.Nombre = nombre;
                    break;
                }
            }
        }
        public Proveedor ObtenerProveedorPorLegajo(int legajo) // Metodo para obtener un proveedor por su legajo
        {
            return proveedores.FirstOrDefault(proveedor => proveedor.Legajo == legajo);
        }

        public List<Proveedor> ObtenerTodos() // Metodo para obtener todos los proveedores en una lista
        {
            return proveedores;
        }

        // Implementacion del metodo GetEnumerator para permitir la enumeracion de la lista de proveedores
        public IEnumerator GetEnumerator() 
        {
            return new ListaProveedoresEnumerator(proveedores);
        }
    }
}
