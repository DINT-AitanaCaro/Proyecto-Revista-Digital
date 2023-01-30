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
    class ServicioArticulo
    {
        private SqliteConnection conexion = new SqliteConnection(Properties.Settings.Default.SQLiteLocation);
      
        public void AddArticulo(Articulo articulo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO articulos VALUES(null,@idAutor,@idSeccion,@titulo,@imagen,@contenido,@publicado)";
            comando.Parameters.Add("@idAutor", SqliteType.Integer);
            comando.Parameters.Add("@idSeccion", SqliteType.Text);
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@contenido", SqliteType.Text);
            comando.Parameters.Add("@publicado", SqliteType.Integer);

            comando.Parameters["@idAutor"].Value = articulo.AutorArticulo.Id;
            comando.Parameters["@idSeccion"].Value = articulo.IdSeccion;
            comando.Parameters["@titulo"].Value = articulo.Titulo;
            comando.Parameters["@imagen"].Value = articulo.Imagen;
            comando.Parameters["@contenido"].Value = articulo.Contenido;
            comando.Parameters["@publicado"].Value = articulo.Publicado;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

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
                    articulos.Add(new Articulo(Convert.ToInt32(lector["id"]), sa.GetAutor(Convert.ToInt32(lector["idAutor"])), Convert.ToInt32(lector["idSeccion"]), (string)lector["titulo"], (string)lector["imagen"], (string)lector["contenido"], Convert.ToInt32(lector["publicado"])==1));
                }
            }
            lector.Close();
            conexion.Close();
            return articulos;
        }

        public void UpdateArticulo()
        {

        }
    }
}
