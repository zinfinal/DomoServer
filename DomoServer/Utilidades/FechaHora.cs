using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Utilidades
{
    class FechaHora
    {
        public static string FechaFormato(string formato)//funcion que vuelde la fecha en formato
        {
            DateTime time = DateTime.Now;//FUNCION 

            return time.ToString(formato);
        }

        public static string Fecha()
        {
            return FechaFormato("MM/d/yyyy");
        }

        public static string Hora()
        {
            return FechaFormato("HH:mm:ss");
        }

        public static string Hora(Boolean full)
        {
            return FechaFormato("T");
        }
    }
}
