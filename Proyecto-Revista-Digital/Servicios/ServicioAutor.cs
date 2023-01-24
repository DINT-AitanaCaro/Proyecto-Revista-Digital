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
    class ServicioAutor
    {
        private SqliteConnection conexion = new SqliteConnection("Data Source=revista.db");
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

        public ObservableCollection<Autor> GetAutores(int autorId)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM autores";
            ObservableCollection<Autor> autores = new ObservableCollection<Autor>();
            SqliteDataReader lector = comando.ExecuteReader();
            if(lector.HasRows)
            {
                while(lector.Read())
                {
                    //autores.Add(new Autor((int)lector["id"],(string)lector["nombre"]), (string)lector["imagen"]), (string)lector["nickname"]), (string)lector["social"]));
                }
            }
            lector.Close();
            conexion.Close();
            return autores;
        }
    }
}
