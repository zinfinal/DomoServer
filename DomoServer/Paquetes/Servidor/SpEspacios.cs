using DomoServer.Modelo;
using DomoServer.Red;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Paquetes.Servidor
{
    class SpEspacios : PacketWriter
    {
        public byte[] GetEspacios()
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
    }
}
