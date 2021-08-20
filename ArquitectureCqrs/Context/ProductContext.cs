using Microsoft.Data.SqlClient;
using PatronCqrs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.Context
{
    public class ProductContext : IProductContext
    {

        readonly string ConnectionString;
        public ProductContext(string connectionString) => ConnectionString = connectionString;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> Add(Product product)
        {
            Object CommandExecutionResult = null;
            try
            {
                using (var Connection = new SqlConnection(ConnectionString))
                {
                    using (var Command = Connection.CreateCommand())
                    {
                        Command.CommandType = System.Data.CommandType.Text;
                        Command.CommandText = "Update Products set " +
                            $"{nameof(Product.Name)} = @Name, " +
                            $"{nameof(Product.QuantityPerUnit)} = @QuantityPerUnit, " +
                            $"{nameof(Product.Description)} = @Description," +
                            $"{nameof(Product.UnitPrice)} = @UnitPrice, " +
                            $"{nameof(Product.UnitsInStock)} = @UnitsInStock, " +
                            $"{nameof(Product.UnitsOnOrder)} = @UnitsOnOrder," +
                            $"{nameof(Product.ReorderLevel)} = @ReorderLevel, " +
                            $"{nameof(Product.Discontinued)} = @Discontinued " +
                            $"Where Id = {product.Id}";

                        SetSqlParameters(Command.Parameters, product);
                        await Connection.OpenAsync();

                        CommandExecutionResult = await Command.ExecuteScalarAsync();

                        return CommandExecutionResult ==
                            null ? -1 : (int)CommandExecutionResult;
                    }
                }
            }
            catch (Exception ex)
            {
                // Procesar la excepción
                Debug.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAll()
        {
            List<Product> Products = null;
            try
            {
                using (var Connection = new SqlConnection(ConnectionString))
                {
                    using (var Command = Connection.CreateCommand())
                    {
                        Command.CommandType = System.Data.CommandType.Text;
                        Command.CommandText = "Select * from Products;";

                        await Connection.OpenAsync();
                        var Reader = await Command.ExecuteReaderAsync();
                        if (Reader != null)
                        {
                            Products = new List<Product>();
                            while (await Reader.ReadAsync())
                            {
                                Products.Add(GetProduct(Reader));
                            }
                        } 
                    }
                }

                return Products;
            }
            catch (Exception ex)
            {
                // Procesar la excepción
                Debug.WriteLine(ex.Message);
                return new List<Product>();
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetById(int id)
        {
            Product Product = null;
            var Connection = new SqlConnection(ConnectionString);
            try
            {
                var Command = Connection.CreateCommand();
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandText = "Select * from Products where Id = @Id;";
                Command.Parameters.AddWithValue("@Id", id);

                await Connection.OpenAsync();
                var Reader = await Command.ExecuteReaderAsync();
                if (Reader != null)
                {
                    await Reader.ReadAsync();
                    Product = GetProduct(Reader);
                    await Reader.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                // Procesar la excepción
                Debug.WriteLine(ex.Message);
            }

            await Connection.DisposeAsync();

            return Product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Remove(int id)
        {
            Object CommandExecutionResult = null;
            var Connection = new SqlConnection(ConnectionString);
            try
            {
                var Command = Connection.CreateCommand();
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandText = "Delete from Products where Id = @Id;";

                Command.Parameters.AddWithValue("@Id", id);

                await Connection.OpenAsync();
                CommandExecutionResult = await Command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Procesar la excepción
                Debug.WriteLine(ex.Message);
            }

            await Connection.DisposeAsync();

            return CommandExecutionResult ==
                null ? false : ((int)CommandExecutionResult == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> Update(Product product)
        {
            Object CommandExecutionResult = null;
            var Connection = new SqlConnection(ConnectionString);
            try
            {
                using (var Command = Connection.CreateCommand())
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = "Update Products set " +
                        $"{nameof(Product.Name)} = @Name, " +
                        $"{nameof(Product.QuantityPerUnit)} = @QuantityPerUnit, " +
                        $"{nameof(Product.Description)} = @Description," +
                        $"{nameof(Product.UnitPrice)} = @UnitPrice, " +
                        $"{nameof(Product.UnitsInStock)} = @UnitsInStock, " +
                        $"{nameof(Product.UnitsOnOrder)} = @UnitsOnOrder," +
                        $"{nameof(Product.ReorderLevel)} = @ReorderLevel, " +
                        $"{nameof(Product.Discontinued)} = @Discontinued " +
                        $"Where Id = {product.Id}";

                    SetSqlParameters(Command.Parameters, product);


                    CommandExecutionResult = await Command.ExecuteNonQueryAsync();
                }
                
            }
            catch (Exception ex)
            {
                // Procesar la excepción
                Debug.WriteLine(ex.Message);
            }

          
            return CommandExecutionResult ==
                null ? false : ((int)CommandExecutionResult == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="product"></param>
        private void SetSqlParameters(SqlParameterCollection parameters, Product product)
        {
            parameters.AddWithValue($"@{nameof(Product.Name)}", product.Name);
            parameters.AddWithValue($"@{nameof(Product.QuantityPerUnit)}",
                product.QuantityPerUnit);
            parameters.AddWithValue($"@{nameof(Product.Description)}",
    product.Description);
            parameters.AddWithValue($"@{nameof(Product.UnitPrice)}",
    product.UnitPrice);
            parameters.AddWithValue($"@{nameof(Product.UnitsInStock)}",
    product.UnitsInStock);
            parameters.AddWithValue($"@{nameof(Product.UnitsOnOrder)}",
    product.UnitsOnOrder);
            parameters.AddWithValue($"@{nameof(Product.ReorderLevel)}",
    product.ReorderLevel);
            parameters.AddWithValue($"@{nameof(Product.Discontinued)}",
    product.Discontinued);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        Product GetProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = reader.GetInt32(
                    reader.GetOrdinal(nameof(Product.Id))),
                Name = reader.GetString(
                    reader.GetOrdinal(nameof(Product.Name))),
                QuantityPerUnit = reader.GetString(
                    reader.GetOrdinal(nameof(Product.QuantityPerUnit))),
                Description = reader.GetString(
                    reader.GetOrdinal(nameof(Product.Description))),
                UnitPrice = reader.GetDecimal(
                    reader.GetOrdinal(nameof(Product.UnitPrice))),
                UnitsInStock = reader.GetInt32(
                    reader.GetOrdinal(nameof(Product.UnitsInStock))),
                UnitsOnOrder = reader.GetInt32(
                    reader.GetOrdinal(nameof(Product.UnitsOnOrder))),
                ReorderLevel = reader.GetInt32(
                    reader.GetOrdinal(nameof(Product.ReorderLevel))),
                Discontinued = reader.GetBoolean(
                    reader.GetOrdinal(nameof(Product.Discontinued)))
            };
        }


    }
}
