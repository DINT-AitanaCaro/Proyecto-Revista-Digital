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
    ///     Servicio para gestionar y realizar operaciones con los artículos de la base de datos.
    /// </summary>
    class ServicioArticulo
    {
        /// <summary>
        ///     Conexión con la base de datos.
        /// </summary>
        private SqliteConnection conexion = new SqliteConnection(Properties.Settings.Default.SQLiteLocation);
      
        /// <summary>
        ///     Método para añadir un artículo a la base de datos.
        /// </summary>
        /// <param name="articulo">Artículo que se desea añadir a la base de datos.</param>
        public void AddArticulo(Articulo articulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO articulos VALUES(null,@idAutor,@idSeccion,@titulo,@imagen,@contenido,@publicado,@urlPdf)";
            comando.Parameters.Add("@idAutor", SqliteType.Integer);
            comando.Parameters.Add("@idSeccion", SqliteType.Text);
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@contenido", SqliteType.Text);
            comando.Parameters.Add("@publicado", SqliteType.Integer);
            comando.Parameters.Add("@urlPdf", SqliteType.Text);

            comando.Parameters["@idAutor"].Value = articulo.AutorArticulo.Id;
            comando.Parameters["@idSeccion"].Value = articulo.IdSeccion;
            comando.Parameters["@titulo"].Value = articulo.Titulo;
            comando.Parameters["@imagen"].Value = articulo.Imagen;
            comando.Parameters["@contenido"].Value = articulo.Contenido;
            comando.Parameters["@publicado"].Value = articulo.Publicado;
            comando.Parameters["@urlPdf"].Value = string.IsNullOrEmpty(articulo.UrlPdf) ? string.Empty : articulo.UrlPdf;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        /// <summary>
        ///     Método para eliminar un artñiculo de la base de datos.
        /// </summary>
        /// <param name="Idarticulo">Id del artículo que se quiere eliminar.</param>
        public void DeleteArticulo(int Idarticulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM articulos WHERE id = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);

            comando.Parameters["@id"].Value = Idarticulo;

            comando.ExecuteNonQuery();
            conexion.Close();
        }
        
        /// <summary>
        ///     Método para obtener todos los artículos de la base de datos.
        /// </summary>
        /// <returns>ObservableCollection de articulos.</returns>
        public ObservableCollection<Articulo> GetArticulos()
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM articulos";
            ObservableCollection<Articulo> articulos = new ObservableCollection<Articulo>();
            SqliteDataReader lector = comando.ExecuteReader();
            ServicioAutor sa = new ServicioAutor();
            if(lector.HasRows)
            {
                while (lector.Read())
                {
                    articulos.Add(new Articulo(Convert.ToInt32(lector["id"]), sa.GetAutor(Convert.ToInt32(lector["idAutor"])), Convert.ToInt32(lector["idSeccion"]), (string)lector["titulo"], (string)lector["imagen"], (string)lector["contenido"], Convert.ToInt32(lector["publicado"])==1, (string)lector["urlPdf"]));
                }
            }
            lector.Close();
            conexion.Close();
            return articulos;
        }

        /// <summary>
        ///     Método para obrener los artículos de una sección.
        /// </summary>
        /// <param name="IdSeccion">Id de la seccion de la que se desea obtener los artículos.</param>
        /// <returns>ObservableCollection de artículos.</returns>
        public ObservableCollection<Articulo> GetArticulosPorSeccion(int IdSeccion)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM articulos WHERE idSeccion = @id AND publicado = 1";

            comando.Parameters.Add("@id", SqliteType.Integer);

            comando.Parameters["@id"].Value = IdSeccion;

            ObservableCollection<Articulo> articulos = new ObservableCollection<Articulo>();
            SqliteDataReader lector = comando.ExecuteReader();
            ServicioAutor sa = new ServicioAutor();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    articulos.Add(new Articulo(Convert.ToInt32(lector["id"]), sa.GetAutor(Convert.ToInt32(lector["idAutor"])), Convert.ToInt32(lector["idSeccion"]), (string)lector["titulo"], (string)lector["imagen"], (string)lector["contenido"], Convert.ToInt32(lector["publicado"]) == 1, (string)lector["urlPdf"]));
                }
            }
            lector.Close();
            conexion.Close();
            return articulos;
        }

        /// <summary>
        ///     Método para marcar un artículo como publicaodo.
        /// </summary>
        /// <param name="idArticulo">Id del artículo a marcar como publicado.</param>
        public void PublicarArticulo(int idArticulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE articulos SET publicado = 1 WHERE id = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters["@id"].Value = idArticulo;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        /// <summary>
        ///     Método para actualizar la url del pdf correspondiente a un artículo.
        /// </summary>
        /// <param name="idArticulo">Id del artículo a actualizar.</param>
        /// <param name="urlPdf">URL del pdf.</param>
        public void UpdateUrlPdf(int idArticulo, string urlPdf)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE articulos SET urlPdf = @urlPdf WHERE id = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@urlPdf", SqliteType.Text);
            comando.Parameters["@id"].Value = idArticulo;
            comando.Parameters["@urlPdf"].Value = urlPdf;

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
