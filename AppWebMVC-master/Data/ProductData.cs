using Microsoft.Data.SqlClient;
using WebAppProductsandCategories.Models;

namespace WebAppProductsandCategories.Data
{
    public class ProductData : DataAccess
    {
        public ProductData(IConfiguration configuration) : base(configuration)
        {

        }

        // Read All Products
        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"],
                            CategoryId = (int)reader["CategoryId"]
                        });
                    }
                }
            }

            return products;
        }

        // Create a New Product
        public void CreateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Products (Name, Price, CategoryId) VALUES (@Name, @Price, @CategoryId)", connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Update an Existing Product
        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Delete a Product
        public void DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Products WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Get a Single Product by Id
        public Product? GetProductById(int productId)
        {
            Product? product = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"],
                            CategoryId = (int)reader["CategoryId"]
                        };
                    }
                }
            }

            return product;
        }
    }
}
