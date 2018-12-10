using DomoServer.Datos;
using DomoServer.Modelo;
using DomoServer.Paquetes.Servidor;
using DomoServer.Red;
using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Paquetes
{
    class RpLogin : PacketReader
    {
        Cliente cliente;

        public RpLogin(byte[] packetData, Cliente cliente) : base(packetData)
        {
            this.cliente = cliente;
        }

        public void loginUsuario()
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            byte[] codigo = ReadBytes(10);
            byte[] contrasena = ReadBytes(6);
            byte tipo = ReadByte();
            byte[] TiempoActual = ReadBytes(10);
            byte[] CliVersion = ReadBytes(3);

            string codigo_ = ConvertidorTipos.ByteArrayToString(codigo);
            string contrasena_ = ConvertidorTipos.ByteArrayToString(contrasena);

            TipoCliente tipo_ = TipoCliente.Espacio;

            switch(tipo)
            {
                case 0x00:
                    tipo_ = TipoCliente.Usuario;
                    break;
                case 0x01:
                    tipo_ = TipoCliente.Espacio;
                    break;
            }
            
            string TiempoActual_ = ConvertidorTipos.ByteArrayToString(TiempoActual);
            string CliVersion_ = ConvertidorTipos.ByteArrayToString(CliVersion);

            byte result = 0x00;

            Log.dBlack("Enviado para ingresar: ({0}){1}", cliente.Puerto.ToString(), codigo_);

            if (CliVersion_.Equals(Config.APP_VERSION))
            {
                Usuario usuario = Usuarios.Login(codigo_, contrasena_);

                if (usuario.USUid == 0)
                {
                    result = 0x02;
                }
                else
                {
                    if(usuario.USUonline == 1)
                    {
                        result = 0x03;
                    }
                    else
                    {
                        result = 0x01;
                        
                        cliente.Tipo = TipoCliente.Usuario;

                        cliente.Usuario = usuario;

                        Usuarios.SetOnline(usuario.USUid, 1);

                        Log.dBlack("Login ({0}): ({1})", cliente.Ip, codigo_);
                    }
                    
                }
            }
            else
            {
                result = 0x06;
            }
            
            //

            PacketWriter pw = new PacketWriter();
            pw.WriteByte((byte) 0xC1);
            pw.WriteByte(sizeCode);
            pw.WriteByte(headCode);
            pw.WriteByte(subCode);
            pw.WriteByte(result);

            cliente.Send(pw.Compile());
        }

        public void loginEspacio()
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            byte id = ReadByte();
            //byte[] contrasena = ReadBytes(10);

            //string codigo = TypeConverter.ByteArrayToString(codigo);
            //string contrasena = TypeConverter.ByteArrayToString(contrasena);

            Log.dBlue("Enviado para ingresar: ({0}){1}", cliente.Puerto.ToString(), "Aula C-101 - Ethernet Shield");

            cliente.Tipo = TipoCliente.Espacio;

            Espacio espacio = new Espacio(1, 1, "Aula C-101", "Salon C-101", "192.168.0.5", 5252, 1);
            
            cliente.Espacio = espacio;
            /*
            byte[] data = new byte[] { opCode, sizeCode, sizeCode, subCode };
            socket.Send(data);
            */
            Log.dBlue("Login ({0}): ({1})", cliente.Ip.ToString(), "Aula C-101");
        }

        internal void GetEspacios(List<Cliente> clientes)
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            byte espacio = ReadByte();
            byte pin = ReadByte();
            byte estado = ReadByte();

            cliente.Send(new SpEspacios().GetEspacios());
        }
    }
}