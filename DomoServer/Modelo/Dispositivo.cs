using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class Dispositivo
    {
        public int DISid { get; set; }
        public int ESPid { get; set; }
        public int TDIid { get; set; }
        public string DISnombre { get; set; }
        public int DISmodo { get; set; }
        public int DISpin { get; set; }
        public int PACid { get; set; }
        public int DIScompuerta_logica { get; set; }
        public string DISvalor { get; set; }
        public int DISestado { get; set; }
    }
}
