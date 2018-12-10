using DomoServer.Datos;
using DomoServer.Red;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DomoServer
{
    class ServidorCore
    {
        public ServidorSocket socketServer = new ServidorSocket();

        public ServidorCore()
        {
            
            if (!Db.Inicializar("localhost", "3306", "upthink", "root", ""))
            {
                return;
            }

            Usuarios.SetOfflineTodos();

            if (!Permisos.loadPermisos())
            {
                return;
            }
            
            socketServer.Inicializar(IPAddress.Parse("172.30.105.65"), 7070);
        }

        public void StartGame()
        {

            socketServer.Escuchar();

            //Thread thread = new Thread(new ThreadStart(MainLoop));
            //thread.Start();
            // MainLoop();
        }
    }
}
