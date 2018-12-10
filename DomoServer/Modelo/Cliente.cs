using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class Cliente
    {
        public int Id { get; set; }
        public TipoCliente Tipo { get; set; }
        public Socket Socket { get; set; }

        public String Ip { get; set; }
        public int Puerto { get; set; }

        public Usuario Usuario { get; set; }
        public Espacio Espacio { get; set; }

        public int Send(byte[] buffer) {
            int r = 0;

            if (Socket != null && Socket.Connected)
            {
                lock (Socket)
                {
                    r = Socket.Send(buffer);
                    Log.dBlack("Enviado: " + BitConverter.ToString(buffer));
                }
            }

            return r;
        }
    }
}
