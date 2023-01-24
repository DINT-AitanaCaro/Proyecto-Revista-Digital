using Microsoft.Data.Sqlite;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioSQLite
    {
        private SqliteConnection conexion = new SqliteConnection("Data Source=revista.db");
        public void CrearBD()
        {
            //Abrimos conexión
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = @"CREATE TABLE IF NOT EXISTS autores (
                  nombre varchar(100),                      
                  imagen varchar(100),                      
                  nickname varchar(50),  
                  social varchar(50) CHECK( social IN ('Instagram','Twitter','Facebook') ) 
                                    )";
            comando.ExecuteNonQuery();

            comando.CommandText = @"CREATE TABLE IF NOT EXISTS articulos (
                  titulo varchar(100),                      
                  imagen varchar(100),                      
                  contenido varchar(50),  
                                    )";
        }

        public void AddAutor(Autor autor)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO autores VALUES(@nombre,@imagen,@nickname,@social)";
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@nickname", SqliteType.Text);
            comando.Parameters.Add("@social", SqliteType.Text);

            comando.Parameters["@nombre"].Value = autor.Nombre;
            comando.Parameters["@imagen"].Value = autor.Imagen;
            comando.Parameters["@nickname"].Value = autor.Nickname;
            comando.Parameters["@social"].Value = autor.Social;

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
