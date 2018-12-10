using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomoServer.Utilidades
{
    class Log
    {
        private static RichTextBox box;

        public static void setBindingBox(RichTextBox box)
        {
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Log.box = box;
        }
        public static void d(String texto)
        {
            Log.print("" + texto, Color.Red, true);
        }

        public static void d(string format, params string[] value)
        {
            Log.print(string.Format(format, value), Color.Red, true);
        }

        public static void dBlack(string format, params string[] value)
        {
            Log.print(string.Format(format, value), Color.Black, true);
        }

        public static void dBlue(string format, params string[] value)
        {
            Log.print(string.Format(format, value), Color.Blue, true);
        }

        public static void dPink(string format, params string[] value)
        {
            Log.print(string.Format(format, value), Color.Pink, true);
        }

        public static void dGreen(string format, params string[] value)
        {
            Log.print(string.Format(format, value), Color.Green, true);
        }

        public static void i(String texto, Color color)
        {
            Log.print(texto, color, true);
        }

        public static void p(String texto)
        {
            Log.print(texto, Color.Black, false);
        }

        public static void p(String texto, Color color)
        {
            Log.print(texto, color, false);
        }

        private static void print(String texto, Color color, Boolean showDate)
        {
            string log = "";

            if(showDate)
            {
                log = FechaHora.Hora() + " ";
            }

            log = log + texto + "\r\n";

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(log);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();

        }
    }
}
