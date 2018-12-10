using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class TipoDispositivo
    {
        public int TDIid { get; set; }
        public string TDInombre { get; set; }
        public string TDIimagen { get; set; }
        
        public static class Acciones
        {
            public static class Desconocido
            {
                public const int Encender = -3;
                public const int Apagar = -2;
                public const int Mostrar = -1;
            }
            public static class Foco
            {
                public const int Encender = 1;
                public const int Apagar = 2;
                public const int Mostrar = 3;
            }

            public static class Ventilador
            {
                public const int Encender = 4;
                public const int Apagar = 5;
                public const int Mostrar = 6;
            }

            public static class Cortina
            {
                public const int Abrir = 7;
                public const int Cerrar = 8;
                public const int Mostrar = 9;
            }

            public static class Puerta
            {
                public const int Abrir = 10;
                public const int Ver_Estado = 11;
                public const int Mostrar = 12;
            }
            public static class Proyector
            {
                public const int Encender = 13;
                public const int Apagar = 14;
                public const int Mostrar = 15;
            }
            public static class Camara_de_Video
            {
                public const int Encender = 16;
                public const int Apagar = 17;
                public const int Transmitir = 18;
                public const int Mostrar = 19;
            }
            public static class Temperatura
            {
                public const int Ver_temperatura = 20;
            }
            public static class Sensor_de_Movimiento
            {
                public const int Ver_Estado = 21;
            }
        }
    }
}
