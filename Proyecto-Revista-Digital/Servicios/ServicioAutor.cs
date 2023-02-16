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
    ///     Servicio para realizar operaciones con los autores en la base de datos.
    /// </summary>
    class ServicioAutor
    {
        /// <summary>
        ///     Conexión con la base de datos.
        /// </summary>
        private SqliteConnection conexion = new SqliteConnection(Properties.Settings.Default.SQLiteLocation);
        /// <summary>
        ///     Método para añadir un autor a la base de datos.
        /// </summary>
        /// <param name="autor">Autor que se desea añadir a la base de datos.</param>
        public void AddAutor(Autor autor)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO autores VALUES(null,@nombre,@imagen,@nickname,@social)";
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

        /// <summary>
        ///     Método para editar un autor en la base de datos.
        /// </summary>
        /// <param name="autor">Autor a editar en la base de datos.</param>
        public void EditAutor(Autor autor)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE autores SET nombre = @nombre, imagen = @imagen, nickname = @nickname, social = @social WHERE id = @id";

            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@imagen", SqliteType.Text);
            comando.Parameters.Add("@nickname", SqliteType.Text);
            comando.Parameters.Add("@social", SqliteType.Text);
            comando.Parameters.Add("@id", SqliteType.Integer);

            comando.Parameters["@nombre"].Value = autor.Nombre;
            comando.Parameters["@imagen"].Value = autor.Imagen;
            comando.Parameters["@nickname"].Value = autor.Nickname;
            comando.Parameters["@social"].Value = autor.Social;
            comando.Parameters["@id"].Value = autor.Id;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        /// <summary>
        ///     Método para eliminar un autor de la base de datos.
        /// </summary>
        /// <param name="autorId">Id del autor a eliminar.</param>
        public void DeleteAutor(int autorId)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM autores WHERE id = @id";

            comando.Parameters.Add("@id", SqliteType.Integer);

            comando.Parameters["@id"].Value = autorId;

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        /// <summary>
        ///     Método para obtener todos los autores en la base de datos.
        /// </summary>
        /// <returns>ObservableCollection de autores.</returns>
        public ObservableCollection<Autor> GetAutores()
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM autores";
            ObservableCollection<Autor> autores = new ObservableCollection<Autor>();
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    autores.Add(new Autor(Convert.ToInt32(lector["id"]), (string)lector["nombre"], (string)lector["imagen"], (string)lector["nickname"], (string)lector["social"]));
                }
            }
            lector.Close();
            conexion.Close();
            return autores;
        }

        /// <summary>
        ///     Método para obtener un autor de la base de datos.
        /// </summary>
        /// <param name="id">Id del autor que se desea obtener.</param>
        /// <returns>Autor de la base de datos con el mismo id que el recibido.</returns>
        public Autor GetAutor(int id)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM autores WHERE id = @id";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters["@id"].Value = id;
            Autor autor = null;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                autor = new Autor(Convert.ToInt32(lector["id"]), (string)lector["nombre"], (string)lector["imagen"], (string)lector["nickname"], (string)lector["social"]);
            }
            lector.Close();
            conexion.Close();
            return autor;
        }
    }
}
