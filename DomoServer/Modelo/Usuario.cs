using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Modelo
{
    class Usuario
    {
        public int USUid { get; set; }
        public int NIVid { get; set; }
        public string USUusuario { get; set; }
        public string USUpassword { get; set; }
        public string USUimagen { get; set; }
        public string USUemail { get; set; }
        public int USUubicacion { get; set; }
        public byte USUonline { get; set; }
        public byte USUestado { get; set; }
        public string USUfecha_creado { get; set; }
    }
}
