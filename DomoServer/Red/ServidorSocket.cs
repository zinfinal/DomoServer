using DomoServer.Datos;
using DomoServer.Modelo;
using DomoServer.Paquetes;
using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Red
{
    class ServidorSocket
    {
        public int Puerto { get; set; }
        public IPAddress Ip { get; set; }
        public Socket Socket { get; set; }
        public List<Cliente> Clientes { get; set; }

        private byte[] _buffer = new byte[1024];

        public void Inicializar(IPAddress ip, int puerto)
        {
            this.Ip = ip;
            this.Puerto = puerto;

            Log.d("[ServidorSocket] Inicializar por Ip: {1} Puerto: {0}", Puerto.ToString(), Ip.ToString());
        }

        public void Escuchar()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(new IPEndPoint(Ip, Puerto));

            Socket.Listen(10);

            Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Clientes = new List<Cliente>();

            Log.d("[ServidorSocket] Comenzó Escuchar ");
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Cliente cliente = new Cliente();

            try { 
                Socket socket = Socket.EndAccept(ar);

                var iep = ((IPEndPoint)socket.RemoteEndPoint);

                var ip = iep.Address;
                var puerto = iep.Port;

                cliente = new Cliente();

                cliente.Socket = socket;
                cliente.Ip = ip.ToString();
                cliente.Puerto = puerto;
                cliente.Tipo = TipoCliente.Desconocido;

                lock(Clientes) {
                    Clientes.Add(cliente);
                }

                Log.dBlack("[ServidorSocket] Nuevo Cliente conectado: {0}", socket.RemoteEndPoint.ToString());

                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                //socket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), socket);
            }
            catch (SocketException e)
            {
                if (cliente.Socket != null)
                {
                    cliente.Socket.Close();

                    lock (Clientes)
                    {
                        Clientes.Remove(cliente);
                    }

                    Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                }
            }
            catch (Exception e)
            {
                if (cliente.Socket != null)
                {
                    cliente.Socket.Close();

                    lock (Clientes)
                    {
                        Clientes.Remove(cliente);
                    }

                    Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                }
            }
        }
        
        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

            if (socket.Connected)
            {
                int recibidos;

                try
                {
                    recibidos = socket.EndReceive(ar);
                }
                catch (Exception ex)
                {
                    for (int j = 0; j < Clientes.Count; j++)
                    {
                        if (Clientes[j].Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            Log.d("[ServidorSocket] Cliente Desconectado: " + Clientes[j].Socket.RemoteEndPoint.ToString());

                            switch(Clientes[j].Tipo)
                            {
                                case TipoCliente.Usuario:
                                    Usuarios.SetOnline(Clientes[j].Usuario.USUid, 0);
                                    break;
                            }

                            Clientes[j].Socket.Close();

                            lock (Clientes) {
                                Clientes.RemoveAt(j);
                            }
                        }
                    }
                    return;
                }
                
                if (recibidos > 0)
                {
                    if (_buffer[0].Equals(0xC1) || _buffer[0].Equals(0xC2) || _buffer[0].Equals(0xC3) || _buffer[0].Equals(0xC4))
                    {
                        for (int i = 1; i < _buffer.Length;)
                        {
                            int Size = _buffer[i];
                            byte[] TempArray = new byte[Size];

                            Array.Copy(_buffer, TempArray, Size);

                            if (TempArray[0].Equals(0xC1) || TempArray[0].Equals(0xC2) || TempArray[0].Equals(0xC3) || TempArray[0].Equals(0xC4))
                            {
                                Protocolo(socket, TempArray);
                            }

                            if (_buffer[Size].Equals(0xC1) || _buffer[Size].Equals(0xC2) || _buffer[Size].Equals(0xC3) || _buffer[Size].Equals(0xC4))
                            {
                                i += _buffer[i];
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Log.d("[ServidorSocket] Paquete Desconocido OpCode desde el cliente [{0}]", socket.RemoteEndPoint.ToString());
                    }

                    Array.Clear(_buffer, 0, _buffer.Length);
                }
                else
                {
                    for (int j = 0; j < Clientes.Count; j++)
                    {
                        if (Clientes[j].Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                        {
                            Log.d("[ServidorSocket] Cliente Desconectado: " + Clientes[j].Socket.RemoteEndPoint.ToString());
                            switch (Clientes[j].Tipo)
                            {
                                case TipoCliente.Usuario:
                                    Usuarios.SetOnline(Clientes[j].Usuario.USUid, 0);
                                    break;
                            }

                            Clientes[j].Socket.Close();

                            lock (Clientes)
                            {
                                Clientes.RemoveAt(j);
                            }
                        }
                    }
                    return;
                }

                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }

            
        }
        
        private void Protocolo(Socket socket, byte[] TempArray)
        {
            Log.dBlack("Recibido: " + BitConverter.ToString(TempArray));

            PacketReader packetReader = new PacketReader(TempArray);

            byte opCode = packetReader.ReadByte();
            byte sizeCode = packetReader.ReadByte();
            byte headCode = packetReader.ReadByte();
            byte subCode = packetReader.ReadByte();

            switch (opCode)
            {
                case 0xC1:
                    switch (subCode)
                    {
                        case 0x01:
                            new RpLogin(TempArray, getUsuarioBySocket(socket)).loginUsuario();
                            break;
                        case 0x02:
                            new RpLogin(TempArray, getUsuarioBySocket(socket)).loginEspacio();
                            break;
                        case 0x03:
                            new RpUsuarios(TempArray, getUsuarioBySocket(socket)).CambiarUbicacion();
                            break;
                    }
                    break;
                case 0xC2:
                    switch (subCode)
                    {
                        case 0x00:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).Dispositivo(Clientes);
                            break;
                        case 0x01:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).EnviarFoco(Clientes);
                            break;
                        case 0x02:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).AbrirPuerta(Clientes);
                            break;
                        case 0x03:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).EnviarVentilador(Clientes);
                            break;
                        case 0x05:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).AbrirCortina(Clientes);
                            break;
                        case 0x06:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).CerrarCortina(Clientes);
                            break;
                    }
                    break;
                case 0xC3:
                    switch (subCode)
                    {
                        case 0x01:
                            new RpDispositivos(TempArray, getUsuarioBySocket(socket)).EnviarTemperatura(Clientes);
                            break;
                    }
                    break;
                case 0xC4:
                    switch (subCode)
                    {
                        case 0x01:
                            //new RpDispositivos(TempArray, getUsuarioBySocket(socket)).EnviarTemperatura(Clientes);
                            break;
                    }
                    break;
                case 0xC5://Permisos

                    break;

            }
        }

        public Cliente getUsuarioBySocket(Socket socket)
        {
            for (int j = 0; j < Clientes.Count; j++)
            {
                if (Clientes[j].Socket.RemoteEndPoint.ToString().Equals(socket.RemoteEndPoint.ToString()))
                {
                    return Clientes[j];
                }
            }

            return null;
        }
    }
}
