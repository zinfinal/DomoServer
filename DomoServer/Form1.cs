using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomoServer
{
    public partial class Form1 : Form
    {
        ServidorCore gameCore;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.setBindingBox(richTextBox1);

            gameCore = new ServidorCore();
            gameCore.StartGame();

            /*
            Log.d("[ServerCore] Empezando ...");

            Log.p("[BasDeDatos] Conexión Ok");

            Log.d("[ServerCore] servidor con éxito inicializado, escuchando...");
            */
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "DomoServer Version 1.001.10A\nCopyright (C) 2016 Master Project", "Acerca");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void encenderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (var s in gameCore.socketServer.Clientes) {
                byte[] b = { 0x01 };
                s.Socket.Send(b);
            }
        }

        private void apagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var s in gameCore.socketServer.Clientes)
            {
                byte[] b = { 0x00 };
                s.Socket.Send(b);
            }
        }
    }
}
