﻿using Microsoft.Data.Sqlite;
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
                  id INTEGER primary key
                  nombre varchar(100) NOT NULL,                      
                  imagen varchar(100),                      
                  nickname varchar(50),  
                  social varchar(50) CHECK( social IN ('Instagram','Twitter','Facebook') ) 
                                    )";
            comando.ExecuteNonQuery();

            comando.CommandText = @"CREATE TABLE IF NOT EXISTS articulos (
                  titulo varchar(100) primary key,                      
                  imagen varchar(100),                      
                  contenido varchar(50),  
                                    )";
            conexion.Close();
        }
    }
}
