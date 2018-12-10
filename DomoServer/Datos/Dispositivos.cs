using DomoServer.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Datos
{
    class Dispositivos
    {
        public static Dispositivo GetById(int DISid)
        {
            Dispositivo dispositivo = new Dispositivo();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM uptDIStDispositivo WHERE DISid = ?DISid", Db.Conexion);
            cmd.Parameters.AddWithValue("DISid", DISid);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                dispositivo.DISid = dr.GetInt32("DISid");
                dispositivo.ESPid = dr.GetInt32("ESPid");
                dispositivo.TDIid = dr.GetInt32("TDIid");
                dispositivo.DISnombre = dr.GetString("DISnombre");
                dispositivo.DISmodo = dr.GetInt32("DISmodo");
                dispositivo.DISpin = dr.GetInt32("DISpin");
                dispositivo.PACid = dr.GetInt32("PACid");
                dispositivo.DIScompuerta_logica = dr.GetByte("DIScompuerta_logica");
                dispositivo.DISestado = dr.GetByte("DISestado");
            }

            dr.Close();

            return dispositivo;
        }

        public static void SetPACid(int DISid, int PACid)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE uptDIStDispositivo SET PACid = ?PACid WHERE DISid = ?DISid", Db.Conexion);
            cmd.Parameters.AddWithValue("DISid", DISid);
            cmd.Parameters.AddWithValue("PACid", PACid);
            cmd.ExecuteNonQuery();
        }
    }
}
