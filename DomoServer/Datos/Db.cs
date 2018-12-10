using DomoServer.Utilidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Datos
{
    class Db
    {
        public static MySqlConnection Conexion { get; set; }

        private static string host;
        private static string port;
        private static string databaseName;
        private static string login;
        private static string password;

        public static string ConnectionString
        {
            get { return string.Format("Server={0}; Database={1}; Port={2}; UserID={3}; Password={4}", host, databaseName, port, login, password); }
        }

        public static bool Inicializar(string dbHost, string dbPort, string dbName, string dbLogin, string dbPassword)
        {

            try
            {
                host = dbHost;
                port = dbPort;
                databaseName = dbName;
                login = dbLogin;
                password = dbPassword;

                Log.d("[BaseDeDatos] Inicializar la conexión con - Servidor: {0} Puerto: {1} Base de Datos: {2} Usuario: {3} Contraseña: {4}", host, port, databaseName, login, new String('-', password.Length));

                return CheckConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static bool CheckConnection()
        {
            try
            {
                Conexion = new MySqlConnection(ConnectionString);
                Conexion.Open();

                Log.d("[BaseDeDatos] Conexión Ok");
                return true;
            }
            catch (Exception exception)
            {
                Log.d("[BaseDeDatos] La conexión falló");
                return false;
            }
        }
    }
}
