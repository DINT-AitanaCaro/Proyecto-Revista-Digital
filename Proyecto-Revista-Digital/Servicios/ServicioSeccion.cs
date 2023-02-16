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
    /// <summary>
    ///     Servicio para gestionar las secciones con la base de datos
    /// </summary>
    class ServicioSeccion
    {
        /// <summary>
        ///     Conexión con la base de datos.
        /// </summary>
        private SqliteConnection conexion = new SqliteConnection(Properties.Settings.Default.SQLiteLocation);

        /// <summary>
        ///     Método para añadir una sección nueva a la base de datos.
        /// </summary>
        /// <param name="seccion">Seccion a añadir a la bd</param>
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

        /// <summary>
        ///     Método para obtener todas las secciones almacenadas en la base de datos.
        /// </summary>
        /// <returns>ObservableCollection de secciones</returns>
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
