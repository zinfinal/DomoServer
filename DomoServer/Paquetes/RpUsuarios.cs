using DomoServer.Datos;
using DomoServer.Modelo;
using DomoServer.Red;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Paquetes
{
    class RpUsuarios : PacketReader
    {
        Cliente cliente;

        public RpUsuarios(byte[] packetData, Cliente cliente) : base(packetData)
        {
            this.cliente = cliente;
        }

        public void CambiarUbicacion()
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int ubicacion = ReadInt32();

            cliente.Usuario.USUubicacion = ubicacion;

            Usuarios.SetUbicacion(cliente.Usuario.USUid, cliente.Usuario.USUubicacion);

            PacketWriter pw = new PacketWriter();
            pw.WriteByte((byte)0xC1);
            pw.WriteByte(0x08);
            pw.WriteByte(0xF1);
            pw.WriteByte(0x03);
            pw.WriteInt32(ubicacion);

            cliente.Send(pw.Compile());
        }

    }
}
