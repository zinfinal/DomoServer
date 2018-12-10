using DomoServer.Datos;
using DomoServer.Modelo;
using DomoServer.Red;
using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static DomoServer.Modelo.TipoDispositivo.Acciones;

namespace DomoServer.Paquetes
{
    class RpDispositivos: PacketReader
    {
        Cliente cliente;

        public RpDispositivos(byte[] packetData, Cliente cliente) : base(packetData)
        {
            this.cliente = cliente;
        }
        internal void Dispositivo(List<Cliente> clientes)
        {

            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int dispositivo = ReadByte();
            byte pin = ReadByte();
            byte estado = ReadByte();


            byte[] data = new byte[] { 0xC2, 0xF1, 0x00, 0x01, pin, estado };

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Espacio)
                {
                    cliente.Send(data);
                }
            }

        }

        internal void EnviarFoco(List<Cliente> clientes)
        {

            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int DISid = ReadInt32();
            int PACid = ReadInt32();
            
            Dispositivo dispositivo = Dispositivos.GetById(DISid);
            
            switch (PACid)
            {
                case Foco.Encender:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo.DISid, Foco.Encender))
                    {
                        //Dispositivos.SetPACid(DISid, PACid);
                        
                        PacketWriter pwUsuario = new PacketWriter();

                        pwUsuario.WriteByte(0xC4);
                        pwUsuario.WriteByte(0x00);
                        pwUsuario.WriteByte(0xF1);
                        pwUsuario.WriteByte(0x01);
                        pwUsuario.WriteInt32(dispositivo.DISid);
                        pwUsuario.WriteInt32(PACid);

                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0x06, 0xF1, 0x01, (byte)dispositivo.DISpin, 0x01 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(pwUsuario.Compile());

                                //if (cliente.Usuario.USUubicacion == Ubicacion.fragment_dispositivos)
                                //{
                                //    cliente.Send(pwUsuario.Compile());
                                //}
                                
                            }
                        }

                        //data_espacio = new byte[] { 0xC2, 0x00, 0xF1, 0x01, pin, 0x01 };
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0x00, 0xF1, 0x00, 0x00 });
                    }
                    break;
                case Foco.Apagar:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo.DISid, Foco.Apagar))
                    {
                        PacketWriter pwUsuario = new PacketWriter();

                        pwUsuario.WriteByte(0xC4);
                        pwUsuario.WriteByte(0x00);
                        pwUsuario.WriteByte(0xF1);
                        pwUsuario.WriteByte(0x01);
                        pwUsuario.WriteInt32(dispositivo.DISid);
                        pwUsuario.WriteInt32(PACid);

                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0x06, 0xF1, 0x01, (byte)dispositivo.DISpin, 0x00 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(pwUsuario.Compile());
                            }
                        }

                        //data_espacio = new byte[] { 0xC2, 0x00, 0xF1, 0x01, pin, 0x00 };
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0x00, 0xF1, 0x00, 0x00 });
                    }
                    break;
            }

            /*
            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Espacio)
                {
                    cliente.Send(data_espacio);
                }
                else if (cliente.Tipo == TipoCliente.Usuario)
                {
                    
                    cliente.Send(pwUsuario.Compile());
                }
            }
            */

        }

        internal void EnviarVentilador(List<Cliente> clientes)
        {

            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int DISid = ReadInt32();
            int PACid = ReadInt32();

            Dispositivo dispositivo = Dispositivos.GetById(DISid);

            switch (PACid)
            {
                case Ventilador.Encender:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo.DISid, Ventilador.Encender))
                    {
                        PacketWriter pwUsuario = new PacketWriter();

                        pwUsuario.WriteByte(0xC4);
                        pwUsuario.WriteByte(0x00);
                        pwUsuario.WriteByte(0xF1);
                        pwUsuario.WriteByte(0x01);
                        pwUsuario.WriteInt32(dispositivo.DISid);
                        pwUsuario.WriteInt32(PACid);

                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0x06, 0xF1, 0x01, (byte)dispositivo.DISpin, 0x01 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(pwUsuario.Compile());

                                //if (cliente.Usuario.USUubicacion == Ubicacion.fragment_dispositivos)
                                //{
                                //    cliente.Send(pwUsuario.Compile());
                                //}

                            }
                        }

                        //data_espacio = new byte[] { 0xC2, 0x00, 0xF1, 0x01, pin, 0x01 };
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0x00, 0xF1, 0x00, 0x00 });
                    }
                    break;
                case Ventilador.Apagar:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo.DISid, Ventilador.Apagar))
                    {
                        PacketWriter pwUsuario = new PacketWriter();

                        pwUsuario.WriteByte(0xC4);
                        pwUsuario.WriteByte(0x00);
                        pwUsuario.WriteByte(0xF1);
                        pwUsuario.WriteByte(0x01);
                        pwUsuario.WriteInt32(dispositivo.DISid);
                        pwUsuario.WriteInt32(PACid);

                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0x06, 0xF1, 0x01, (byte)dispositivo.DISpin, 0x00 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(pwUsuario.Compile());
                            }
                        }

                        //data_espacio = new byte[] { 0xC2, 0x00, 0xF1, 0x01, pin, 0x00 };
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0x00, 0xF1, 0x00, 0x00 });
                    }
                    break;
            }

        }

        /*
        internal void EnviarFoco(List<Cliente> clientes)
        {

            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int dispositivo = ReadInt32();
            int accion = ReadInt32();

            //
            byte pin = 2;

            switch (accion)
            {
                case Foco.Encender:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo, Foco.Encender))
                    {
                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0xF1, 0x00, 0x01, pin, 0x01 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(new byte[] { 0xC4, 0xF1, 0x00, 0x01, pin, 0x01 });
                            }
                        }
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0xF1, 0x00, 0x02, pin, 0x01 });
                    }
                    break;
                case Foco.Apagar:
                    if (Permisos.allow(cliente.Usuario.NIVid, dispositivo, Foco.Apagar))
                    {
                        foreach (Cliente cliente in clientes)
                        {
                            if (cliente.Tipo == TipoCliente.Espacio)
                            {
                                cliente.Send(new byte[] { 0xC2, 0xF1, 0x00, 0x01, pin, 0x00 });
                            }
                            else if (cliente.Tipo == TipoCliente.Usuario)
                            {
                                cliente.Send(new byte[] { 0xC4, 0xF1, 0x00, 0x01, pin, 0x00 });
                            }
                        }
                    }
                    else
                    {
                        cliente.Send(new byte[] { 0xC5, 0xF1, 0x00, 0x02, pin, 0x01 });

                        return;
                    }
                    break;
            }
        }
        */

        /*
        public byte[] GetDispositivos()
        {

            List<Espacio> espacios = new List<Espacio>();

            espacios.Add(new Espacio(1, 1, "C-101", "Salon C-101", "192.168.0.5", 5252, 1));
            espacios.Add(new Espacio(2, 1, "C-102", "Salon C-102", "192.168.0.6", 5253, 1));
            espacios.Add(new Espacio(3, 1, "C-103", "Salon C-103", "192.168.0.7", 5255, 1));

            int size = 7 + (espacios.Count * 34);
            WriteByte(0xC1);//opCode
            WriteByte(size);//sizeCode
            WriteByte(0xF3);//headCode
            WriteByte(0x00);//subcode


            WriteByte(0x00);

            WriteByte(espacios.Count);

            for (int i = 0; i < espacios.Count; ++i)
            {
                WriteInt32(espacios[i].ESPid);
                WriteInt32(espacios[i].TESid);
                WriteString(espacios[i].ESPnombre, 10);
                WriteString(espacios[i].ESPdecripcion, 25);
                WriteString(espacios[i].ESPip, 15);
                WriteInt16(espacios[i].ESPpuerto);
                WriteInt16(espacios[i].ESPestado);
                WriteByte(0x00);//??

            }

            return Compile();
        }
        */
        internal void AbrirPuerta(List<Cliente> clientes)
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int DISid = ReadInt32();
            int PACid = ReadInt32();

            Dispositivo dispositivo = Dispositivos.GetById(DISid);
            
            byte[] data = new byte[] { 0xC2, 0x05, 0xF1, 0x02, (byte)dispositivo.DISpin };

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Espacio)
                {
                    cliente.Send(data);
                }
            }

        }

        internal void EnviarTemperatura(List<Cliente> clientes)
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            int lectura = ReadInt32();

            double mv = (lectura / 1024.0) * 5000;
            double temperatura = mv / 10;


            PacketWriter pw = new PacketWriter();
            
            pw.WriteByte(0xC4);
            pw.WriteByte(0xF1);
            pw.WriteByte(0x00);
            pw.WriteByte(0x01);
            pw.WriteInt32(12);
            //pw.WriteFloat(temperatura);

            //byte[] data = new byte[] { 0xC2, 0xF1, 0x00, 0x01, pin, estado };

            var data = pw.Compile();
            Log.dPink("Temperatura:" + temperatura);

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Usuario)
                {
                    cliente.Send(data);
                }
            }
        }

        internal void AbrirCortina(List<Cliente> clientes)
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            byte DISpin = ReadByte();

            byte[] data = new byte[] { 0xC2, 0x05, 0xF1, 0x03, DISpin };

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Espacio)
                {
                    cliente.Send(data);
                }
            }

        }

        internal void CerrarCortina(List<Cliente> clientes)
        {
            byte opCode = ReadByte();
            byte sizeCode = ReadByte();
            byte headCode = ReadByte();
            byte subCode = ReadByte();

            byte DISpin = ReadByte();

            byte[] data = new byte[] { 0xC2, 0x05, 0xF1, 0x04, DISpin };

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Tipo == TipoCliente.Espacio)
                {
                    cliente.Send(data);
                }
            }

        }
    }
}
