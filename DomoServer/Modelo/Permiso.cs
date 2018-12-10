using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class Permiso
    {
        public int PERid { get; set; }
        public int NIVid { get; set; }
        public int DISid { get; set; }
        public int PACid { get; set; }
        public int PERvalor { get; set; }
        
        public Permiso(int PERid, int NIVid, int DISid, int PACid, int PERvalor)
        {
            this.PERid = PERid;
            this.NIVid = NIVid;
            this.DISid = DISid;
            this.PACid = PACid;
            this.PERvalor = PERvalor;
        }
    }
}
