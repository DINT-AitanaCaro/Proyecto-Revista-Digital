using Microsoft.Data.Sqlite;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioSeccion
    {
        private SqliteConnection conexion = new SqliteConnection("Data Source=revista.db");

        public void AddSeccion(Seccion seccion)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO secciones VALUES(null,@nombre)";
            comando.Parameters.Add("@nombre", SqliteType.Text);

            comando.Parameters["@nombre"].Value = seccion.NombreSeccion;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public ObservableCollection<Seccion> GetSecciones()
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM secciones";
            ObservableCollection<Seccion> secciones = new ObservableCollection<Seccion>();
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    secciones.Add(new Seccion(Convert.ToInt32(lector["id"]),  (string)lector["nombre"]));
                }
            }
            lector.Close();
            conexion.Close();
            return secciones;
        }
    }
}
