using Microsoft.Data.Sqlite;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.VistasModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    /// <summary>
    ///     Servicio para crear la base de datos SQLite de la revista
    /// </summary>
    class ServicioSQLite
    {
        /// <summary>
        ///     Variable de tipo SqliteConncetion para la conexión
        /// </summary>
        private SqliteConnection conexion = new SqliteConnection(Properties.Settings.Default.SQLiteLocation);
        /// <summary>
        ///     En este método se crea la base de datos, es llamado desde:
        /// <see cref="MainWindowVM">MainWindowVM</see>
        /// </summary>
        public void CrearBD()
        {
            //Abrimos conexión
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            //comando.CommandText = @"DROP TABLE IF EXISTS autores;";
            comando.CommandText = @"CREATE TABLE IF NOT EXISTS autores (
                    id INTEGER primary key,
                    nombre varchar(100) NOT NULL,                      
                    imagen varchar(100),                      
                    nickname varchar(50),  
                    social varchar(50) CHECK( social IN ('Instagram','Twitter','Facebook') ) 
                                    )";
            comando.ExecuteNonQuery();

            //comando.CommandText = @"DROP TABLE IF EXISTS articulos;";
            comando.CommandText = @"CREATE TABLE IF NOT EXISTS articulos (
                    id INTEGER primary key,
                    idAutor INTEGER,
                    idSeccion varchar(50),
                    titulo varchar(100) UNIQUE,
                    imagen varchar(100),                      
                    contenido varchar(50), 
                    publicado INTEGER,
                    urlPdf varchar(250),
                    FOREIGN KEY (idAutor) REFERENCES autores(id)
                    FOREIGN KEY (idSeccion) REFERENCES secciones(id)
                                    )";

            comando.ExecuteNonQuery();

            //comando.CommandText = @"DROP TABLE IF EXISTS secciones;";

            comando.CommandText = @"CREATE TABLE IF NOT EXISTS secciones (
                    id INTEGER primary key,
                     nombre varchar(100) NOT NULL UNIQUE
                                    )";

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
