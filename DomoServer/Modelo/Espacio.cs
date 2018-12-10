using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class Espacio
    {
        public int ESPid { get; set; }
        public int TESid { get; set; }
        public String ESPnombre { get; set; }
        public String ESPdecripcion { get; set; }
        public String ESPip { get; set; }
        public int ESPpuerto { get; set; }
        public int ESPestado { get; set; }


        public Espacio(int ESPid, int TESid, String ESPnombre, String ESPdecripcion, String ESPip, int ESPpuerto, int ESPestado)
        {
            this.ESPid = ESPid;
            this.TESid = TESid;
            this.ESPnombre = ESPnombre;
            this.ESPdecripcion = ESPdecripcion;
            this.ESPip = ESPip;
            this.ESPpuerto = ESPpuerto;
            this.ESPestado = ESPestado;
        }
    }
}
