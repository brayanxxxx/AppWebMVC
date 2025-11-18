using Microsoft.Data.SqlClient;
using WebAppProductsandCategories.Models;



namespace WebAppProductsandCategories.Data
{
    public class CategoryData : DataAccess
    {
        public CategoryData(IConfiguration configuration) : base(configuration)
        {

        }


        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Categories", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }

            return categories;
        }

        // Create a New Category
        public void CreateCategory(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Categories (Name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", category.Name);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Update an Existing Category
        public void UpdateCategory(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Categories SET Name = @Name WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Delete a Category
        public void DeleteCategory(int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Categories WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Get a Single Category by Id
        public Category? GetCategoryById(int categoryId)
        {
            Category? category = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Categories WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            Name = reader["Name"].ToString()
                        };
                    }
                }
            }

            return category;
        }
    }
}
