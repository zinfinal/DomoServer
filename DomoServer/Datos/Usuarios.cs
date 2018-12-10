using DomoServer.Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Datos
{
    class Usuarios
    {
        public static Usuario Login(string codigo, string password)
        {
            Usuario usuario = new Usuario();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM uptUSUpUsuario WHERE USUusuario = ?USUusuario AND USUpassword = ?USUpassword", Db.Conexion);
            cmd.Parameters.AddWithValue("USUusuario", codigo);
            cmd.Parameters.AddWithValue("USUpassword", password);

            MySqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.Read())
            {
                usuario.USUid = dr.GetInt32("USUid");
                usuario.NIVid = dr.GetInt32("NIVid");
                usuario.USUusuario = dr.GetString("USUusuario");
                usuario.USUpassword = dr.GetString("USUpassword");
                usuario.USUimagen = dr.GetString("USUimagen");
                usuario.USUemail = dr.GetString("USUemail");
                usuario.USUonline = dr.GetByte("USUonline");
                usuario.USUestado = dr.GetByte("USUestado");
                usuario.USUfecha_creado = dr.GetString("USUfecha_creado");                
            }

            dr.Close();

            return usuario;
        }

        public static void SetOnline(int USUid, int USUonline)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE uptUSUpUsuario SET USUonline = ?USUonline WHERE USUid = ?USUid", Db.Conexion);
            cmd.Parameters.AddWithValue("USUid", USUid);
            cmd.Parameters.AddWithValue("USUonline", USUonline);
            cmd.ExecuteNonQuery();
        }

        public static void SetOfflineTodos()
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE uptUSUpUsuario SET USUonline = ?USUonline", Db.Conexion);
            cmd.Parameters.AddWithValue("USUonline", 0);
            cmd.ExecuteNonQuery();
        }

        public static void SetUbicacion(int USUid, int USUubicacion)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE uptUSUpUsuario SET USUubicacion = ?USUubicacion WHERE USUid = ?USUid", Db.Conexion);
            cmd.Parameters.AddWithValue("USUid", USUid);
            cmd.Parameters.AddWithValue("USUubicacion", USUubicacion);
            cmd.ExecuteNonQuery();
        }

        public static void ObtenerUsuarios()
        {

        }
    }
}
