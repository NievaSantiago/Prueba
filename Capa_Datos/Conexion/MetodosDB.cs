using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.IO;
using Capa_Datos.Entidades;
namespace Capa_Datos.Conexion
{
    public class MetodosDB
    {

        private static string Ruta = "C:/Users/nieva/Desktop/Sistema_Kiosco/Base_De_Datos/Kiosco.db";
        private static string ConS = @"Data Source=" + Ruta+";Version=3";

        public static int ComprobarDB()
        {
            if (!File.Exists(Ruta)) return -1;//si el archivo db no existe el metodo retorna -1.
            else { return 1; }//si el archivo db esxite el metodo retorna 1
        }
        public MetodosDB()
        {

        }
        //añadir nuevo producto
        public static int AgregarProducto(Producto producto)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConS))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd;
                    cmd = new SQLiteCommand("INSERT INTO Productos (Codigo,Nombre,Precio,Stock,Marca,Categoria) VALUES (@CODIGO,@NOMBRE,@PRECIO,@STOCK,@MARCA,@CATEGORIA)", connection);
                    cmd.Parameters.AddWithValue("CODIGO", producto.Codigo);
                    cmd.Parameters.AddWithValue("NOMBRE", producto.Nombre.ToString());
                    cmd.Parameters.AddWithValue("PRECIO", producto.Precio);
                    cmd.Parameters.AddWithValue("STOCK", producto.Stock);
                    cmd.Parameters.AddWithValue("MARCA", producto.Marca);
                    cmd.Parameters.AddWithValue("CATEGORIA", producto.Categoria);
                    cmd.ExecuteNonQuery();
                    
                    return 1;
                }
                catch (Exception err)
                {
                    return -1;
                }
            }

        }

        //eliminar producto
        public static int EliminarProducto(int codigo)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConS))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd;
                    cmd = new SQLiteCommand("DELETE FROM Productos WHERE Codigo=@CODIGO", connection);
                    cmd.Parameters.AddWithValue("CODIGO",codigo);                 
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception err)
                {
                    return -1;
                }
            }
        }
        //buscar producto
        public static List<Producto> BuscarPor(int codigo = 0, string nombre = null, string marca = null, string categoria = null)
        {
            List<Producto> ListP;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConS))
                {
                    if (codigo != 0)
                    {
                        connection.Open();
                        var cmd = new SQLiteCommand(("SELECT Codigo,Nombre,Precio,Stock,NombreM,NombreC FROM Productos P LEFT JOIN Marcas M ON(P.Marca == M.Id)LEFT JOIN Categorias C ON(P.Categoria == C.Id) WHERE Codigo == @CODIGO") , connection);
                        cmd.Parameters.AddWithValue("CODIGO",codigo);
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        ListP = new List<Producto>();
                        while (dr.Read())
                        {
                            ListP.Add(new Producto
                            {
                                Codigo = Convert.ToInt64(dr["Codigo"]),
                                Nombre = dr["Nombre"].ToString(),
                                Precio = Convert.ToDouble(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                MarcaS = dr["NombreM"].ToString(),
                                CategoriaS = dr["NombreC"].ToString()

                            }); 
                        }
                        connection.Close();
                        return ListP;
                    }
                    if (nombre != null)
                    {
                        //SELECT * FROM Productos WHERE Nombre LIKE '%p%';
                        connection.Open();
                        var cmd = new SQLiteCommand(("SELECT Codigo,Nombre,Precio,Stock,NombreM,NombreC FROM Productos P LEFT JOIN Marcas M ON(P.Marca == M.Id)LEFT JOIN Categorias C ON(P.Categoria == C.Id) WHERE Nombre LIKE '%@NOMBRE%'"), connection);
                        cmd.Parameters.AddWithValue("NOMBRE", nombre);
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        ListP = new List<Producto>();
                        while (dr.Read())
                        {
                            ListP.Add(new Producto
                            {
                                Codigo = Convert.ToInt64(dr["Codigo"]),
                                Nombre = dr["Nombre"].ToString(),
                                Precio = Convert.ToDouble(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                MarcaS = dr["NombreM"].ToString(),
                                CategoriaS = dr["NombreC"].ToString()

                            });
                        }
                        return ListP;
                    }
                    if (marca != null)
                    {
                        //SELECT * FROM Productos WHERE Nombre LIKE '%p%';
                        connection.Open();
                        var cmd = new SQLiteCommand(("SELECT Codigo,Nombre,Precio,Stock,NombreM,NombreC FROM Productos P LEFT JOIN Marcas M ON(P.Marca == M.Id)LEFT JOIN Categorias C ON(P.Categoria == C.Id) WHERE Marca LIKE '%@MARCA%'"), connection);
                        cmd.Parameters.AddWithValue("MARCA", marca);
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        ListP = new List<Producto>();
                        while (dr.Read())
                        {
                            ListP.Add(new Producto
                            {
                                Codigo = Convert.ToInt64(dr["Codigo"]),
                                Nombre = dr["Nombre"].ToString(),
                                Precio = Convert.ToDouble(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                MarcaS = dr["NombreM"].ToString(),
                                CategoriaS = dr["NombreC"].ToString()

                            });
                        }
                        return ListP;
                    }
                    if (categoria != null)
                    {
                        //SELECT * FROM Productos WHERE Nombre LIKE '%p%';
                        connection.Open();
                        var cmd = new SQLiteCommand(("SELECT (Codigo,Nombre,Precio,Stock,NombreM,NombreC)FROM Productos P LEFT JOIN Marcas M ON(P.Marca == M.Id)LEFT JOIN Categorias C ON(P.Categoria == C.Id) WHERE Categoria LIKE '%@CATEGORIA%'"), connection);
                        cmd.Parameters.AddWithValue("CATEGORIA", categoria);
                        SQLiteDataReader dr = cmd.ExecuteReader();
                        ListP = new List<Producto>();
                        while (dr.Read())
                        {
                            ListP.Add(new Producto
                            {
                                Codigo = Convert.ToInt64(dr["Codigo"]),
                                Nombre = dr["Nombre"].ToString(),
                                Precio = Convert.ToDouble(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                MarcaS = dr["NombreM"].ToString(),
                                CategoriaS = dr["NombreC"].ToString()

                            });
                        }
                        connection.Close();
                        return ListP;
                    }

                    return null;
                }

            }
            catch (Exception err)
            {
                return null;
            }
        }
        //actualizar produto 
        public static int ActualizarProducto(int codigo,Producto producto)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConS))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd;
                    cmd = new SQLiteCommand("UPDATE Productos SET Nombre =@NOMBRE, Precio =@PRECIO, Stock =@STOCK, Marca =@MARCA, Categoria =@CATEGORIA WHERE Codigo==@CODIGO", connection);
                    cmd.Parameters.AddWithValue("NOMBRE", producto.Nombre.ToString());
                    cmd.Parameters.AddWithValue("PRECIO", producto.Precio);
                    cmd.Parameters.AddWithValue("STOCK", producto.Stock);
                    cmd.Parameters.AddWithValue("MARCA", producto.Marca);
                    cmd.Parameters.AddWithValue("CATEGORIA", producto.Categoria);
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception err)
                {
                    return -1;
                }
            }
        }
        public static List<Producto> Productos() {
            using (SQLiteConnection connection = new SQLiteConnection(ConS)) {
                try
                {
                    List<Producto> ListP=new List<Producto>();
                    connection.Open();
                    var cmd = new SQLiteCommand(("SELECT Codigo,Nombre,Precio,Stock,NombreM,NombreC FROM Productos P LEFT JOIN Marcas M ON(P.Marca == M.Id)LEFT JOIN Categorias C ON(P.Categoria == C.Id)"), connection);
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListP.Add(new Producto
                        {
                            Codigo = Convert.ToInt64(dr["Codigo"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDouble(dr["Precio"]),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            MarcaS = dr["NombreM"].ToString(),
                            CategoriaS = dr["NombreC"].ToString()

                        });
                    }
                    return ListP;
                }
                catch (Exception err)
                {
                    return null;
                }
            }
        }
    }
}
