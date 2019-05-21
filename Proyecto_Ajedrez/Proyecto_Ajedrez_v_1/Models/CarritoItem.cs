using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Ajedrez_v_1.Models
{
    public class CarritoItem
    {
        private producto _producto;
        private int _cantidad;

        public producto Producto
        {
            get
            {
                return _producto;
            }

            set
            {
                _producto = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return _cantidad;
            }

            set
            {
                _cantidad = value;
            }
        }

        public CarritoItem()
        { }

        public CarritoItem(producto productoC, int cantidad)
        {
            this._producto = productoC;
            this._cantidad = cantidad;
        }
    }
}