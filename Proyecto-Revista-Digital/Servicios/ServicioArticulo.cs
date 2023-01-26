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
    /*class ServicioArticulo
    {
        private SqliteConnection conexion = new SqliteConnection("Data Source=revista.db");
        public void AddArticulo(Articulo articulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO articulos VALUES(@titulo,@idAutor,@seccion,@imagen,@contenido,@publicado)";
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@idAutor", SqliteType.Integer);
            comando.Parameters.Add("@seccion", SqliteType.Text);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@contenido", SqliteType.Text);
            comando.Parameters.Add("@publicado", SqliteType.Integer);

            comando.Parameters["@titulo"].Value = articulo.Titulo;
            comando.Parameters["@idAutor"].Value = articulo.IdAutor;// o Autor.Id al pasar a Articulo hay q consultar al autor de ese id para parsearlo
            comando.Parameters["@seccion"].Value = articulo.Seccion;
            comando.Parameters["@imagen"].Value = articulo.Imagen;
            comando.Parameters["@contenido"].Value = articulo.Contenido;
            comando.Parameters["@publicado"].Value = articulo.Publicado;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void EditArticulo(Articulo articulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE articulos SET titulo = @titulo, idAutor = @idAutor, seccion = @seccion, contenido  = @contenido, publicado = @publicado WHERE id = @id";
            
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@idAutor", SqliteType.Integer);
            comando.Parameters.Add("@seccion", SqliteType.Integer);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@contenido", SqliteType.Text);
            comando.Parameters.Add("@publicado", SqliteType.Integer);

            comando.Parameters["@titulo"].Value = articulo.Titulo;
            comando.Parameters["@idAutor"].Value = articulo.IdAutor;
            comando.Parameters["@seccion"].Value = articulo.Seccion;
            comando.Parameters["@imagen"].Value = articulo.Imagen;
            comando.Parameters["@contenido"].Value = articulo.Contenido;
            comando.Parameters["@publicado"].Value = articulo.Publicado;
            
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void DeleteArticulo(int articuloId)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM articulos WHERE id = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);

            comando.Parameters["@id"].Value = articuloId;

            comando.ExecuteNonQuery();
            conexion.Close();
        }
        
        public ObservableCollection<Articulo> GetArticulos()
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM articulos";
            ObservableCollection<Articulo> articulos = new ObservableCollection<Articulo>();
            SqliteDataReader lector = comando.ExecuteReader();
            if(lector.HasRows)
            {
                while (lector.Read())
                {
                    articulos.Add(new Articulo(Convert.ToInt32(lector["titulo"]), (string)lector["i"], (string)lector["imagen"], (string)lector["nickname"], (string)lector["social"]));
                }
            }
            lector.Close();
            conexion.Close();
            return autores;
        }
    }*/
}
