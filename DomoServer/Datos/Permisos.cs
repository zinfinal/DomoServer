using DomoServer.Modelo;
using DomoServer.Utilidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Datos
{
    class Permisos
    {
        private static List<Permiso> permisos;
        
        public static bool loadPermisos()
        {
            permisos = new List<Permiso>();

            MySqlCommand cmd = new MySqlCommand("SELECT PERid, NIVid, DISid, PACid, PERvalor FROM uptPERtPermiso", Db.Conexion);
            
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                permisos.Add(new Permiso(dr.GetInt32("PERid"), dr.GetInt32("NIVid"), dr.GetInt32("DISid"), dr.GetInt32("PACid"), dr.GetByte("PERvalor")));
            }

            dr.Close();

            Log.d("[Permisos] Cargado Correctamente.");

            return true;
        }

        public static bool allow(int NIVid, int DISid, int PACid)
        {
            var q = from p in permisos
                    where p.NIVid == NIVid && p.DISid == DISid && p.PACid == PACid
                    select new { p.PERvalor};

            //return q.First().PERvalor == 1;
            return true;
        }

        public static bool denied(int NIVid, int DISid, int PACid)
        {
            return !allow(NIVid, DISid, PACid);
        }
    }
}