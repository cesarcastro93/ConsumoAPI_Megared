using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Tool
{
    public class EntregarCambio
    {
        public int DineroDeCambio(int Numero1, int Numero2)
        {
            int numero1 = Numero1;
            int numero2 = Numero2;
            int resultado = numero1 - numero2;

            return resultado;
        }
    }
}